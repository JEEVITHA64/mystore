using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace mystore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        
        public string Search { get; set; }
        public class clients
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public DateTime create_dat { get; set; } 
            public DateTime modified_at { get; set; }

            internal static object Find(Func<object, bool> p)
            {
                throw new NotImplementedException();
            }
        }
       

        public void OnGet()
        {
            


            try
            {
                String connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=ProgrammingDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT*FROM clients";
                    using (SqlCommand command=new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader=command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = ""+ reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();
                                clientInfo.modified_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {

            }
        }
        public class ClientInfo
        {
            public String id;
            public String name;
            public String email;
            public String phone;
            public String address;
            public String created_at;
           public String modified_at;
           
        }
    }

    internal class datatable
    {
    }
}
