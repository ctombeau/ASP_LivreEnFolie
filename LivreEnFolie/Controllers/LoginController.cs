using LivreEnFolie.Dao;
using LivreEnFolie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LivreEnFolie.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        [Route("")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("")]
        public IActionResult LoginAction()
        {
            if(ModelState.IsValid)
            {
                string username = Request.Form["username"];
                string password = Request.Form["password"];
                LoginDao log = new LoginDao();
                Client client = new Client();
                client = log.Login(username, password);
                Console.WriteLine(client.username);
                if(client.username!=null && client.email!=null && client.password!=null)
                {
                    this.HttpContext.Session.SetString("username" ,client.username);
                    return RedirectToAction("ListerLivre","Livre");
                }
                else
                {
                    ViewBag.message = "Le nom d'utlisateur et/ou le mot de passe est incorrect.";
                    return View("Login");
                    
                }
                    
                
            }
            else
                return View("Login");
        }

        [HttpGet]
        public IActionResult Logout() 
        {
            this.HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}
