using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;

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
    int leftHeldTicks;
    int rightHeldTicks;
	bool casted;
    public List<ButtonType> pressedSequence;
	public List<Tuple<ButtonType, int>> pressedQueue;
	ListButtonsUI listButtonsUI;
    [SerializeField] SpellManager spellManager;

    void Awake()
	{
        GameTick.OnTick += Check;
        GameTick.OnTick += DecreaseQueue;
        leftButton = InputSystem.actions.FindAction("LeftButton");
		rightButton = InputSystem.actions.FindAction("RightButton");
		leftHeld = false;
		rightHeld = false;
        leftHeldTicks = 0;
        rightHeldTicks = 0;
		casted = false;
        pressedSequence = new List<ButtonType>();
		pressedQueue = new List<Tuple<ButtonType, int>>();
        listButtonsUI = ListButtonsUI.instance;

    }

	void DecreaseQueue()
	{
		if (pressedQueue.Count == 0)
		{
			return;
		} 
		else
		{
			for (int i = 0; i < pressedQueue.Count; i++)
			{
				if (pressedQueue[i].Item2 <= 0)
				{
					pressedSequence.Add(pressedQueue[i].Item1);
					if (pressedQueue[i].Item1 == ButtonType.Left) {
                        listButtonsUI.DrawLeft();
                    } else {
						listButtonsUI.DrawRight();
					}
                    pressedQueue.RemoveAt(i);
                } else {
					pressedQueue[i] = new Tuple<ButtonType, int>(pressedQueue[i].Item1, pressedQueue[i].Item2 - 1);
				}
			}
		}
    }

	void Check()
	{
		bool leftPressed = leftButton.IsPressed();
		bool rightPressed = rightButton.IsPressed();
		if (leftHeld)
		{
			leftHeldTicks++;
		}
		if (rightHeld)
		{
			rightHeldTicks++;
        }
		if(leftPressed && rightPressed && leftHeldTicks <= 3 && rightHeldTicks <= 3 && !casted) {
			casted = true;
			leftHeld = true;
			rightHeld = true;
            spellManager.Cast(pressedSequence);
            pressedSequence = new List<ButtonType>();
			pressedQueue = new List<Tuple<ButtonType, int>>();
            listButtonsUI.DeleteAll();
		}
		else if (leftPressed && !leftHeld) 
		{
			leftHeld = true;
            pressedQueue.Add(new Tuple<ButtonType, int>(ButtonType.Left, 2));
        } 
		else if (rightPressed && !rightHeld) 
		{
			rightHeld = true;
            pressedQueue.Add(new Tuple<ButtonType, int>(ButtonType.Right, 2));
        }
		if (!leftPressed) {
			leftHeld = false;
			leftHeldTicks = 0;
			casted = false;
        }
        if (!rightPressed) {
            rightHeld = false;
            rightHeldTicks = 0;
			casted = false;
        }
    }
}
