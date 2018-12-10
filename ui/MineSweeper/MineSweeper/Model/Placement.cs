using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Model
{
    class Placement
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Placement() { }

        public Placement(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
    }
}
