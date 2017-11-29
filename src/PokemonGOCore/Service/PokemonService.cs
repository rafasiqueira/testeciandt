using PokemonGOCore.Model;
using PokemonGOCore.Repository;
using System.Collections.Generic;

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
    }
}
