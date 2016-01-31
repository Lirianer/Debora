using UnityEngine;
using System.Collections;

public class TimeHUD : HUD {

    int secondsLeft;

	void Start ()
    {
        secondsLeft = Constants.GAME_TIME;
        GetScoreTextMesh().text = "" + secondsLeft;
        InvokeRepeating("DecreaseSecond", 1, 1);
	}
	

    void DecreaseSecond()
    {
        secondsLeft--;
        GetScoreTextMesh().text = "" + secondsLeft;

        if (secondsLeft == 0) {
            Application.LoadLevel("SandBox");
        }
    }

    
}
