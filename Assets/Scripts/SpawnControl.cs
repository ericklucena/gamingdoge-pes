using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class SpawnControl : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == TagControl.Player)
                collider.gameObject.GetComponent<PlayerControl>().OnSpawnArea = true;
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.tag == TagControl.Player)
                collider.gameObject.GetComponent<PlayerControl>().OnSpawnArea = false;
        }
    }
}