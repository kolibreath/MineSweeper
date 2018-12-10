using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Model
{
    
    class Challenge
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Bombs { get; set; }

        public List<Placement> Placements { get; set; }

        public Challenge()
        {

        }

        public Challenge(int row,int column,int Bombs,List<Placement> placements)
        {
            this.Bombs = Bombs;
            this.Row = row;
            this.Column = column;
            this.Placements = placements;
        }
    }
}
