using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancoEmpleado.Models.ViewModel
{
    public class ViewModelEmpleado
    {

        public int ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TipoIdentificacion { get; set; }
        
        public string Identificacion { get; set; }

        public bool Estado { get; set; }

    }
}