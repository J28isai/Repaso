using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repaso.Model
{
    public class User
    {
        public Guid Id{ get; set; }
        public string Correo { get; set; }

        public string Name { get; set; }
        public string UserN { get; set; }
        public string Password { get; set; }

        public User(string correo, string usern, string password,string name)
        {
            Id = Guid.NewGuid();
            Correo = correo;
            UserN = usern;
            Password = password;
            Name = name;


        }
    }
}
