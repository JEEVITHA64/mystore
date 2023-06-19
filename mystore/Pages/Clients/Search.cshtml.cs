using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static mystore.Pages.Clients.IndexModel;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace mystore.Pages.Clients
{
    public class SearchModel : PageModel
    {
        public String errorMessage = "";

        public List<ClientInfo> Clients { get; set; }


        public void OnGet()
        {
            String name = Request.Query["name"];
            try
            {
                String connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=ProgrammingDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT* FROM clients WHERE name LIKE @name";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", "%" + name + "%");
                        Clients = new List<ClientInfo>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            while (reader.Read())
                            {


                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            };


                        }
                    }

                }
            }



            catch (Exception ex)
            {

            }
        }
        public void OnPost()
        {

            try
            {
                
            if (clientInfo.name.Length == 0)
            {
                errorMessage = "Name should be mentioned.";
                return;
            }

            String connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=ProgrammingDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                {
                    clientInfo.id = Request.Form["id"];
                    clientInfo.name = Request.Form["name"];
                    clientInfo.email = Request.Form["email"];
                    clientInfo.phone = Request.Form["phone"];
                    clientInfo.address = Request.Form["address"];

                    Response.Redirect("/Clients/Index");
                }

            }

        }
            catch (Exception ex)
            {
            errorMessage=ex.Message;
            return;
            }
}

        
    }
}
  