using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class GameControl
    {
        private static GameControl _instance;

        private int _players;
        private int _player1Score;
        private int _player2Score;

        public int Player1Score { get { return _player1Score; } }
        public int Player2Score { get { return _player2Score; } }

        // States
        public bool Running { get; private set; }

        public GameControl()
        {
            _players = 0;
            Running = true;
        }

        public int NewPlayer()
        {
            _players++;
            return _players;
        }

        private void Pause()
        {
            Running = false;
            Time.timeScale = 0;
        }

        private void Resume()
        {
            Running = true;
            Time.timeScale = 1;
        }

        public void ToogleRunning()
        {
            if (Running)
                Pause();
            else
                Resume();
        }

        public void GoalScored(int player)
        {
            if (player == 1)
            {
                _player1Score++;
            }
            else if (player == 2)
            {
                _player2Score++;
            }
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
