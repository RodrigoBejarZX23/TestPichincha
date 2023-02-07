using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.Models;

namespace TestPichincha.Repository
{
    public interface IPersonaRepository
    {
        IEnumerable<Persona> GetPersonas();
        Persona GetPersona(int PersonaId);
        void InsertPersona(Persona Persona);
        void DeleteMPersona(int PersonaId);
        void UpdatePersona(Persona Persona);
        void Save();
    }
}
