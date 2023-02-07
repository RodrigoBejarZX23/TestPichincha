using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestPichincha.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Contrasenia { get; set; }
        public bool Estado { get; set; }
        //public ICollection<Cuenta> Cuenta { get; set; }

        public int PersonaId { get; set; }

        [ForeignKey("PersonaId")]
        public Persona Persona { get; set; }
    }
}
