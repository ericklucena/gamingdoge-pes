using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class LegControl : MonoBehaviour
    {

        private PlayerControl _player;

        private GameObject _ballOnReach;
        public AudioClip kickSound;
        private const float _KICK_FORCE = 80f;
        private const float _KICK_UP_FORCE = 140f;

        // Commands
        private bool _kick;
        private bool _kickUp;
        private bool _destroy;
        
        private AudioSource source;

        // Use this for initialization
        void Start()
        {
            _player = transform.parent.GetComponent<PlayerControl>();
            _player.LegControl = this;
            source = GetComponent<AudioSource>();
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
            if (_ballOnReach != null)
            {
                Rigidbody2D rb2 = Getter.GetRigibody2D(_ballOnReach);
                if (_kickUp)
                {

                    rb2.AddForce(new Vector2(0f, _KICK_UP_FORCE));
                    source.PlayOneShot(kickSound);
                }
                else if (_kick)
                {
                    //normalize kick force and distribute between X and Y
                    float deltaX = Mathf.Abs(_player.KickPosition.x - _ballOnReach.transform.position.x);
                    float deltaY = Mathf.Abs(_player.KickPosition.y - _ballOnReach.transform.position.y);
                    rb2.velocity = new Vector2(rb2.velocity.x, 0f);
                    rb2.AddForce(new Vector2(_KICK_FORCE * _player.Direction * (deltaX * 2 / (deltaX + deltaY)), _KICK_FORCE * (deltaY * 2 / (deltaX + deltaY))));
                    source.PlayOneShot(kickSound);
                }

                

            }
            _kick = _kickUp = false;
        }

        private void ManageDestroy()
        {
            if (_destroy)
            {
                if (_ballOnReach)
                {
                    _ballOnReach.gameObject.GetComponent<Animator>().SetTrigger("destroy");
                    Destroy(_ballOnReach, .45f);
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