//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsultaLlamadas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleLlamadas
    {
        public long CallDetailId { get; set; }
        public long MobileLine { get; set; }
        public string CalledPartyNumber { get; set; }
        public string CalledPartyDescription { get; set; }
        public string DateTime { get; set; }
        public string Duration { get; set; }
        public long TotalCost { get; set; }
    }
}
