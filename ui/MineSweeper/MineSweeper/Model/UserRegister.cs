using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Model
{
    class UserRegister
    {

     public string UserName { get; set; }
    public string Password { get; set; }
        public string Email { get; set; }

        public UserRegister(string Username, string UserPassword,string Email)
        {
            this.UserName = Username;
            this.Password = UserPassword;
            this.Email = Email;
        }
    }
}
