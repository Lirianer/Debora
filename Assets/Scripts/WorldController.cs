using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldController : MonoBehaviour
{
    int numCreatedCharacters = 0;
    int gamePadsAvailable = 0;
    int gamePadsOccupied = 0;
    int keyboardsOccupied = 0;
    int numPlayers;

    void Awake()
    {
        numPlayers = 2;

        List<string> gamePadsNames = new List<string>(Input.GetJoystickNames());
        gamePadsNames.RemoveAll(x => x == string.Empty);
        gamePadsAvailable = gamePadsNames.Count;

        for (int i = 1; i <= numPlayers; i++) {
            GameObject playerGO = GameObject.Find("Player" + i);
            playerGO.GetComponent<PlayerInput>().SetControlType(GetControlType());

            HUD hud = GetHUD(i);
            playerGO.GetComponent<PlayerController>().SetHUD(hud);
        }
        
    }


    static public HUD GetHUD(int num)
    {
        return GameObject.Find("HUD/HUD" + num).GetComponent<HUD>();
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
            else if (gamePadsOccupied == 2) {
                ret = Constants.GAMEPAD_3;
            }
            else if (gamePadsOccupied == 3) {
                ret = Constants.GAMEPAD_4;
            }
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
