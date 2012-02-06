using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DangerousGame
{
    class Properties
    {
        public const int WindowWidth = 800;
        public const int WindowHeight = 600;

        public const int TileWidth = 50;
        public const int TileHeight = 50;

        public const int MainCharacterWidth = 32;
        public const int MainCharacterHeight = 46;

        public class TileColorCodes
        {
            public const string Normal = "255255255";
            public const string Obstacle = "000";
            public const string Aggressive = "2372836";

            public const string Grass = "144233105";
            public const string Path = "254241154";
            public const string Roof = "2327777";
            public const string Wall = "255255255";
            public const string Forest = "7516137";

            public const string Door = "1901430";
            public const string Window = "174189124";
        }
    }
}
