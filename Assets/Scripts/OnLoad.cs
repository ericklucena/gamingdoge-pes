using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class OnLoad : MonoBehaviour
    {
        // Prefabs
        public GameObject Player;

        private Vector3 BUILD_POSITION_LEFT = new Vector3(-5.0f, -1.628f);
        private Vector3 BUILD_POSITION_RIGHT = new Vector3(5.0f, -1.628f);
        private AudioSource _source;
        public AudioClip shootSound;
        private GameControl _control;

        void Awake()
        {
            Instantiate(Player, BUILD_POSITION_LEFT, Quaternion.Euler(Vector3.zero));
            GameObject Player2 = Instantiate(Player, BUILD_POSITION_RIGHT, Quaternion.Euler(Vector3.zero)) as GameObject;
            Player2.GetComponent<PlayerControl>().Flip();

            transform.GetComponent<CameraFollow>().StartCamera();
            _control = GameControl.Instance;
            _source = GetComponent<AudioSource>();
            //  float vol = Random.Range(0., volHighRange);
            _source.Play();
           
        }
        void Update() {
            if(_source.isPlaying) {
                Time.timeScale = 0;

            } else if(Time.timeScale == 0 && _control.Running) {
                Time.timeScale = 1;
            }

            
        }
    }
}