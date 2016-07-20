using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Score : MonoBehaviour
    {
        private GameControl _control;
        void Awake()
        {
            _control = GameControl.Instance;
        }


        void Update()
        {
            GetComponent<GUIText>().text = _control.Player1Score + " : " + _control.Player2Score;

        }
    }
}
