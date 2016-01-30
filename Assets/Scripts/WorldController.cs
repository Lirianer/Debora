using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldController : MonoBehaviour
{
    int numCreatedCharacters = 0;
    int gamePadsAvailable = 0;
    int gamePadsOccupied = 0;
    int keyboardsOccupied = 0;

    void Awake ()
    {
        List<string> gamePadsNames = new List<string>(Input.GetJoystickNames());
        gamePadsNames.RemoveAll(x => x == string.Empty);
        gamePadsAvailable = gamePadsNames.Count;

        GameObject.Find("Player1").GetComponent<PlayerInput>().SetControlType(GetControlType());
        GameObject.Find("Player2").GetComponent<PlayerInput>().SetControlType(GetControlType());
  
    }


    string GetControlType()
    {
        string ret = "unavailable";
        if (gamePadsAvailable > 0 && gamePadsAvailable > gamePadsOccupied) {
            if (gamePadsOccupied == 0) {
                ret = Constants.GAMEPAD_1;
            }
            else if (gamePadsOccupied == 1) {
                ret = Constants.GAMEPAD_2;
            }
            Debug.Log(ret);
            gamePadsOccupied++;
        }
        else {
            if (keyboardsOccupied == 2) {
                return "unavailable";
            }
            if (keyboardsOccupied == 0) {
                ret = Constants.KEYBOARD_LEFT;
            }
            else {
                ret = Constants.KEYBOARD_RIGHT;
            }
            keyboardsOccupied++;
        }

        return ret;
    }
}
