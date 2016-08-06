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
        public float time;
        public float endTime = 0;

        private GameControl _control;
        private Animator fireWorkBlue;
        private Animator fireWorkRed;

        void Awake()
        {
            fireWorkBlue = GameObject.FindGameObjectWithTag("fireWorkBlue").GetComponent<Animator>();
            fireWorkRed = GameObject.FindGameObjectWithTag("fireWorkPink").GetComponent<Animator>();
            _control = GameControl.Instance;
            time = _control.GameLength;
        }

        // Update is called once per frame
        void Update()
        {
            if (time >= 0)
            {
                GetComponent<GUIText>().text = time.ToString("0");
                
                time -= Time.deltaTime;
            }
            else
            {
                if (_control.Player1Score > _control.Player2Score)
                {
                    fireWorkRed.SetBool("Win", true);
                    GetComponent<GUIText>().text = "Player 1 WINS!!!";
                }
                else if (_control.Player2Score > _control.Player1Score)
                {
                    fireWorkBlue.SetBool("Win", true);
                    GetComponent<GUIText>().text = "Player 2 WINS!!!";
                }
                else
                {
                    GetComponent<GUIText>().text = "IT'S A DRAW!!!";
                }

                endTime += Time.deltaTime;

                if (endTime > 5) {
                    _control.Reset();
                    SceneManager.LoadScene("PressStart");
                }

                //StartCoroutine(Reload());
            }

        }

        IEnumerator Reload()
        {
            yield return new WaitForSeconds(5);
            /*
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
            SceneManager.SetActiveScene("Press Start");*/
            SceneManager.LoadScene("PressStart");
        }
    }
}
