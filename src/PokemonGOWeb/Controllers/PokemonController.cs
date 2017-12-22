using PokemonGOCore.Model;
using PokemonGOCore.Service;
using PokemonGOWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PokemonGOWeb.Controllers
{
    public class PokemonController : Controller
    {

        #region Index 

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListPartial(int pageSize, int page, string query = null)
        {
            List<Pokemon> pokemonList;

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                pokemonList = new PokemonService().FindAll(x => 
                    x.Name.ToLower().Contains(query) || 
                    x.PokemonType.Description.ToLower().Equals(query)
                );
            }
            else
            {
                pokemonList = new PokemonService().FindAll();
            }

            if (pokemonList != null && pokemonList.Count > 0)
            {
                var pageCount = (int)Math.Ceiling(((decimal)pokemonList.Count) / pageSize);

                return PartialView("ListPartial", new PokemonIndexViewModel()
                {
                    PokemonList = pokemonList.GetRange(pageSize * (page - 1), page == pageCount ? pokemonList.Count - (pageSize * (page - 1)) : pageSize),
                    PageSize = pageSize,
                    Page = page,
                    PageCount = pageCount
                });
            }
            else
            {
                return PartialView("ListPartial", null);
            }
        }

        [HttpPost]
        public ActionResult ToggleCurrentHaveState(int PokemonId)
        {
            var pokeService = new PokemonService();
            var CurrentPokemon = pokeService.FindById(PokemonId);

            if(CurrentPokemon != null)
            {
                CurrentPokemon.CurrentHave = !CurrentPokemon.CurrentHave;
                pokeService.Update(CurrentPokemon);

                return new HttpStatusCodeResult(200);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Delete(int PokemonId)
        {
            var pokeService = new PokemonService();
            var CurrentPokemon = pokeService.FindById(PokemonId);

            if (CurrentPokemon != null)
            {
                pokeService.Delete(CurrentPokemon);

                return new HttpStatusCodeResult(200);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Save(PokemonIndexViewModel pokemon)
        {
            return View("Index");
        }

        #endregion

        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            var pokemonTypes = new PokemonTypeService().FindAll();

            return View(new PokemonCreateViewModel
            {
                PokemonTypes = pokemonTypes
            });
        }

        [HttpPost]
        public ActionResult Create(Pokemon pokemon)
        {
            new PokemonService().Insert(pokemon);

            return new HttpStatusCodeResult(200);
        }

        #endregion
    }
}