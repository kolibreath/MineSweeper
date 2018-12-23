using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Model
{
    class UserScore
    {
        int UserId{ get; set; }
        int Score { get; set; }
        public UserScore(int userid,int score)
        {
            this.UserId = userid;
            this.Score = score;
        }

        public UserScore()
        {

        }
    }
}
