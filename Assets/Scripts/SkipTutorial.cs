using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class SkipTutorial : MonoBehaviour {

    private ControlMapper _control;
    private ControlMapper _secondControl;

    // Use this for initialization
    void Start() {
        _control = new ControlMapper(1);
        _secondControl = new ControlMapper(2);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown(_control.Start) || Input.GetButtonDown(_secondControl.Start)) 
        {
            SceneManager.LoadScene("Game");
        }
    }
}
