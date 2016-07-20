using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class OnLoad : MonoBehaviour
    {
        // Prefabs
        public GameObject Player;

        private Vector3 BUILD_POSITION_LEFT = new Vector3(-3.478f, -4.328f);
        private Vector3 BUILD_POSITION_RIGHT = new Vector3(5.111f, -4.328f);


        void Awake()
        {
            GameObject Player2 = Instantiate(Player, BUILD_POSITION_RIGHT, Quaternion.Euler(Vector3.zero)) as GameObject;
            Player2.GetComponent<PlayerControl>();
            
        }

        void Update()
        {

        }
    }
}