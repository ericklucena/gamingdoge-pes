using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
    public class GoalControl : MonoBehaviour {

        public int scoresTo;
        public AudioClip goalSound;
        private GameControl _control;
        private AudioSource source;
        // Use this for initialization
        void Start() {
            _control = GameControl.Instance;
            source = GetComponent<AudioSource>();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == TagControl.Ball)
            {
                source.PlayOneShot(goalSound);

                _control.GoalScored(scoresTo);
                collider.gameObject.GetComponent<Animator>().SetTrigger("destroy");
                Destroy(collider.gameObject, .45f);
            }
        }
    }
}