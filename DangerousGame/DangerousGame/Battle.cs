using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DangerousGame
{
    class Battle
    {

        private Player Player;
        private Monster Monster;
        private Monster ActivePlayerMonster;

        public enum States
        {
            StartBattle,
            PlayerTurn,
            EnemyTurn,
            EndBattle
        }

        private States State = States.StartBattle;

        public Battle(Player player, Monster monster)
        {
            this.Player = player;
            this.Monster = monster;
            Monster.Reset(3);
            ActivePlayerMonster = Player.GetMonsters()[0];
        }

        public Monster GetMonster()
        {
            return Monster;
        }

        public Monster GetActivePlayerMonster()
        {
            return ActivePlayerMonster;
        }

        public Player GetPlayer()
        {
            return Player;
        }

        public States GetState()
        {
            return this.State;
        }
    }
}
