using LivreEnFolie.Dao;
using LivreEnFolie.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivreEnFolie.Controllers
{
    public class ClientController : Controller
    {
        [Route("clients")]
        [HttpGet]
        public IActionResult ListerClient()
        {
            string username = this.HttpContext.Session.GetString("username");
            if (username != null)
            {
                ViewBag.Username = username;
                List<Client> clients = new List<Client>();
                ClientDao cliDao = new ClientDao();
                clients = (List<Client>)cliDao.getAllClients();
                return View(clients);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        [Route("client/add")]
        public IActionResult AjouterClient() 
        {
            string username = this.HttpContext.Session.GetString("username");
            if (username != null)
            {
                ViewBag.Username = username;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        [Route("client/add")]
        public IActionResult PostClient(Client client) 
        {
            Console.WriteLine(client.username);
            Console.WriteLine(client.email);
            Console.WriteLine(client.password);
            
            
            if(ModelState.IsValid) 
            {   
                string password2 = Request.Form["password2"];

               if (password2.Equals(client.password))
                {
                    ClientDao clientDao = new ClientDao();
                    clientDao.AddClient(client);
                    return RedirectToAction("ListerClient");
                }
                else
                {
                    ViewBag.error = "Les mots de passe doivent etre identiques";
                    return View("AjouterClient");
                }
                     
           
            }
            else
                return View("AjouterClient");
            
        }

        [HttpGet]
        [Route("client/update/{id}")]
        public IActionResult ModifierClient(int id)
        {
            string username = this.HttpContext.Session.GetString("username");
            if (username != null)
            {
                ViewBag.Username = username;
                Client client = new Client();
                ClientDao clientDao = new ClientDao();
                client = clientDao.getClient(id);
                return View(client);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        [Route("client/update/{id}")]
        public IActionResult UpdateClient(int id, Client client) 
        {
            
            if(ModelState.IsValid) 
            {
                ClientDao cliDao = new ClientDao();
                cliDao.PutClient(id, client);
                return RedirectToAction("ListerClient");
            }
            else
                 return View(); 
        }

        [HttpGet]
        [Route("client/delete/{id}")]
        public IActionResult DeleteClient(int id) 
        {
            string username = this.HttpContext.Session.GetString("username");
            if (username != null)
            {
                ViewBag.Username = username;
                ClientDao clientDao = new ClientDao();
                clientDao.DeleteClient(id);
                return RedirectToAction("ListerClient");
            }
            else
                return RedirectToAction("Login", "Login");
        }
    }
}
