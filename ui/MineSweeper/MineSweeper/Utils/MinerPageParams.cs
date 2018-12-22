using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Utils
{
    /*
     作为传递参数的类
     示例:
            var parameters = new MinerPageParams();
            parameters.Row = 8;
            parameters.Column = 8;
            Frame.Navigate(typeof(Miner), parameters);
         */
    class MinerPageParams
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
