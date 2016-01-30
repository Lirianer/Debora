using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
    string controlType;
    string xAxisControlName;
    string yAxisControlName;
    string actionButton;


    void Start () 
	{
		SetInputType ();
	}
	

    public void SetControlType(string controlType)
    {
        this.controlType = controlType;
    }


    void Update()
    {
        if (Input.GetAxis(actionButton) > 0) {
            GetComponent<PlayerController>().OnActionButton();
        }
    }

	
	public float GetHorizontalAxis()
	{
        if (xAxisControlName == null) {
            throw new UnityException("xAxisControlName is null");
        }

		return Input.GetAxis(xAxisControlName);
    }
	
	
	public float GetVerticalAxis()
	{
		return Input.GetAxis(yAxisControlName);
	}


    void SetInputType()
    {
        switch (controlType) {
            case Constants.KEYBOARD_LEFT:
                xAxisControlName = Constants.KEYBOARD1_HORIZONTALAXIS;
                yAxisControlName = Constants.KEYBOARD1_VERTICALAXIS;
                actionButton = Constants.KEYBOARD1_BUTTON_A;
                break;
            case Constants.KEYBOARD_RIGHT:
                xAxisControlName = Constants.KEYBOARD2_HORIZONTALAXIS;
                yAxisControlName = Constants.KEYBOARD2_VERTICALAXIS;
                actionButton = Constants.KEYBOARD2_BUTTON_A;
                break;
            case Constants.GAMEPAD_1:
                xAxisControlName = Constants.GAMEPAD1_HORIZONTALAXIS;
                yAxisControlName = Constants.GAMEPAD1_VERTICALAXIS;
                actionButton = Constants.GAMEPAD1_BUTTON_A;
                break;
            case Constants.GAMEPAD_2:
                xAxisControlName = Constants.GAMEPAD2_HORIZONTALAXIS;
                yAxisControlName = Constants.GAMEPAD2_VERTICALAXIS;
                actionButton = Constants.GAMEPAD2_BUTTON_A;
                break;
        }
    }



}
