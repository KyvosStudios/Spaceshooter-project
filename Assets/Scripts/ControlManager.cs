using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{

	void Start()
	{

	}

	void Update()
	{
		SetLeftJoystickX();
		SetLeftJoystickY();
		SetRightJoystickX();
		SetRightJoystickY();
		SetLeftTrigger();
		SetRightTrigger();
		SetButton0();
		SetButton1();
		SetButton2();
		SetButton3();
		SetButton4();
		SetButton5();

		//print("LEFT X : " + Controls.LeftJoystickXMovement.ToString());
		//print("LEFT Y : " + Controls.LeftJoystickYMovement.ToString());
		//print("RIGHT X : " + Controls.RightJoystickXMovement.ToString());
		//print("RIGHT Y : " + Controls.RightJoystickYMovement.ToString());
	}

	private void SetLeftJoystickX()
	{
		Controls.LeftJoystickXMovement = Input.GetAxis("LeftJoystickX");
	}

	private void SetLeftJoystickY()
	{
		Controls.LeftJoystickYMovement = Input.GetAxis("LeftJoystickY");
	}

	private void SetRightJoystickX()
	{
		Controls.RightJoystickXMovement = Input.GetAxis("RightJoystickX");
	}

	private void SetRightJoystickY()
	{
		Controls.RightJoystickYMovement = Input.GetAxis("RightJoystickY");
	}

	private void SetLeftTrigger()
	{
		Controls.LeftTrigger = Input.GetAxis("LeftTrigger") != 0;
	}

	private void SetRightTrigger()
	{
		Controls.RightTrigger = Input.GetAxis("RightTrigger") != 0;
	}

	private static void SetButton0()
	{
		Controls.Button0 = Input.GetButton("Button0");
	}

	private static void SetButton1()
	{
		Controls.Button1 = Input.GetButton("Button1");
	}

	private static void SetButton2()
	{
		Controls.Button2 = Input.GetButton("Button2");
	}

	private static void SetButton3()
	{
		Controls.Button3 = Input.GetButton("Button3");
	}
	private static void SetButton4()
	{
		Controls.Button4 = Input.GetButton("Button4");
	}
	private static void SetButton5()
	{
		Controls.Button5 = Input.GetButton("Button5");
	}

}

public static class Controls
{

	public static bool LeftJoystickUP
	{
		get
		{
			return LeftJoystickYMovement < 0;
		}
	}

	public static bool LeftJoystickDOWN
	{
		get
		{
			return LeftJoystickYMovement > 0;
		}
	}

	public static bool LeftJoystickLEFT
	{
		get
		{
			return LeftJoystickXMovement < 0;
		}
	}

	public static bool LeftJoystickRIGHT
	{
		get
		{
			return LeftJoystickXMovement > 0;
		}
	}

	public static bool LeftJoystickX
	{
		get
		{
			return LeftJoystickXMovement != 0;
		}
	}
	public static bool LeftJoystickY
	{
		get
		{
			return LeftJoystickYMovement != 0;
		}
	}
	public static bool RightJoystickX
	{
		get
		{
			return RightJoystickXMovement != 0;
		}
	}
	public static bool RightJoystickY
	{
		get
		{
			return RightJoystickYMovement != 0;
		}
	}
	public static Single LeftJoystickXMovement { get; set; }
	public static Single LeftJoystickYMovement { get; set; }
	public static Single RightJoystickXMovement { get; set; }
	public static Single RightJoystickYMovement { get; set; }
	public static bool LeftTrigger { get; set; }
	public static bool RightTrigger { get; set; }
	public static bool Button0 { get; set; }
	public static bool Button1 { get; set; }
	public static bool Button2 { get; set; }
	public static bool Button3 { get; set; }
	public static bool Button4 { get; set; }
	public static bool Button5 { get; set; }
}
