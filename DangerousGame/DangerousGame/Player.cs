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

            Monster p = (Monster)availableMonsters[3].Clone();
            List<Attack> attacks = new List<Attack>();
            attacks.Add(new Attack(25, "Thunderbolt"));
            attacks.Add(new Attack(15, "Tackle"));
            attacks.Add(new Attack(0, "Growl"));
            p.Reset(5, attacks);

            Monsters = new List<Monster>();
            Monsters.Add(p);
        }

        public List<Monster> GetMonsters()
        {
            return Monsters;
        }
    }
}
