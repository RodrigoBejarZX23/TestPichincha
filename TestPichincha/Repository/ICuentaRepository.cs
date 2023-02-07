using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.Models;

namespace TestPichincha.Repository
{
    public interface ICuentaRepository
    {
        IEnumerable<Cuenta> GetCuentas();
        Cuenta GetCuenta(int CuentaId);
        string InsertCuenta(Cuenta Cuenta);
        string DeleteCuenta(int CuentaId);
        string UpdateCuenta(Cuenta Cuenta);
        void Save();
    }
}
