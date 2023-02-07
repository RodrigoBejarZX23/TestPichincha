using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestPichincha.Models
{
    public class Movimiento
    {
        [Key]
        public int MovimientoId { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public int Valor { get; set; }
        public int Saldo { get; set; }

        public int CuentaId { get; set; }

        [ForeignKey("CuentaId")]
        public Cuenta Cuenta { get; set; }
    }
}
