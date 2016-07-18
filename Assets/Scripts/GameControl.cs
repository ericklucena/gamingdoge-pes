using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class GameControl
    {
        private static GameControl _instance;

        private int _players;

        public GameControl()
        {
            _players = 0;
        }

        public int NewPlayer()
        {
            _players++;
            return _players;
        }

        public static GameControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameControl();
                return _instance;
            }
        }

    }
}
