﻿using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts
{
    public class PlayerControl : MonoBehaviour
    {

        private int _playerNumber;
        private ControlMapper _control;

        // Elements
        internal LegControl LegControl { get; set; }
        internal FeetControl FeetControl { get; set; }

        // Properties
        public float Direction { get { return _facingRight ? 1f : -1f; } }
        [SerializeField]
        public bool Grounded { get { return FeetControl.Grounded; } }
        private Vector3 BuildPosition { get { return _facingRight ? BUILD_POSITION_RIGHT : BUILD_POSITION_LEFT; } }

        // Vectors
        private Vector3 BUILD_POSITION_RIGHT = new Vector3(0.5f, -0.5f);
        private Vector3 BUILD_POSITION_LEFT = new Vector3(-0.5f, -0.5f);

        // Forces
        private const float WALK_FORCE = 10f;
        private float JUMP_FORCE = 250f;
        private float BUILD_FORCE = 2f;

        // Limits
        private float HORIZONTAL_WALK_VELOCITY = 2.5f;
        private float HORIZONTAL_DASH_VELOCITY = 5f;

        // Commands
        private bool _foward;
        private bool _backward;
        private bool _jump;
        private bool _kick;
        private bool _dash;
        private bool _build;
        public bool _up;

        // States
        private bool _facingRight = true;

        // Prefabs
        public GameObject Ball;

        // Use this for initialization
        void Start()
        {
            _playerNumber = GameControl.Instance.NewPlayer();
            _control = new ControlMapper(_playerNumber);
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
                _kick = true;

            if (Input.GetButtonDown(_control.Dash))
                _dash = true;
            if (Input.GetButtonUp(_control.Dash))
                _dash = false;

            if (Input.GetButtonDown(_control.Jump))
                _jump = true;

            if (Input.GetButtonDown(_control.Build))
                _build = true;

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
            float velocity = _dash ? HORIZONTAL_DASH_VELOCITY : HORIZONTAL_WALK_VELOCITY;

            if (_foward)
                GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, GetComponent<Rigidbody2D>().velocity.y);
            else if (_backward)
                GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * velocity, GetComponent<Rigidbody2D>().velocity.y);
            else if (Grounded)
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

            _foward = _backward = false;
        }

        private void ManageVerticalMovements()
        {
            if (_jump && Grounded)
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * JUMP_FORCE);

            _jump = false;
        }

        private void ManageKickMovement()
        {
            if (_kick)
            {
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

        private void ManageBuildMovement()
        {
            if (_build)
            {
                GameObject ballInstance = Instantiate(Ball, transform.position + BuildPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
                Rigidbody2D rb2 = Getter.GetRigibody2D(ballInstance);
                rb2.velocity = new Vector2(0, BUILD_FORCE);
            }

            _build = false;
        }

        void Flip()
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
