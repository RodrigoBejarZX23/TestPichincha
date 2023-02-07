using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.DBContexts;
using TestPichincha.Models;

namespace TestPichincha.Repository
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly MyDbContext _dbContext;

        public MovimientoRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string DeleteMovimiento(int MovimientoId)
        {
            var movimiento = _dbContext.Movimientos.Find(MovimientoId);
            _dbContext.Movimientos.Remove(movimiento);
            Save();
            return MovimientoId.ToString();
        }

        public Movimiento GetMovimiento(int MovimientoId)
        {
            return _dbContext.Movimientos.Find(MovimientoId);
        }

        public IEnumerable<Movimiento> GetMovimientos()
        {     
            try
            {
                return _dbContext.Movimientos.ToList();
            }
            catch (Exception e)
            {
                var text = e.ToString();
                return null;
            }
        }

        public string InsertMovimiento(Movimiento Movimiento)
        {
            _dbContext.Add(Movimiento);
            Save();

            _dbContext.Add(Movimiento.Cuenta);
            _dbContext.Entry(Movimiento.Cuenta).State = EntityState.Modified;
            Save();
            return Movimiento.MovimientoId.ToString();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public string UpdateMovimiento(Movimiento Movimiento)
        {
            _dbContext.Entry(Movimiento).State = EntityState.Modified;
            Save();
            return Movimiento.MovimientoId.ToString();
        }

        public Cuenta ObtenerCuenta(int Id)
        {          
            var cuenta = _dbContext.Cuentas.Find(Id);
            return cuenta;
        }
        public Cliente ObtenerCliente(int Id)
        {
            var cliente = _dbContext.Clientes.Find(Id);
            return cliente;
        }
        public Persona ObtenerPersona(int Id)
        {
            var persona = _dbContext.Personas.Find(Id);
            return persona;
        }
    }
}
