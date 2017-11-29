using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGOCore.Model
{
    public class Pokemon
    {
        public int Id { get; set; }       

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public bool CurrentHave { get; set; }

        public int PokemonTypeId { get; set; }
        public PokemonType PokemonType { get; set; }
    }
}
