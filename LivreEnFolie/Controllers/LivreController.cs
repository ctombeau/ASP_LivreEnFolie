using LivreEnFolie.Dao;
using LivreEnFolie.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivreEnFolie.Controllers
{
    public class LivreController : Controller
    {
        [Route("livres")]
        [HttpGet]
        public IActionResult ListerLivre()
        {
            string username = this.HttpContext.Session.GetString("username");
            if(username!=null)
            {
                ViewBag.Username = username;
                List<Livre> livres = new List<Livre>();
                LivreDao livreDao = new LivreDao();
                livres = (List<Livre>)livreDao.getLivres();

                return View(livres);
            }
            else
            { 
                return RedirectToAction("Login","Login");
            }
        }

        [Route("livre/add")]
        [HttpGet]
        public IActionResult AjouterLivre() 
        {
            string username = this.HttpContext.Session.GetString("username");
            if (username != null)
            {
                ViewBag.Username = username;
                return View();
            }
            else
            { 
                return RedirectToAction("Login","Login"); 
            }
        }

        [Route("livre/add")]
        [HttpPost]
        public IActionResult AjouterLivre(Livre livre)
        {   
            LivreDao livreDao = new LivreDao();
            
            if (ModelState.IsValid)
            {
                livre.titre = Request.Form["titre"];
                livre.categorie = Request.Form["categorie"];
                livre.prix = Double.Parse(Request.Form["prix"]);
                livre.quantite = Convert.ToInt32(Request.Form["quantite"]);
                livre.nom = Request.Form["nom"];
                livre.prenom = Request.Form["prenom"];
                livreDao.postLivre(livre);
                return RedirectToAction("ListerLivre");
            }
            else
            {
                return View();
            }
        }

        [Route("update/{id}")]
        [HttpGet]
        public IActionResult ShowUpdateForm(int id)
        {   
            string username = this.HttpContext.Session.GetString("username");
            if(username != null)
            {
                ViewBag.Username = username;
                Livre livre = new Livre();
                LivreDao livreDao = new LivreDao();
                livre = livreDao.getLivre(id);
                return View(livre);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }
    }
}
