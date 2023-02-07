using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestPichincha.Models
{
    public class Persona
    {
        [Key]
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public int Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        //public ICollection<Cliente> Cliente { get; set; }
    }
}
