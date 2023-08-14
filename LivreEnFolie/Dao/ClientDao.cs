using LivreEnFolie.Models;
using System.Data.SqlClient;

namespace LivreEnFolie.Dao
{
    public class ClientDao
    {
        string conString = "Data Source=DESKTOP-OORU9G9\\MSSQLSERVER01;Initial Catalog=LivreEnFolie;Integrated Security=True;TrustServerCertificate=True";
        
        public IEnumerable<Client> getAllClients() 
        {
            List<Client> clients = new List<Client>();

            SqlConnection con = new SqlConnection(conString);
            
            con.Open();

            string req = "select * from client";

            SqlCommand cmd = new SqlCommand(req, con);
            
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Client client = new Client();
                client.id_client= Convert.ToInt32(dr["id_client"]);
                client.username= dr["username"].ToString();
                client.email= dr["email"].ToString();
                client.password= dr["password"].ToString();

                clients.Add(client);
            }
            return clients;
        }

        public void AddClient(Client client)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            string sql = "insert into client(username,email,password) values(@username,@email,@password) ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@username", client.username);
            cmd.Parameters.AddWithValue("@email", client.email);
            cmd.Parameters.AddWithValue("@password", client.password);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Client getClient(int id) 
        {
            Client client = new Client();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "select * from client where id_client=@id";
            SqlCommand cmd =new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
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

        public void PutClient(int id, Client client) 
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "update client set username=@username, email=@email, password=@password where id_client=@id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@username", client.username);
            cmd.Parameters.AddWithValue("@email", client.email);
            cmd.Parameters.AddWithValue("@password",client.password);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteClient(int id) 
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = "delete from client where id_client=@id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
