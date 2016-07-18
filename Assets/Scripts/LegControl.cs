using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class LegControl : MonoBehaviour {

        private PlayerControl _player;

        private GameObject _ballOnReach;

        private const float KICK_FORCE = 100f;

        // Commands

        private bool _kick;

        // Use this for initialization
        void Start() {
            _player = transform.parent.GetComponent<PlayerControl>();
            _player.LegControl = this;
        }

        // Update is called once per frame
        void Update() {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ManageKick();
        }


        // Methods

        public void Kick()
        {
            _kick = true;
        }

        private void ManageKick()
        {
            if (_kick)
            {
                _kick = false;
                if (_ballOnReach != null)
                {
                    Rigidbody2D rb2 = Getter.GetRigibody2D(_ballOnReach);
                    rb2.AddForce(new Vector2(0f, KICK_FORCE));
                }

            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == TagControl.Ball)
            {
                _ballOnReach = collider.gameObject;
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.tag == TagControl.Ball)
            {
                if (_ballOnReach == collider.gameObject)
                    _ballOnReach = null;
            }
        }
    }
}