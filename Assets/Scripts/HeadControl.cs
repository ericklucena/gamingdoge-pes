using UnityEngine;
using System.Collections;
namespace Assets.Scripts 
{
    public class HeadControl : MonoBehaviour 
    {

        private PlayerControl _player;
        private GameObject _ballOnReach;

        private const float _HEADBUTT_FORCE = 80f;

        // Commands
        private bool _headButt;
        // Use this for initialization
        void Start() 
        {
            _player = transform.parent.GetComponent<PlayerControl>();
            _player.HeadControl = this;
        }

        // Update is called once per frame
        void Update() 
        {

        }

        // Update is called once per frame
        void FixedUpdate() 
        {
            ManageHeadButt();
        }

        // Methods

        public void HeadButt() 
        {
            _headButt = true;
        }

        private void ManageHeadButt() 
        {
            if (_ballOnReach != null && _headButt) 
            {
                Rigidbody2D rb2 = Getter.GetRigibody2D(_ballOnReach);
                //normalize kick force and distribute between X and Y
                float deltaX = Mathf.Abs(_player.HeadPosition.x - _ballOnReach.transform.position.x);
                float deltaY = Mathf.Abs(_player.HeadPosition.y - _ballOnReach.transform.position.y);
                rb2.velocity = new Vector2(rb2.velocity.x, 0f);
                rb2.AddForce(new Vector2(_HEADBUTT_FORCE * _player.Direction * (deltaX * 2 / (deltaX + deltaY)), _HEADBUTT_FORCE * (deltaY * 2 / (deltaX + deltaY))));
            }
            _headButt = false;
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