//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BancoEmpleado.Models
{
    using System;
    using System.Collections.Generic;
    [Serializable()]
    public partial class empleado
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string tipo_ident { get; set; }
        public bool estado { get; set; }
        public string identificacion { get; set; }
    }
}
