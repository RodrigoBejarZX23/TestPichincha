using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.DBContexts;
using TestPichincha.Models;

namespace TestPichincha.Repository
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly MyDbContext _dbContext;

        public CuentaRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string DeleteCuenta(int CuentaId)
        {
            var cuenta = _dbContext.Cuentas.Find(CuentaId);
            _dbContext.Cuentas.Remove(cuenta);
            Save();
            return CuentaId.ToString();
        }

        public Cuenta GetCuenta(int CuentaId)
        {
            try
            {
                var obj = _dbContext.Cuentas.Find(CuentaId);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    var emptyobject = new Cuenta();
                    return emptyobject;
                }
            }
            catch (Exception e)
            {
                var text = e.ToString();
                return null;
            }
        }

        public IEnumerable<Cuenta> GetCuentas()
        {
           
            try
            {
                return _dbContext.Cuentas.ToList();
            }
            catch (Exception e)
            {
                var text = e.ToString();
                return null;
            }
        }

        public string InsertCuenta(Cuenta Cuenta)
        {
            _dbContext.Add(Cuenta);
            Save();
            return Cuenta.CuentaId.ToString();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public string UpdateCuenta(Cuenta Cuenta)
        {
            _dbContext.Entry(Cuenta).State = EntityState.Modified;
            Save();
            return Cuenta.CuentaId.ToString();
        }
    }
}
