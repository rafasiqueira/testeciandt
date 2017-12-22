using PokemonGOCore.Model;
using PokemonGOCore.Repository;
using System.Collections.Generic;
using System;

namespace PokemonGOCore.Service
{
    public class PokemonTypeService
    {
        protected PokemonTypeRepository _Repository;

        public PokemonTypeService()
        {
            _Repository = new PokemonTypeRepository();
        }

        public List<PokemonType> FindAll()
        {
            return _Repository.FindAll();
        }
    }
}
