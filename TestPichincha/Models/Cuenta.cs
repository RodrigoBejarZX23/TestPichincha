using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestPichincha.Models
{
    public class Cuenta
    {
        [Key]
        public int CuentaId { get; set; }
        public int NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
        //public ICollection<Movimiento> Movimiento { get; set; }

        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
    }
}
