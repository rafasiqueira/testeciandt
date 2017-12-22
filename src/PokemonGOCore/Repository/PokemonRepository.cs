using PokemonGOCore.Infrastructure;
using PokemonGOCore.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PokemonGOCore.Repository
{
    public class PokemonRepository
    {
        protected DbSet<Pokemon> _Repository;

        public PokemonRepository()
        {
            _Repository = UnitOfWork.Context.Set<Pokemon>();
        }

        public List<Pokemon> FindAll(Expression<Func<Pokemon, bool>> predicate)
        {
            return _Repository.Where(predicate).ToList();
        }

        public List<Pokemon> FindAll()
        {
            return _Repository.ToList();
        }

        public Pokemon FindById(int id)
        {
            return _Repository.Find(id);
        }

        public void Create(Pokemon pokemon)
        {
            _Repository.Add(pokemon);
            UnitOfWork.Context.SaveChanges();
        }

        public void Update(Pokemon pokemon)
        {
            UnitOfWork.Context.Entry(pokemon).State = EntityState.Modified;
            UnitOfWork.Context.SaveChanges();
        }

        public void Insert(Pokemon pokemon)
        {
            _Repository.Add(pokemon);
            UnitOfWork.Context.SaveChanges();
        }

        public void Remove(Pokemon pokemon)
        {
            _Repository.Remove(pokemon);
            UnitOfWork.Context.SaveChanges();
        }

        public void Remove(int id)
        {
            Pokemon pokemon = FindById(id);
            _Repository.Remove(pokemon);
            UnitOfWork.Context.SaveChanges();
        }
    }
}
