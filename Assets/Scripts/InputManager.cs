using System;
using SpaceTower;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected static InputManager instance;
    private bool isOn = true;

    public static InputManager getInstance()
    {
        if (instance == null) {
            instance = (InputManager)FindObjectOfType(typeof(InputManager));

            if (instance == null) {
                Debug.LogError("An instance of " + typeof(InputManager) +
                   " is needed in the scene, but there is none.");
            }
        }

        return instance;
    }

    public bool GetButton(Inputs button, int playerID)
    {
        if (isOn)
        {
            return Input.GetButton(button.ToString() + playerID.ToString());
        }else
        {
            return false;
        }
    }

    public bool GetButtonDown(Inputs button, int playerID)
    {
        if (isOn)
        {
            return Input.GetButtonDown(button.ToString() + playerID.ToString());
        }
        else
        {
            return false;
        }
    }

    public bool GetButtonUp(Inputs button, int playerID)
    {
        if (isOn)
        {
            return Input.GetButtonUp(button.ToString() + playerID.ToString());
        }
        else
        {
            return false;
        }

    }

    public float GetAxisRaw(Inputs input, int playerID)
    {
        if (isOn)
        {
            return Input.GetAxisRaw(input.ToString() + playerID.ToString());
        }
        else
        {
            return 0;
        }
    }

    public float GetAxis(Inputs input, int playerID)
    {
        if (isOn)
        {
            return Input.GetAxis(input.ToString() + playerID.ToString());
        }
        else
        {
            return 0;
        }
    }

    internal void turnOff()
    {
        isOn = false;
    }

    internal void turnOn()
    {
        isOn = true;
    }
}
