using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class SkipTutorial : MonoBehaviour {

    private ControlMapper _control;

    // Use this for initialization
    void Start() {
        _control = new ControlMapper(1);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown(_control.Start)) 
        {
            SceneManager.LoadScene("Game");
        }
    }
}
