using System.ComponentModel.DataAnnotations;

namespace LivreEnFolie.Models
{
    public class Livre
    {
        public int id_livre { get; set; }

        [Required(ErrorMessage = "Le titre du livre ne peut pas etre vide")]
        public string titre { get; set; }

        [Required(ErrorMessage = "Vous devez preciser la catégorie")]
        public string categorie { get;set; }

        [Required(ErrorMessage = "Vous devez donner le prix du livre")]
        public double prix { get; set; }

        [Required(ErrorMessage = "Vous devez preciser la quantité en stock")]
        public int quantite { get; set; }

        [Required(ErrorMessage = "vous devez preciser le nom de l'auteur")]
        public string nom { get; set; }

        [Required(ErrorMessage = "Vous devez preciser le prénom de l'auteur")]
        public string prenom { get; set; }
    }
}
