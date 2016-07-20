using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class GoalControl : MonoBehaviour {

        public int scoresTo;

        private GameControl _control;

        // Use this for initialization
        void Start() {
            _control = GameControl.Instance;
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
            if (collider.tag == TagControl.Ball)
            {
                _control.GoalScored(scoresTo);
                Destroy(collider.gameObject, 2);
            }
        }
    }
}