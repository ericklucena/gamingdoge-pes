using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class TagControl
    {
        private const string BALL = "Ball";
        private const string PLAYER = "Player";
        private const string GROUND = "Ground";
        private const string SPAWN = "Spawn Area";

        public static string Ball { get { return BALL; } }
        public static string Player { get { return PLAYER; } }
        public static string Ground { get { return GROUND; } }

        public static string SpawArea { get { return SPAWN; } }
    }
}
