using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Notes.Models; // Asegúrate de incluir el espacio de nombres correcto

public class RecordatoriosViewModel : BaseViewModel
{
    private readonly RecordatorioService _service;
    public ObservableCollection<Recordatorio> Recordatorios { get; } = new();

    public ICommand AgregarCommand { get; }
    public ICommand EliminarCommand { get; }

    public RecordatoriosViewModel()
    {
        _service = new RecordatorioService();
        AgregarCommand = new Command(async () => await AgregarRecordatorio());
        EliminarCommand = new Command<Recordatorio>(async (r) => await EliminarRecordatorio(r));
        _ = CargarRecordatorios();
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
        var nuevo = new Recordatorio { Texto = "Nuevo recordatorio", FechaHora = DateTime.Now.TimeOfDay, Activo = true };
        Recordatorios.Add(nuevo);
        await _service.GuardarTodosAsync(Recordatorios.ToList());
    }

    private async Task EliminarRecordatorio(Recordatorio r)
    {
        if (Recordatorios.Contains(r))
            Recordatorios.Remove(r);
        await _service.GuardarTodosAsync(Recordatorios.ToList());
    }
}
