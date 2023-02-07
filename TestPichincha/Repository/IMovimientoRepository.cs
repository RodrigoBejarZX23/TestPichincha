using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.Models;

namespace TestPichincha.Repository
{
    public interface IMovimientoRepository
    {
        IEnumerable<Movimiento> GetMovimientos();
        Movimiento GetMovimiento(int MovimientoId);
        string InsertMovimiento(Movimiento Movimiento);
        string DeleteMovimiento(int MovimientoId);
        string UpdateMovimiento(Movimiento Movimiento);
        void Save();
        Cuenta ObtenerCuenta(int MovimientoId);
        Cliente ObtenerCliente(int CuentaId);
        Persona ObtenerPersona(int ClienteId);
    }
}
