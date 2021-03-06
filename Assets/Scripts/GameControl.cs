﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class GameControl
    {
        private static GameControl _instance;

        // Attributes
        private int _players;
        private int _player1Score;
        private int _player2Score;

        // Constants
        private const int _GAME_LENGTH = 30;
        private Chronometer chronometer;

        public int Player1Score { get { return _player1Score; } }
        public int Player2Score { get { return _player2Score; } }
        public int GameLength { get { return _GAME_LENGTH; } }

        // States
        public bool Running { get; private set; }

        public GameControl()
        {
            _players = 0;
            Running = true;
            chronometer = GameObject.FindGameObjectWithTag("Chronometer").GetComponent<Chronometer>();
        }

        public int NewPlayer()
        {
            _players++;
            return _players;
        }

        public void Pause()
        {
            Running = false;
            Time.timeScale = 0;
        }

        public void Resume()
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
            if (chronometer.time < 0) return;
            if (player == 1)
            {
                _player1Score++;
            }
            else if (player == 2)
            {
                _player2Score++;
            }
        }

        public void Reset()
        {
            _player1Score = _player2Score = 0;
            _players = 0;
        }

        public static GameControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameControl();
                _instance.chronometer = GameObject.FindGameObjectWithTag("Chronometer").GetComponent<Chronometer>(); ;
                return _instance;
            }
        }

    }
}
