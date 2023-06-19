using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Sql;


namespace mystore.Pages.Contact
{
    public class ContactModel : PageModel
    {
        public ContactInfo contactInfo = new ContactInfo();
        public String errorMessage = " ";

        public String successMessage = "";
        private string connectionString;

        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {

           
        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            Response.Redirect("/Contact/Index");
        }
      
    }
}
