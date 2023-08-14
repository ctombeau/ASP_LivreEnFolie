using LivreEnFolie.Models;
using Microsoft.Data.SqlClient;

namespace LivreEnFolie.Dao
{
    public class LoginDao
    {
        string conString = "Data Source=DESKTOP-OORU9G9\\MSSQLSERVER01;Initial Catalog=LivreEnFolie;Integrated Security=True;TrustServerCertificate=True"; // ;User ID=jedma; password=Edma1995

        public Client Login(string username, string password)
        {
            Client client = new Client();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "select * from client where username = @username and password=@password";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue ("@password", password);

            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read()) 
            {
                
                client.id_client = Convert.ToInt32(dr["id_client"]);
                client.username = dr["username"].ToString();
                client.email = dr["email"].ToString();
                client.password = dr["password"].ToString();
            }
            return client;
        }
    }
}
