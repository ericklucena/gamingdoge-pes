using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform[] players;        // Reference to the player's transform.
        [SerializeField]
        private float distance;
        [SerializeField]
        private float yPos = 0f;

        public void StartCamera()
        {
            GameObject[] playersGameObjects = playersGameObjects = GameObject.FindGameObjectsWithTag("Player");
            players = Array.ConvertAll<GameObject, Transform>(playersGameObjects, x => x.transform);
        }

        void Update()
        {
            SetCameraPos();
        }

        private void SetCameraPos()
        {
            Vector3 middle = players[0].position;
            for (int i = 1; i < players.Length; i++)
            {
                middle += players[i].position;
            }
            middle /= players.Length;
            transform.position = new Vector3(
                middle.x,
                yPos,
                transform.position.z
            );
        }

        private float GetLessX()
        {
            float x = players[0].position.x;
            for (int i = 1; i < players.Length; i++)
            {
                if (x > players[i].position.x)
                {
                    x = players[i].position.x;
                }
            }
            return x;
        }

        private float GetGreatherX()
        {
            float x = players[0].position.x;
            for (int i = 1; i < players.Length; i++)
            {
                if (x < players[i].position.x)
                {
                    x = players[i].position.x;
                }
            }
            return x;
        }

    }
}