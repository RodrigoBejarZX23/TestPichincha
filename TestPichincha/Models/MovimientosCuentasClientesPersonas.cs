using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPichincha.Models
{
    public class MovimientosCuentasClientesPersonas
    {
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public int NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public int SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int Valor { get; set; }
        public int MovimientoId { get; set; }           
        public int CuentaId { get; set; }
        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        
    }
}
