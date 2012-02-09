using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DangerousGame
{
    class Player
    {

        private List<Monster> Monsters;

        public Player(List<Monster> availableMonsters)
        {

            Monster p = availableMonsters[3];
            p.Reset(5);

            Monsters = new List<Monster>();
            Monsters.Add(p);
        }

        public List<Monster> GetMonsters()
        {
            return Monsters;
        }
    }
}
