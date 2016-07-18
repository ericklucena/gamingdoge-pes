using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform[] players;        // Reference to the player's transform.

        void Awake()
        {
            // Setting up the reference.
            GameObject[] playersGameObjects = GameObject.FindGameObjectsWithTag(TagControl.Player);
            // Setting up the reference.
            players = Array.ConvertAll<GameObject, Transform>(playersGameObjects, x => x.transform);

        }

        void Update()
        {
            transform.position = new Vector3(players[0].position.x, 0, -10);
        }


    }
}