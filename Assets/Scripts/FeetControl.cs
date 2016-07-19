using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class FeetControl : MonoBehaviour {

        private PlayerControl _player;

        private GameObject _ground;

        public bool Grounded { get { return _ground != null; } }

        // Use this for initialization
        void Start() {
            _player = transform.parent.GetComponent<PlayerControl>();
            _player.FeetControl = this;
        }

        // Update is called once per frame
        void Update() {

        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == TagControl.Ground)
            {
                _ground = collider.gameObject;
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.tag == TagControl.Ground)
            {
                if (_ground == collider.gameObject)
                    _ground = null;
            }
        }
    }
}