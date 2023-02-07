using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.Models;

namespace TestPichincha.Repository
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> GetClientes();
        Cliente GetCliente(int ClienteId);
        string InsertCliente(Cliente Cliente);
        string DeleteCliente(int ClienteId);
        string UpdateCliente(Cliente Cliente);
        void Save();
    }
}
