using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MineSweeper.Presenter
{
    class AutoMineSweeper
    {
        private int[,] Field;
        private int[,] Panel;
        private int Row { get; set; }
        private int Column { get; set; }

        private long Time;

        private int[,] Visited;

        //当前发现的mine的数量
        public int Counter { get; set; }

        public int Bombs;

        //对应着已经打开了的field（已经将数字或者雷暴露出来）
        public int[,] Record { get; set; }
        private const int OPEN = 9;
        public AutoMineSweeper(int Row, int Column, int Bombs, MineGenerator generator)
        {
            Field = generator.Field;
            Panel = generator.Panel;

            this.Row = Row;
            this.Column = Column;


            this.Bombs = Bombs;

            Record = new int[Row, Column];
            Visited = new int[Row, Column];
        }

        public  long ExecuteSweepAsync(int startX, int startY,int delay)
        {
            long result = 0;
            var factory = new TaskFactory();
            var task = factory.StartNew(() =>
            {
                Sweep(startX, startY,delay);
            });
           // task.Start();
            return result;  
        }
        /// <summary>
        /// 机器人扫雷过程 的同步代码
        /// 直接暴力破解
        /// </summary>
        public void Sweep(int startX, int startY,int delay) {
            SweepHelper(startX, startY, delay);
        }

        private void SweepHelper(int curX, int curY,int delay)
        {
            int[] x = { -1, 0, 1 };
            int[] y = { -1, 0, 1 };


            Visited[curX, curY] = OPEN;
            Queue<Pair> queue = new Queue<Pair>();
            queue.Enqueue(new Pair(curX, curY));

            while(queue.Count!= 0)
            {
                Pair cur = queue.Dequeue();
                for(int i = 0;i < 3; i++)
                {
                    for(int j = 0; j<3; j++)
                    {
                        int nextX = cur.x + x[i];
                        int nextY = cur.y + y[j];

                        if (nextX < 0 || nextY < 0 || nextY >= Row || nextY >= Column)
                            continue;
                        if (Visited[nextX, nextY] == OPEN)
                            continue;
                        //扫到了地雷
                        if(Field[nextX,nextY] == -1)
                        {
                            Record[nextX, nextY] = 9;
                            Counter++;
                            Task.Delay(delay * 1000);
                            Debug.WriteLine("current counter" + Counter);
                        }

                        queue.Enqueue(new Pair(nextX, nextY));
                        Visited[nextX, nextY] = OPEN;
                    }
                }
                if (Counter == Bombs)
                    break;
            }
        }
    }

    class Pair
    {
        public int x { get; set; }
        public int y { get; set; }

        public Pair(int x ,int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
