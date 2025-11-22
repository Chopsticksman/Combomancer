using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public enum ButtonType {
	Left,
	Right
}

public class ButtonManager : MonoBehaviour
{

	InputAction leftButton;
	InputAction rightButton;
	bool leftHeld;
	bool rightHeld;
	int ticks;
	public List<ButtonType> pressedSequence;
	[SerializeField] ListButtonsUI listButtonsUI;

    void Awake()
    {
		leftButton = InputSystem.actions.FindAction("LeftButton");
		rightButton = InputSystem.actions.FindAction("RightButton");
		leftHeld = false;
		rightHeld = false;
		ticks = 0;
		pressedSequence = new List<ButtonType>();
    }

    // Update is called once per frame
    void Update()
    {
		if(ticks++ >= 2) 
		{
			bool leftPressed = leftButton.IsPressed();
			bool rightPressed = rightButton.IsPressed();
			if(leftPressed && rightPressed && !leftHeld && !rightHeld) {
				Debug.Log("both pressed");
				leftHeld = true;
				rightHeld = true;
				pressedSequence = new List<ButtonType>();
				listButtonsUI.DeleteAll();
			}
			else if (leftPressed && !leftHeld) 
			{
				pressedSequence.Add(ButtonType.Left);
				Debug.Log("leftButton");
				leftHeld = true;
				listButtonsUI.DrawLeft();
			} 
			else if (rightPressed && !rightHeld) 
			{
				pressedSequence.Add(ButtonType.Right);
				Debug.Log(pressedSequence.ToString());
				rightHeld = true;
				listButtonsUI.DrawRight();
			}
			else if (!leftPressed && !rightPressed)
			{
				leftHeld = false;
				rightHeld = false;
			}
			ticks = 0;
		}
    }
}
