using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Chronometer : MonoBehaviour
    {
        private float _time;

        private GameControl _control;


        void Awake()
        {
            _control = GameControl.Instance;
            _time = _control.GameLength;
        }

        // Update is called once per frame
        void Update()
        {
            if (_time >= 0)
            {
                GetComponent<GUIText>().text = _time.ToString("0");
                _time -= Time.deltaTime;
            }
            else
            {
                if (_control.Player1Score > _control.Player2Score)
                {
                    GetComponent<GUIText>().text = "Player 1 WINS!!!";
                }
                else if (_control.Player2Score > _control.Player1Score)
                {
                    GetComponent<GUIText>().text = "Player 2 WINS!!!";
                }
                else
                {
                    GetComponent<GUIText>().text = "IT'S A DRAW!!!";
                }

                StartCoroutine(Reload());
            }

        }

        IEnumerator Reload()
        {
            yield return new WaitForSeconds(5);
            _control.Reset();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
            SceneManager.SetActiveScene(scene);
        }
    }
}
