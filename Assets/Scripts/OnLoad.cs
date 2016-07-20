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


        void Awake()
        {
            Instantiate(Player, BUILD_POSITION_LEFT, Quaternion.Euler(Vector3.zero));
            GameObject Player2 = Instantiate(Player, BUILD_POSITION_RIGHT, Quaternion.Euler(Vector3.zero)) as GameObject;
            Player2.GetComponent<PlayerControl>().Flip();

            transform.GetComponent<CameraFollow>().StartCamera();
        }

        void Update()
        {

        }
    }
}