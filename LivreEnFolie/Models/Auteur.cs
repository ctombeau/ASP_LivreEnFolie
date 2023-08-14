namespace LivreEnFolie.Models
{
    public class Auteur
    {
        public int id_auteur { get; set; }

        public string nom { get; set; }

        public string prenom { get; set; }

        public string NomComplet()
        {
            return nom + " " + prenom;
        }
    }
}
