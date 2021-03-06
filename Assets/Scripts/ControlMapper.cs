﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class ControlMapper
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";
        private const string JUMP = "Jump";
        private const string FIRE = "Fire";
        private const string DASH = "Dash";
        private const string BUILD = "Build";
        private const string DESTROY = "Destroy";
        private const string START = "Start";

        private int _player;

        public ControlMapper(int player)
        {
            _player = player;
        }

        public string Horizontal { get { return HORIZONTAL + _player; } }
        public string Vertical { get { return VERTICAL + _player; } }
        public string Jump { get { return JUMP + _player; } }
        public string Fire { get { return FIRE + _player; } }
        public string Dash { get { return DASH + _player; } }
        public string Build { get { return BUILD + _player; } }
        public string Destroy { get { return DESTROY + _player; } }
        public string Start { get { return START + _player; } }

    }   
}
