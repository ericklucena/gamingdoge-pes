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

        public static string Ball { get { return BALL; } }
        public static string Player { get { return PLAYER; } }
        public static string Ground { get { return GROUND; } }
    }
}
