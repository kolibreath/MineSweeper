using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MineSweeper
{
    /// <summary>
    /// 用户注册的时候提供的信息
    /// </summary>
    [DataContract]
    public class User
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public  string Passowrd { get; set; }

        public User(string email, string password)
        {
            Email = email;
            Passowrd = password;
        }
    }
}
