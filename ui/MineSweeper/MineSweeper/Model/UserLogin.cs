using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MineSweeper
{
    /// <summary>
    /// 用户登录的时候提供的信息
    /// </summary>
  
    public class UserLogin
    {
        private string userName;

        public string Email { get; set; }

        
        public  string Passowrd { get; set; }

        public UserLogin()
        {

        }
        public UserLogin(string email, string password)
        {
            Email = email;
            Passowrd = password;
        }

      
    }
}
