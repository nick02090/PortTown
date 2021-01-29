using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models
{
    public class User: TableAddable
    { 
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string username, string email, string password)
        {
            // TODO: Remove this when connected to API
            Id = Guid.NewGuid();

            Username = username;
            Email = email;
            Password = password;
        }
    }
}
