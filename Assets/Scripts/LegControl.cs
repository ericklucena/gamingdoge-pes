using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class LegControl : MonoBehaviour {

        private PlayerControl _player;

        private GameObject _ballOnReach;

        private const float KICK_FORCE = 100f;
        private const float KICK_UP_FORCE = 70f;

        // Commands
        private bool _kick;
        private bool _kickUp;
        private bool _destroy;

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
            ManageDestroy();
        }


        // Methods

        public void Kick()
        {
            _kick = true;
        }

        public void KickUp()
        {
            _kickUp = true;
        }

        public void Destroy()
        {
            _destroy = true;
        }

        private void ManageKick()
        {
            if (_kick || _kickUp)
            {
                if (_ballOnReach != null)
                {
                    Rigidbody2D rb2 = Getter.GetRigibody2D(_ballOnReach);
                    rb2.AddForce(new Vector2(_kick?KICK_FORCE*_player.Direction:0f, _kickUp?KICK_UP_FORCE:0f));
                }
                _kick = _kickUp = false;
            }
        }

        private void ManageDestroy()
        {
            if (_destroy)
            {
                if (_ballOnReach)
                {
                    Destroy(_ballOnReach);
                    _ballOnReach = null;
                }
            }

            _destroy = false;
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