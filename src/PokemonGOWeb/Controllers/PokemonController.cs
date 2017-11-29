using PokemonGOCore.Model;
using PokemonGOCore.Service;
using PokemonGOWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PokemonGOWeb.Controllers
{
    public class PokemonController : Controller
    {
        public ActionResult Index()
        {
            var service = new PokemonService();
            List<Pokemon> allPokemons = service.FindAll();

            //ToDo: Passar o allPokemons para a view model
            //Opcional: Criar uma view model para passar para a tela ao invés de passar a model do banco

            return View();
        }

        public ActionResult Search(string word)
        {
            return View("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(PokemonViewModel pokemon)
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return View("Index");
        }
    }
}