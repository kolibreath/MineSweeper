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
    public string Userpassword { get; set; }
        public string Email { get; set; }

        public UserRegister(string Username, string UserPassword,string Email)
        {
            this.UserName = Username;
            this.Userpassword = Userpassword;
            this.Email = Email;
        }
    }
}
