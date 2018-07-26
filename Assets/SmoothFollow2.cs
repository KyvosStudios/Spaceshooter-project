using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow2 : MonoBehaviour
{

	public Transform target;
	public float distance = 3.0f;
	public float height = 3.0f;
	public float rotation_damping = 10.0f;

	public Single SlowDamping;
	public Single StandardDamping;
	public Single FastDamping;

	Vector3 wanted_position;
	Quaternion wanted_rotation;
	void LateUpdate()
	{
		wanted_position = target.TransformPoint(0, height, -distance);

		transform.position = Vector3.Lerp(transform.position, wanted_position, Time.deltaTime * GetDamping());
		//transform.position = Vector3.Lerp(transform.position, wanted_position, Time.deltaTime * 8);
		
		wanted_rotation = Quaternion.LookRotation(target.position - transform.position, target.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, wanted_rotation, Time.deltaTime * rotation_damping);
		
		print("DISTANCE : " + Vector3.Distance(gameObject.transform.position, StateManager.Player.transform.position));
	}

	private Single GetDamping()
	{
		Single retval = 0;
		switch (StateManager.PlayerController.SpeedMode)
		{
			case PlayerController.SpeedModeEnum.Slow:
				retval = SlowDamping;
				break;
			case PlayerController.SpeedModeEnum.Standard:
				retval = StandardDamping;
				break;
			case PlayerController.SpeedModeEnum.Fast:
				retval = FastDamping;
				break;
			default:
				retval = StandardDamping;
				break;
		}
		return retval;
	}

}
