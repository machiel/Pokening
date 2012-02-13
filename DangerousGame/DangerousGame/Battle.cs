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

        public enum Outcomes
        {
            PlayerWon,
            Undecided,
            EnemyWon
        }

        private Outcomes Outcome = Outcomes.Undecided;

        private States State = States.StartBattle;

        public Battle(Player player, Monster monster)
        {
            this.Player = player;
            this.Monster = monster;
            Monster.Reset(3);
            ActivePlayerMonster = Player.GetMonsters()[0];
            State = States.PlayerTurn;
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

        public Outcomes GetOutcome()
        {
            return Outcome;
        }

        public void Attack()
        {
            if (State == States.PlayerTurn)
            {
                Monster.DecreaseHealth(20);

                if (Monster.GetHealth() <= 0)
                {
                    Outcome = Outcomes.PlayerWon;
                    State = States.EndBattle;
                }

                State = States.EnemyTurn;
            }
            else if (State == States.EnemyTurn)
            {
                ActivePlayerMonster.DecreaseHealth(10);

                if (ActivePlayerMonster.GetHealth() <= 0)
                {
                    Outcome = Outcomes.EnemyWon;
                    State = States.EndBattle;
                }

                State = States.PlayerTurn;
            }
        }
    }
}
