using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Presenter
{
    class MineGenerator
    {
        //地雷阵的长度和宽度
        int Row = 9;
        int Column = 9;

        public int OpenCount = 0;
        //表示有没有初始化成功
        public static bool _is_intialized = false;
        //如果field 内部是-1的话 就表示一个炸弹
        //Field表示一个地雷阵
        public int[,] Field { get; set; }
        int Bombs;
        //panel[i,j]表示这个点这里的数字 如果 1 即表示周围八个点有有一个确定的炸弹
        public int[,] Panel { get; set; }

        //记录一个扫雷的情况
        private const int OPEN = 9;
        public int[,] Record { get; set; }


        private int current = 0;
        //初始化Field 和 Panel 
        //如果踩到炸弹的话直接结束游戏 不会 展示panel中记录的周围的炸弹数目
        public MineGenerator(int Row, int Column,int Bombs)
        {
            this.Bombs = Bombs;
            this.Row = Row;
            this.Column = Column;
            this.Field = new int[Row, Column];
            this.Panel = new int[Row, Column];
            this.Record = new int[Row, Column];
            //随机选取Bomb个点作为炸弹生成
            int counter = 0;
            while(counter < Bombs)
            {
                var random = new Random();
                var x = random.Next(0, 100) % Row;
                var y = random.Next(0, 100) % Column;
                if (Field[x, y] != -1)
                    counter++;
                Field[x, y] = -1;
            }

            //初始化panel
            int[] xi = { -1, 0, 1 };
            int[] yi = { -1, 0, 1 };

            for(int i = 0;i < Row;i++)

                for(int j = 0; j< Column; j++)
                {
                    int bombCounter = 0;
                    //如果这个点是炸弹的话直接return
                    if (Field[i, j] == -1)
                    {
                        Panel[i, j] = -1;
                        continue;
                    }

                    for(int k = 0; k < xi.Length; k++)
                    {
                        for(int m = 0; m<yi.Length; m++)
                        {
                            if (k == 1 && m == 1)
                                continue;
                            int x = i + xi[k];
                            int y = j + yi[m];
                            if (x == -1 || x == Row || y == -1 || y == Column)
                            {
                                continue;
                            }
                            if (Field[x, y] == -1)
                                bombCounter++;
                        }
                    }
                    Panel[i, j] = bombCounter;
             
                }

            _is_intialized = true;
        }
        
        //生成field 的初始区域 使用dfs 
        //有一个生成总数 如果超过生成总数也不会继续生成
        public void InitArea(int dfs,int level,int x , int y,int max)
        {
            if (level > dfs || current > max)
                return;

            //全局最小值
            current++;
            Record[x, y] = OPEN;
            int[] dy = { -1, 0, 1 };
            int[] dx = { -1, 0, 1 };
            for(int i = 0;i < dx.Length; i++)
            {
                for(int j = 0; j<dy.Length; j++)
                {
                    int nextX = x + dx[i];
                    int nextY = y + dy[j];
                    if (nextX < 0 || nextX >= Row || nextY < 0 || nextY >= Column)
                        continue;
                    if (Field[nextX, nextY] == -1)
                        continue;
                    InitArea(dfs, level + 1, nextX, nextY,max);
                } 
            }
            OpenCount = current;
        }
        
    }
}   
