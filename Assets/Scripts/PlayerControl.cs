using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts
{
    public class PlayerControl : MonoBehaviour
    {

        private int _playerNumber;
        private ControlMapper _control;

        // Elements
        internal HeadControl HeadControl { get; set; }
        internal LegControl LegControl { get; set; }
        internal FeetControl FeetControl { get; set; }

        // Public Properties
        public float Direction { get { return _facingRight ? 1f : -1f; } }
        public bool Grounded { get { return FeetControl.Grounded; } }
        public bool OnSpawnArea { get; set; }
        public Vector3 KickPosition { get { return transform.position + (_facingRight?_KICK_POSITION_RIGHT:_KICK_POSITION_LEFT); } }
        public Vector3 HeadPosition { get { return transform.position + _HEAD_POSITION; } }

        // Private Properties
        private Vector3 _BuildPosition { get { return _facingRight ? _BUILD_POSITION_RIGHT : _BUILD_POSITION_LEFT; } }

        // Vectors
        private Vector3 _BUILD_POSITION_RIGHT = new Vector3(0.35f, -0.5f);
        private Vector3 _BUILD_POSITION_LEFT = new Vector3(-0.35f, -0.5f);
        private Vector3 _KICK_POSITION_RIGHT = new Vector3(0.24f, -0.19f);
        private Vector3 _KICK_POSITION_LEFT = new Vector3(-0.24f, -0.19f);
        private Vector3 _HEAD_POSITION = new Vector3(0f, 0.5f);

        // Forces
        private const float _WALK_FORCE = 10f;
        private float _JUMP_FORCE = 220f;
        private float _BUILD_FORCE = 2f;

        // Limits
        private float _HORIZONTAL_WALK_VELOCITY = 2.5f;
        private float _HORIZONTAL_DASH_VELOCITY = 5f;

        // Commands
        private bool _foward;
        private bool _backward;
        private bool _jump;
        private bool _kick;
        private bool _dash;
        private bool _build;
        public bool _up;
        public bool _destroy;

        // Internal States
        private bool _facingRight = true;
        private bool _jumping = false;

        // Prefabs
        public GameObject Ball;
        private bool _headButt;

        private Animator _anim;
        private AudioSource _source;
        [SerializeField]
        private AudioClip audio;

        // Use this for initialization
        void Start()
        {
            _playerNumber = GameControl.Instance.NewPlayer();
            _control = new ControlMapper(_playerNumber);
            _anim = gameObject.GetComponent<Animator>();
            _anim.SetInteger("Player", _playerNumber);
            _source = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            ManageControlInputs();
        }

        void FixedUpdate()
        {
            ManageHorizontalMovements();
            ManageVerticalMovements();
            ManageKickMovement();
            ManageHeadButt();
            ManageBuildMovement();
        }

        private void ManageControlInputs()
        {
            float h = Input.GetAxis(_control.Horizontal);

            if (h > 0)
            {
                _foward = true;
                if (!_facingRight)
                    Flip();
            }
            else if (h < 0)
            {
                _backward = true;
                if (_facingRight)
                    Flip();
            }

            if (Input.GetButtonDown(_control.Fire))
                _kick = _headButt = true;

            if (Input.GetButtonDown(_control.Dash))
                _dash = true;
            if (Input.GetButtonUp(_control.Dash))
                _dash = false;

            if (Input.GetButtonDown(_control.Jump))
                _jump = true;
            if (Input.GetButtonUp(_control.Jump))
                _jump = false;

            if (Input.GetButtonDown(_control.Build))
                _build = true;

            if (Input.GetButtonDown(_control.Destroy))
                _destroy = true;

            if (Input.GetAxis(_control.Vertical) > 0)
                _up = true;
            else
                _up = false;

            if (Input.GetButtonDown(_control.Start))
                GameControl.Instance.ToogleRunning();

        }

        private void ManageHorizontalMovements()
        {
            Rigidbody2D rb2 = GetComponent<Rigidbody2D>();
            float velocity = _dash ? _HORIZONTAL_DASH_VELOCITY : _HORIZONTAL_WALK_VELOCITY;

            if (_foward) {
                rb2.velocity = new Vector2(velocity, rb2.velocity.y);
                if(Grounded)
                    _anim.SetBool("Running", true);

            } else if (_backward) {
                rb2.velocity = new Vector2(-1 * velocity, rb2.velocity.y);
                if(Grounded)
                    _anim.SetBool("Running", true);

            } else if (Grounded) {
                rb2.velocity = new Vector2(0, rb2.velocity.y);
                _anim.SetBool("Running", false);

            } else {
                _anim.SetBool("Running", false);
            }
            _foward = _backward = false;
        }

        private void ManageVerticalMovements()
        {
            _anim.SetBool("Ground", Grounded);
            Rigidbody2D rb2 = GetComponent<Rigidbody2D>();

            if (_jump && Grounded && !_jumping)
            {
                rb2.AddForce(Vector2.up * _JUMP_FORCE);
                _jumping = true;
                _anim.SetBool("Jump", true);
            }

            if (!_jump && _jumping)
            {
                rb2.velocity = new Vector2(rb2.velocity.x, rb2.velocity.y<0?rb2.velocity.y:0.5f);
                _jumping = false;
            }
            if(rb2.velocity.y < 0)
            {
                _anim.SetBool("Jump", false);
            }

        }

        private void ManageKickMovement()
        {
            if (_kick)
            {
                _anim.SetTrigger("Attack");
                if (_up)
                {
                    LegControl.KickUp();
                }
                else
                {
                    LegControl.Kick();
                }
            }

            _kick = _up = false;
        }

        private void ManageHeadButt() {
            if (_headButt) 
            {
                HeadControl.HeadButt();
            }
            _headButt = false;
        }

        private void ManageBuildMovement()
        {
            if (_build && OnSpawnArea)
            {
                _anim.SetTrigger("Attack");
                GameObject ballInstance = Instantiate(Ball, transform.position + _BuildPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
                Rigidbody2D rb2 = Getter.GetRigibody2D(ballInstance);
                rb2.velocity = new Vector2(0, _BUILD_FORCE);
            }

            if (_destroy)
            {
                _source.PlayOneShot(audio);
                _anim.SetTrigger("Attack");
                LegControl.Destroy();
            }

            _build = false;
            _destroy = false;
        }

        public void Flip()
        {
            // Switch the way the player is labelled as facing.
            _facingRight = !_facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
