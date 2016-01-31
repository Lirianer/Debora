using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    int score = 0;

	void Start ()
    {
        GetScoreTextMesh().text = "" + score;
	}


    TextMesh GetScoreTextMesh()
    {
        return transform.Find("Text").GetComponent<TextMesh>();
    }


    public void IncreaseScore()
    {
        score++;
        GetScoreTextMesh().text = "" + score;
    }
}
