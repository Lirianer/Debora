using UnityEngine;
using System.Collections;

public class TimeHUD : HUD {

    int secondsLeft;
    bool isStopped = false;

	void Start ()
    {
        secondsLeft = Constants.GAME_TIME;
        GetScoreTextMesh().text = "" + secondsLeft;
        InvokeRepeating("DecreaseSecond", 1, 1);
        GetWinTextMesh().text = "";
	}
	

    void DecreaseSecond()
    {
        secondsLeft--;
        GetScoreTextMesh().text = "" + secondsLeft;

        if (secondsLeft == 0) {
            GetComponent<SpriteRenderer>().enabled = false;
            GetScoreTextMesh().text = "";
            
            if (GetWinningPlayerNum() == 1) {
                GetWinTextMesh().text = "PLAYER 1 WINS!!";
            }
            else if (GetWinningPlayerNum() == 2) {
                GetWinTextMesh().text = "PLAYER 2 WINS!!";
            }
            else {
                GetWinTextMesh().text = "DRAW!";
            }

            CancelInvoke("DecreaseSecond");
            Invoke("ResetGame", Constants.WIN_PAUSE_DURATION);
            isStopped = true;
        }
    }


    int GetWinningPlayerNum()
    {
        if (WorldController.GetHUD(1).GetScore() > WorldController.GetHUD(2).GetScore()) {
            return 1;
        }
        else if (WorldController.GetHUD(2).GetScore() > WorldController.GetHUD(1).GetScore()) {
            return 2;
        }
        else {
            return -1; //Draw
        }
    }


    void ResetGame()
    {
        Application.LoadLevel("SandBox");
    }



    TextMesh GetWinTextMesh()
    {
        return transform.Find("WinText").GetComponent<TextMesh>();
    }

}
