using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class PlayerControl : MonoBehaviour
    {

        private int _playerNumber;
        private ControlMapper _control;


        internal LegControl LegControl { get; set; }

        // Vectors

        private Vector3 BUILD_POSITION = new Vector3(0.5f, -0.5f);

        // Forces
        private const float WALK_FORCE = 10f;
        private float JUMP_FORCE = 250f;
        private float BUILD_FORCE = 2f;

        // Commands
        private bool _foward;
        private bool _backward;
        private bool _jump;
        private bool _kick;
        private bool _build;

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
                _foward = true;
            else if (h < 0)
                _backward = true;

            if (Input.GetButtonDown(_control.Fire))
                _kick = true;

            if (Input.GetButtonDown(_control.Jump))
                _jump = true;

            if (Input.GetButtonDown(_control.Build))
                _build = true;

        }

        private void ManageHorizontalMovements()
        {
            if (_foward)
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * WALK_FORCE);
            else if (_backward)
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * WALK_FORCE);

            _foward = _backward = false;
        }

        private void ManageVerticalMovements()
        {
            if (_jump)
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * JUMP_FORCE);

            _jump = false;
        }

        private void ManageKickMovement()
        {
            if (_kick)
                LegControl.Kick();

            _kick = false;
        }

        private void ManageBuildMovement()
        {
            if (_build)
            {
                GameObject ballInstance = Instantiate(Ball, transform.position + BUILD_POSITION, Quaternion.Euler(Vector3.zero)) as GameObject;
                Rigidbody2D rb2 = Getter.GetRigibody2D(ballInstance);
                rb2.velocity = new Vector2(0, BUILD_FORCE);
            }

            _build = false;
        }
    }
}
