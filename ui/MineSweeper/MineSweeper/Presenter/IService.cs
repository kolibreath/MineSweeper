using Flurl;
using MineSweeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Flurl.Http;
using System.Diagnostics;
using MineSweeper.Utils;

namespace MineSweeper.Presenter
{
    class  IService
    {
        string api = "http://39.108.79.110:4242/api/v1.0/";
        /// <summary>
        /// 登录成功之后返回值
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> Login(UserLogin user)
        {
            var requestApi =  api + "login/";
            //实现一个post 请求并且获取返回值
            Message message = null;
            try
            {
                message = await requestApi.PostJsonAsync(user).ReceiveJson<Message>();
                if (message.Code == 200)
                {
                    UserAccountHelper.USER_ID = message.UserId;
                    return true;
                }
                else
                    return false;
            }
            catch (FlurlHttpException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 如果登录成功且返回值为200 return true 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       public async Task<bool> SignUp (UserRegister user)
        {
            var requestApi = api + "signup/";
            Message message = null;
            try
            {
                message = await requestApi.PostJsonAsync(user).ReceiveJson<Message>();
                if (message.Code == 200)
                    return true;
                else
                    return false;
            }
            catch(FlurlHttpException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// 获取前三位排名的玩家
        /// </summary>
        /// <returns></returns>
        public async Task< List< Player>> GetTopPlayers()
        {
            
            var requestApi = api + "topthree/";
            Players ps = await requestApi.GetJsonAsync<Players>();
            List<Player> players = new List<Player>();
            foreach( Player player in ps.TopPlayers)
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
        public async Task<MineGenerator> Challenge(Player player,
            int row,int column,int bombs)
        {
            var requestApi = api+"challenge/";
            //构造函数中初始化地雷阵
            MineGenerator mine = new MineGenerator(row, column, bombs);
            int[,] field = mine.Field;

            List<Placement> placements = new List<Placement>();

            for(int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    //地雷的数目
                    if (field[i, j] == 1)
                    {
                        Placement placement = new Placement(i, j);
                        placements.Add(placement);
                    }
                }
            }

            Challenge challenge = new Challenge(row, column, bombs,placements);
            //挑战某人的时候需要生成挑战的二维数组！
            Message message = await requestApi
                .SetQueryParam("myid", new { UserAccountHelper.USER_ID })
                .SetQueryParam("otherid", new { player.UserId })
                .PostJsonAsync(challenge)
                .ReceiveJson<Message>();

            if (message.Code == 200)
                return mine;
            else
                return null;
        }

        /// <summary>
        /// 用户获取更新 看自己有没有收到别人的挑战
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        async Task<bool> GetStatus(UserLogin user)
        {
            var requestApi =  api + "status/";
            //要根据请求的过程重建这个地雷阵
            ChallengeMessage message = await requestApi.PostJsonAsync(user).ReceiveJson<ChallengeMessage>();

            if (message.Code == 200)
                return true;
            else
                return false;
        }
 
      
    }
}
