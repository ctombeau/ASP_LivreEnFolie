using LivreEnFolie.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace LivreEnFolie.Dao
{
    public class LivreDao
    {
        string conString = "Data Source=DESKTOP-OORU9G9\\MSSQLSERVER01;Initial Catalog=LivreEnFolie;Integrated Security=True;TrustServerCertificate=True";

        public IEnumerable<Livre> getLivres()
        {
            List<Livre> livres = new List<Livre>();
            

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("getLivre", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Livre livre = new Livre();
                    livre.id_livre = Convert.ToInt32(dr["id_livre"]);
                    livre.titre = dr["titre"].ToString();
                    livre.categorie = dr["categorie"].ToString();
                    livre.prix = Convert.ToDouble(dr["prix"]);
                    livre.quantite = Convert.ToInt32(dr["quantite"]);
                    livre.nom = dr["nom"].ToString();
                    livre.prenom = dr["prenom"].ToString();
                    livres.Add(livre);
                }
            }

            return livres;
            //con.Close();
        }

        public void postLivre(Livre livre)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand("AjoutLivre",con);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@titre", livre.titre));
            cmd.Parameters.Add(new SqlParameter("@categorie",livre.categorie));
            cmd.Parameters.Add(new SqlParameter("@prix",livre.prix));
            cmd.Parameters.Add(new SqlParameter("@quantite",livre.quantite));
            cmd.Parameters.Add(new SqlParameter("@nom",livre.nom));
            cmd.Parameters.Add(new SqlParameter("@prenom",livre.prenom));
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public Livre getLivre(int id) 
        {
            Livre livre = new Livre();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string req ="select * from livre inner join auteur" +
                " on livre.id_auteur=auteur.id_auteur where id_livre=?";

            SqlCommand cmd = new SqlCommand(req, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read()) 
            {
                
                livre.id_livre = Convert.ToInt32(dr["id_livre"]);
                livre.titre = dr["titre"].ToString();
                livre.categorie = dr["categorie"].ToString();
                livre.prix = Convert.ToDouble(dr["prix"]);
                livre.quantite = Convert.ToInt32(dr["quantite"]);
                livre.nom = dr["nom"].ToString();
                livre.prenom = dr["prenom"].ToString();
            }
            return livre;
        }
    }
}
