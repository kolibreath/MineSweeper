using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Model
{
    class UserSignup
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserSignup(string UserName , string Email,string Password)
        {
            this.UserName = UserName;
            this.Email = Email;
            this.Password = Password;
        }
    }
}
