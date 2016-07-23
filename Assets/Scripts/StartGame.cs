using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class StartGame : MonoBehaviour 
{
    private AudioSource _audio;
    private ControlMapper _control;
    [SerializeField]
    private AudioClip startSound;
    private bool _start;
    // Use this for initialization
    void Start () 
    {
        _control = new ControlMapper(1);
        _audio = GetComponent<AudioSource>();
        _start = false;
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetButtonDown(_control.Fire) || _start)
        {
            if(!_start)
                _audio.PlayOneShot(startSound);
            if(_start && !_audio.isPlaying)
                SceneManager.LoadScene("Tutorial");
            _start = true;
        }
    }
}
