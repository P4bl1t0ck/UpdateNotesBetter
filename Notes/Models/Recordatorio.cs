using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    //Este es el modelo de recordatorio, que contiene el texto del recordatorio, la fecha y hora en que debe sonar y si está activo o no.
    //Debe de poder guardar múliples registros en formato JSOn en un archivo en el almacenamiento local de la aplicación.
    //Tambine debe de incluir un método para cargar los recordatorios desde el archivo JSON y otro para guardar los recordatorios en el archivo JSON.
    //Muy similar al primer prototip de la aplicación de notas, pero con un enfoque en recordatorios.
 
    public class Recordatorio
    {
        public string Texto { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Activo { get; set; }
        //Una función que retorne si el recordatorio se encuentra activo (true o false)
        //y si la fecha y hora del recordatorio es igual o posterior a la fecha y hora actual.
        public bool DebeActivar() { return Activo && DateTime.Now >= FechaHora; }
    }
}
