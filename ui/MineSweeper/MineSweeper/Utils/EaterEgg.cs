using MineSweeper.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;

namespace MineSweeper.Utils
{
    /// <summary>
    /// here is an eater egg which is sorry for not representing a gift for her @Rawsharkill
    /// </summary>
    class EaterEgg
    {
        public int row;
        public int column;
        public int x;
        public int y;

        public EaterEgg(int row, int column, int x, int y)
        {
            this.row = row;
            this.column = column;
            this.x = x;
            this.y = y;
        }

        public bool TriggerEaterEgg(int [,] field)
        {
            return false;
            if(row  != 9 || column != 9) {
                return false;
            }
            if (x != 4 && y != 4) return false;
            for(int i = 0; i< row; i++)
            {
                for(int j = 0; j < column; j++)
                {
                    field[i, j] = 0;
                }
            }

            field[2, 2] = 1;
            field[2, 6] = 1;
            field[8, 4] = 1;
            for(int i = 1; i < column - 1; i++)
            {
                field[3, i] = 1;
                field[5, i] = 1;
            }

            for (int i = 0; i < column; i++)
            {
                field[4, i] = 1;
            }

            for (int i = 2; i < column - 2; i++)
            {
                field[6, i] = 1;
            }

            for (int i = 3; i < column - 3; i++)
            {
                field[7, i] = 1;
            }
            DialogCreator.CreateDialog("猪猪我错了没有给你准备圣诞礼物！", "这里给你道个歉好不好 不要生我的气了QAQ");
            return true;
        }

    }
     
}
