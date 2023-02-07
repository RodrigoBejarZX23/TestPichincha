using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.DBContexts;
using TestPichincha.Models;

namespace TestPichincha.Repository
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly MyDbContext _dbContext;

        public PersonaRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteMPersona(int PersonaId)
        {
            var persona = _dbContext.Personas.Find(PersonaId);
            _dbContext.Personas.Remove(persona);
            Save();
        }

        public Persona GetPersona(int PersonaId)
        {
            return _dbContext.Personas.Find(PersonaId);
        }

        public IEnumerable<Persona> GetPersonas()
        {
            return _dbContext.Personas.ToList();
        }

        public void InsertPersona(Persona Persona)
        {
            _dbContext.Add(Persona);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdatePersona(Persona Persona)
        {
            _dbContext.Entry(Persona).State = EntityState.Modified;
            Save();
        }
    }
}
