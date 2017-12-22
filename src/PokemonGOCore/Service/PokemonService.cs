using PokemonGOCore.Model;
using PokemonGOCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PokemonGOCore.Service
{
    public class PokemonService
    {
        protected PokemonRepository _Repository;

        public PokemonService()
        {
            _Repository = new PokemonRepository();
        }

        public List<Pokemon> FindAll()
        {
            return _Repository.FindAll();
        }

        public List<Pokemon> FindAll(Expression<Func<Pokemon, bool>> predicate)
        {
            return _Repository.FindAll(predicate);
        }

        public Pokemon FindById(int id)
        {
            return _Repository.FindById(id);
        }

        public void Update(Pokemon pokemon)
        {
            _Repository.Update(pokemon);
        }

        public void Delete(Pokemon pokemon)
        {
            _Repository.Remove(pokemon);
        }

        public void Insert(Pokemon pokemon)
        {
            _Repository.Insert(pokemon);
        }

    }
}
