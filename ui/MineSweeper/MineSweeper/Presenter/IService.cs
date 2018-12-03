using Flurl;
using MineSweeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Flurl.Http;

namespace MineSweeper.Presenter
{
    class  IService
    {
        string api = "";
        /// <summary>
        /// 登录成功之后返回值
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> Login(User user)
        {
            var requestApi =  api + "login/";
            //实现一个post 请求并且获取返回值
            Message message = await requestApi.PostJsonAsync(user).ReceiveJson<Message>();
            if (message.Code == 200)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 如果登录成功且返回值为200 return true 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       public async Task<bool> Signin (User user)
        {
            var requestApi = api + "signin/";  
            Message message = await requestApi.PostJsonAsync(user).ReceiveJson<Message>();
            if (message.Code == 200)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取前三位排名的玩家
        /// </summary>
        /// <returns></returns>
        public async Task< List< Player>> GetTopPlayers()
        {
            
            var requestApi = api + "topthree/";
            IList<dynamic> list = await requestApi.GetJsonListAsync();
            List<Player> players = new List<Player>();
            foreach( Player player in list)
            {
                players.Add(player);
            }

            return players;
        }

        /// <summary>
        /// 挑战某一个玩家
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        async Task<bool> Challenge(Player player)
        {
            var requestApi = api+"challenge/";
            Message message = await requestApi
                .SetQueryParam("email", new { player.Email })
                .GetJsonAsync<Message>();
            if (message.Code == 200)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 用户获取更新 看自己有没有收到别人的挑战
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        async Task<bool> GetUpdate(User user)
        {
            var requestApi =  api + "status/";
            Message message = await requestApi.PostJsonAsync(user).ReceiveJson<Message>();
            if (message.Code == 200)
                return true;
            else
                return false;
        }
 
      
    }
}
