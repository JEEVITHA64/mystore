using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;

using static mystore.Pages.Clients.IndexModel;

namespace mystore.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        private object id;

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=ProgrammingDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT*FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        
            {
                clientInfo.id = Request.Form["id"];
                clientInfo.name = Request.Form["name"];
                clientInfo.email = Request.Form["email"];
                clientInfo.phone = Request.Form["phone"];
                clientInfo.address = Request.Form["address"];

                if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.email.Length == 0 || clientInfo.address.Length == 0)
                {
                    errorMessage = "All the fields are required";
                    return;
                }

                if (ModelState.IsValid)
                {
                    if (clientInfo.phone.Length > 10)
                    {
                        errorMessage = "Phone number must not exceed 10 characters.";
                        return;
                    }
                    try
                    {
                        String connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=ProgrammingDB;Integrated Security=True";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            String sql = "update clients " +
                                           "set name=@name, email=@email, phone=@phone, address=@address " +
                                            "where id=@id";


                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@name", clientInfo.name);
                                command.Parameters.AddWithValue("@email", clientInfo.email);
                                command.Parameters.AddWithValue("@phone", clientInfo.phone);
                                command.Parameters.AddWithValue("@address", clientInfo.address);
                                command.Parameters.AddWithValue("@id", clientInfo.id);


                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        return;
                    }
                    Response.Redirect("/Clients/Index");
                }
            }
        }
    }

