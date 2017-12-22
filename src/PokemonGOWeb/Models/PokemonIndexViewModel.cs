using PokemonGOCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonGOWeb.Models
{
    public class PokemonIndexViewModel
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public List<Pokemon> PokemonList { get; set; }
    }
}