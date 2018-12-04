using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Presenter
{
    class AutoMineSweeper
    {
        private int[,] Field;
        private int[,] Panel;
        private int Row;
        private int Column;

        //对应着已经打开了的field（已经将数字或者雷暴露出来）
        private int[,] Opened;
        public AutoMineSweeper(int Row,int Column, int Bombs)
        {
            MineGenerator generator = new MineGenerator(Row, Column,Bombs);
            Field = generator.Field;
            Panel = generator.Panel;

            this.Row = Row;
            this.Column = Column;
        }

        /// <summary>
        /// 机器人扫雷过程
        /// 直接暴力破解
        /// </summary>
        public void Sweep()
        {
            var random = new Random();
            int startX = random.Next(1, 100) % Row;
            int startY = random.Next(1, 100) % Column;

        }
    }
}
