using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Presenter
{
    class ScoreAccounter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">
        /// 时间：秒 可以通过TimerCounter 中GetElapsedTime 获取
        /// </param>
        /// <param name="level">
        /// 分成三哥等级 easy = 1 normal = 2 hard = 3
        /// </param>
        /// <returns></returns>
        public static int CountScore(int time,int level)
        {
            int maxedTime = 300;
            if (maxedTime > time)
                return level * Math.Abs(maxedTime - time);
            else
                return 0;
        }
    }
}
