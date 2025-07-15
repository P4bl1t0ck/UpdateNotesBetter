using System;

public class ProcesarArchivo()
{
	string rutaArchivo = "/app/data/entrada.txt";

	if (File.Exists(rutaArchivo)){
		string contenido = File.ReadAllText(rutaArchivo);
	    Console.WriteLine("Contenido Orginal");
		Console.WriteLine(contenido);

		string rutaSalida = "/app/data/salida.txt";
	    File.WriteAllText(rutaSalida, contenido.ToUpper());
		Console.WriteLine("Archivo procesado correctamente");

}
else
{

	Console.WriteLine("Archivo no encontrado")
}
