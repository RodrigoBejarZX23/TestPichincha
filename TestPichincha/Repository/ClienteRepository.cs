using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.DBContexts;
using TestPichincha.Models;

namespace TestPichincha.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly MyDbContext _dbContext;

        public ClienteRepository(MyDbContext dbContext) {
            _dbContext = dbContext;
        }

        public string DeleteCliente(int ClienteId)
        {
            var cliente = _dbContext.Clientes.Find(ClienteId);
            _dbContext.Clientes.Remove(cliente);
            Save();
            return ClienteId.ToString();
        }

        public Cliente GetCliente(int ClienteId)
        {
            try
            {   var obj = _dbContext.Clientes.Find(ClienteId);
                if (obj != null) {
                    return obj;
                }
                else
                { 
                    var emptyobject = new Cliente();
                    return emptyobject;
                }                
            }
            catch (Exception e)
            {
                var text = e.ToString();
                return null;
            }
        }

        public IEnumerable<Cliente> GetClientes()
        {
            try
            {
                return _dbContext.Clientes.ToList();
            }
            catch (Exception e)
            {
                var text = e.ToString();
                return null;
            }
            
        }

        public string InsertCliente(Cliente Cliente)
        {
            _dbContext.Add(Cliente);
            Save();
            return Cliente.ClienteId.ToString();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public string UpdateCliente(Cliente Cliente)
        {
            _dbContext.Entry(Cliente).State = EntityState.Modified;
            Save();
            return Cliente.ClienteId.ToString();
        }
    }
}
