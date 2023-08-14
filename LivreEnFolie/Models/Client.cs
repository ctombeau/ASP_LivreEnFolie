using System.ComponentModel.DataAnnotations;

namespace LivreEnFolie.Models
{
    public class Client
    {
        
        public int id_client { get; set; }

        [Required(ErrorMessage = "Le nom du client ne peut pas etre vide")]
        public string username { get; set; }

        [Required(ErrorMessage = "l'email ne peut pas etre vide")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Le mot de passe ne peut pas etre vide")]
        public string password { get; set; }
    }
}
