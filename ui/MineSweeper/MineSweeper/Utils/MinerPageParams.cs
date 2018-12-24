using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Utils
{
    /// <summary>
    /// 作为传递参数的类
    ///  示例:
    ///      var parameters = new MinerPageParams();
    ///      parameters.Row = 8;
    ///      parameters.Column = 8;
    ///      parameters.Bombs = 10;
    ///      Frame.Navigate(typeof(Miner), parameters);
    /// </summary>
    class MinerPageParams
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Bombs { get; set; }
        public int OpenButtonNum { get; set; }
        public int OpenButtonDepth { get; set;  }
        
    }
}
