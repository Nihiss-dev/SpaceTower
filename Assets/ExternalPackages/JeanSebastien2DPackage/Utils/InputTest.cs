using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private bool osWin;

    void Awake()
    {
        Debug.Log("hello");
        Debug.Log(Application.platform);
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("I am on Windows!");
            osWin = true;
        }
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            osWin = false;
            Debug.Log("I am on Mac!");
        }
    }

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
        //####################################################################################
        //Input if on Windows
        //#####################################################################################
        if (osWin)
	    {
            //Test input Buttons
            if (Input.GetButtonDown("ButtonA"))
            {
                Debug.Log("input Button A");
            }

            if (Input.GetButtonDown("ButtonB"))
            {
                Debug.Log("input Button B");
            }

            if (Input.GetButtonDown("ButtonX"))
            {
                Debug.Log("input Button X");
            }

            if (Input.GetButtonDown("ButtonY"))
            {
                Debug.Log("input Button Y");
            }

            if (Input.GetButtonDown("LBumper"))
            {
                Debug.Log("input Left Bumper");
            }

            if (Input.GetButtonDown("RBumper"))
            {
                Debug.Log("input Right Bumper");
            }

            if (Input.GetButtonDown("LSClick"))
            {
                Debug.Log("input Left Stick Click");
            }

            if (Input.GetButtonDown("RSClick"))
            {
                Debug.Log("input Right Stick Click");
            }

            if (Input.GetButtonDown("Start"))
            {
                Debug.Log("input Start");
            }

            if (Input.GetButtonDown("Back"))
            {
                Debug.Log("input Back");
            }

            //Test input axes

            if (Input.GetAxis("LHorizontal") < 0)
            {
                Debug.Log("inputLeft Horizontal moins");
            }

            if (Input.GetAxis("LHorizontal") > 0)
            {
                Debug.Log("inputLeft Horizontal plus");
            }

            if (Input.GetAxis("LVertical") < 0)
            {
                Debug.Log("inputLeft Vertical moins");
            }

            if (Input.GetAxis("LVertical") > 0)
            {
                Debug.Log("inputLeft Vertical plus");
            }

            if (Input.GetAxis("RHorizontal") < 0)
            {
                Debug.Log("input Right Horizontal moins");
            }

            if (Input.GetAxis("RHorizontal") > 0)
            {
                Debug.Log("input Right Horizontal plus");
            }

            if (Input.GetAxis("RVertical") < 0)
            {
                Debug.Log("input Right Vertical moins");
            }

            if (Input.GetAxis("RVertical") > 0)
            {
                Debug.Log("input Right Vertical plus");
            }

            if (Input.GetAxis("DPHorizontal") < 0)
            {
                Debug.Log("input D-Pad Horizontal moins");
            }

            if (Input.GetAxis("DPHorizontal") > 0)
            {
                Debug.Log("input D-Pad Horizontal plus");
            }

            if (Input.GetAxis("DPVertical") < 0)
            {
                Debug.Log("input D-Pad Vertical moins");
            }

            if (Input.GetAxis("DPVertical") > 0)
            {
                Debug.Log("input D-Pad Vertical plus");
            }

            if (Input.GetAxis("RTrigger") > 0)
            {
                Debug.Log("input Right Trigger");
            }

            if (Input.GetAxis("LTrigger") > 0)
            {
                Debug.Log("input Left Trigger");
            }
        }
        //####################################################################################
        //Input if on MAC
        //#####################################################################################
        else
        {
            //Test input Buttons
            if (Input.GetButtonDown("ButtonA_Mac"))
            {
                Debug.Log("input Button A");
            }

            if (Input.GetButtonDown("ButtonB_Mac"))
            {
                Debug.Log("input Button B");
            }

            if (Input.GetButtonDown("ButtonX_Mac"))
            {
                Debug.Log("input Button X");
            }

            if (Input.GetButtonDown("ButtonY_Mac"))
            {
                Debug.Log("input Button Y");
            }

            if (Input.GetButtonDown("LBumper_Mac"))
            {
                Debug.Log("input Left Bumper");
            }

            if (Input.GetButtonDown("RBumper_Mac"))
            {
                Debug.Log("input Right Bumper");
            }

            /*
            if (Input.GetButtonDown("LSClick_Mac"))
            {
                Debug.Log("input Left Stick Click");
            }

            if (Input.GetButtonDown("RSClick_Mac"))
            {
                Debug.Log("input Right Stick Click");
            }
            

            if (Input.GetButtonDown("Start_Mac"))
            {
                Debug.Log("input Start");
            }

            if (Input.GetButtonDown("Back_Mac"))
            {
                Debug.Log("input Back");
            }
            */

			if (Input.GetButtonDown("DPRight_Mac"))
{
                Debug.Log("input D-Pad Right");
            }

			if (Input.GetButtonDown("DPLeft_Mac"))
            {
                Debug.Log("input D-Pad Left");
            }

			if (Input.GetButtonDown("DPUp_Mac"))
            {
                Debug.Log("input D-Pad Up");
            }

			if (Input.GetButtonDown("DPDown_Mac"))
            {
                Debug.Log("input D-Pad Down");
            }

            //Test input axes

            if (Input.GetAxis("LHorizontal") < 0)
            {
                Debug.Log("inputLeft Horizontal moins");
            }

            if (Input.GetAxis("LHorizontal") > 0)
            {
                Debug.Log("inputLeft Horizontal plus");
            }

            if (Input.GetAxis("LVertical") < 0)
            {
                Debug.Log("inputLeft Vertical moins");
            }

            if (Input.GetAxis("LVertical") > 0)
            {
                Debug.Log("inputLeft Vertical plus");
            }

            if (Input.GetAxis("RHorizontal_Mac") < 0)
            {
                Debug.Log("input Right Horizontal moins");
            }

            if (Input.GetAxis("RHorizontal_Mac") > 0)
            {
                Debug.Log("input Right Horizontal plus");
            }

            if (Input.GetAxis("RVertical_Mac") < 0)
            {
                Debug.Log("input Right Vertical moins");
            }

            if (Input.GetAxis("RVertical_Mac") > 0)
            {
                Debug.Log("input Right Vertical plus");
            }

            if (Input.GetAxis("RTrigger_Mac") > 0)
            {
                Debug.Log("input Right Trigger");
            }

            if (Input.GetAxis("LTrigger_Mac") > 0)
            {
                Debug.Log("input Left Trigger");
            }
        }

    }
}
