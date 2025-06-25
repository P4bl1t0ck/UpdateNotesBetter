using Notes.Models;
using System.Text.Json;

public class RecordatorioService
{
    private readonly string filePath;

    public RecordatorioService()
    {
        filePath = Path.Combine(FileSystem.AppDataDirectory, "recordatorios.json");
    }

    public async Task<List<Recordatorio>> ObtenerTodosAsync()
    {
        if (!File.Exists(filePath))
            return new List<Recordatorio>();

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Recordatorio>>(json) ?? new List<Recordatorio>();
    }

    public async Task GuardarTodosAsync(List<Recordatorio> recordatorios)
    {
        var json = JsonSerializer.Serialize(recordatorios, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }
}
