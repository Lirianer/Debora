using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {


	void Start ()
    {
        Invoke("ResetGame", Constants.END_SCREEN_DURATION);
	}
	
	
    void ResetGame()
    {
        Application.LoadLevel("SandBox");
    }
}
