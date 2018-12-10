using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Model
{
    class ChallengeMessage
    {
        public string Msg { get; set; }
        public string UserName { get; set; }

        public int Code { get; set; }

        public int Score { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }

        public int Bombs { get; set; }

        public List<Placement> placements;
    }
}
