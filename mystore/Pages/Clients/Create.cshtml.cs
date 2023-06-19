using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static mystore.Pages.Clients.IndexModel;
using System.Data.SqlClient;


namespace mystore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage="";
       
        public void OnGet()
        {

        }
        public void Onpost()
        {
            clientInfo.name = Request.Form["name"]; 
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if(clientInfo.name.Length==0 || clientInfo.email.Length==0 ||clientInfo.phone.Length==0 || clientInfo.address.Length==0  )
            {
                errorMessage = "all the fields are required";
                return;
            }
            if(ModelState.IsValid)
            {
               if (clientInfo.phone.Length>10)
                {
                    errorMessage = "phone number should not extend more than 10";
                    return;
                }
            }



            try
            {
                String connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=ProgrammingDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Insert into clients" + "(name,email,phone,address)Values" + "(@name,@email,@phone,@address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

                   
            clientInfo.name = "";clientInfo.email = "";clientInfo.phone = "";clientInfo.address = "";
            successMessage = "New clients added successfully";
        }
    }
}
