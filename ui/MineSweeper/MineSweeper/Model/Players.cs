using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Model
{
    class Players
    {
        public List<Player> TopPlayers { get; set; }
    }
    class Player
    {
        public string Email { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }
    }
}
