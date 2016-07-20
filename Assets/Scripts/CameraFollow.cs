using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform[] players;        // Reference to the player's transform.
        [SerializeField]
        private float minSizeY;
        [SerializeField]
        private float maxSize;
        [SerializeField]
        private float distance;
        [SerializeField]
        private float yPos = 0f;

        void Awake()
        {
            
        }

        public void StartCamera()
        {
            GameObject[] playersGameObjects = playersGameObjects = GameObject.FindGameObjectsWithTag("Player");
            players = Array.ConvertAll<GameObject, Transform>(playersGameObjects, x => x.transform);
        }

        void Update()
        {
            SetCameraPos();
            SetCameraSize();
        }

        private void SetCameraPos()
        {

            float minSizeX = minSizeY * Screen.width / Screen.height;

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

        private void SetCameraSize()
        {
            //horizontal size is based on actual screen ratio
            float minSizeX = minSizeY;

            float width = (Mathf.Abs(GetGreatherX() - GetLessX()) * 0.5f) + distance;
            //computing the size
            float camSizeX = Mathf.Max(width, minSizeX);
            Camera.main.orthographicSize = Mathf.Max(
                camSizeX * Screen.height / Screen.width, minSizeY);

            if (Camera.main.orthographicSize > maxSize)
                Camera.main.orthographicSize = maxSize;
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