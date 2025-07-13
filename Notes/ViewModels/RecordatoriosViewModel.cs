using MvvmHelpers;
using Notes.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Notes.ViewModels
{
    public class RecordatoriosViewModel : BaseViewModel
    {
        private readonly RecordatorioService _service;
        public ObservableCollection<Recordatorio> Recordatorios { get; } = new();

        public ICommand AgregarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand EditarCommand { get; }

        public RecordatoriosViewModel()
        {
            _service = new RecordatorioService();
            AgregarCommand = new Command(async () => await AgregarRecordatorio());
            EliminarCommand = new Command<Recordatorio>(async (r) => await EliminarRecordatorio(r));
            EditarCommand = new Command<Recordatorio>(async (r) => await EditarRecordatorio(r));
            

            _ = CargarRecordatorios();
            _ = VerificarRecordatoriosActivos();
        }

        private async Task CargarRecordatorios()
        {
            var lista = await _service.ObtenerTodosAsync();
            Recordatorios.Clear();
            foreach (var item in lista)
                Recordatorios.Add(item);
        }

        private async Task AgregarRecordatorio()
        {
            // Pide el texto al usuario
            string texto = await Application.Current.MainPage.DisplayPromptAsync(
                "Nuevo Recordatorio",
                "¿Qué deseas recordar?",
                placeholder: "Ej: Pagar el agua");

            if (!string.IsNullOrWhiteSpace(texto))
            {
                // Pide la fecha/hora
                var fechaResult = await Application.Current.MainPage.DisplayPromptAsync(
                    "Fecha y Hora",
                    "Ingresa fecha y hora (dd/MM/yyyy HH:mm):",
                    initialValue: DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                if (DateTime.TryParseExact(fechaResult, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fechaHora))
                {
                    var nuevo = new Recordatorio
                    {
                        Texto = texto,
                        FechaHora = fechaHora,
                        Activo = true
                    };

                    Recordatorios.Add(nuevo);
                    await _service.GuardarTodosAsync(Recordatorios.ToList());
                }
            }
        }

        private async Task EditarRecordatorio(Recordatorio recordatorio)
        {
            string nuevoTexto = await Application.Current.MainPage.DisplayPromptAsync(
                "Editar Recordatorio",
                "Nuevo texto:",
                initialValue: recordatorio.Texto);

            if (!string.IsNullOrWhiteSpace(nuevoTexto))
            {
                recordatorio.Texto = nuevoTexto;
                await _service.GuardarTodosAsync(Recordatorios.ToList());
            }
        }

        private async Task EliminarRecordatorio(Recordatorio r)
        {
            if (await Application.Current.MainPage.DisplayAlert("Eliminar", "¿Estás seguro?", "Sí", "No"))
            {
                Recordatorios.Remove(r);
                await _service.GuardarTodosAsync(Recordatorios.ToList());
            }
        }

        private async Task VerificarRecordatoriosActivos()
        {
            while (true)
            {
                await Task.Delay(60000); // Revisa cada minuto
                foreach (var r in Recordatorios)
                {
                    if (r.DebeActivar())
                    {
                        await Application.Current.MainPage.DisplayAlert("Recordatorio", r.Texto, "OK");
                        r.Activo = false; // Desactiva después de mostrar
                    }
                }
            }
        }
    }
}