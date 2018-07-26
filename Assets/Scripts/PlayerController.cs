using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts;

[System.Serializable]




public class PlayerController : MonoBehaviour
{
	public Single VMoveSpeed;
	public Single HMoveSpeed;
	public Single ExtraSpeed;
	public Single SlowSpeed;
	public Single StandardSpeed;
	public Single CurrentSpeed { get; private set; }

	public GameObject RapidFireGun;
	public GameObject SingleFireLeftGun;
	public GameObject SingleFireRightGun;

	public Rigidbody BulletSingle;
	public Rigidbody BulletRapid;
	public Single RapidFireRate;
	public Single SingleFireRate;

	public GameObject CrossHair;

	public Single TargettingRange;

	private float RapidNextFire;
	private float SingleNextFire;

	private Single FastSpeed { get; set; }
	private Single ForwardSpeed { get; set; }

	private Single MoveH { get; set; }
	private Single MoveV { get; set; }

	private GameObject ReachedCheckPoint { get; set; }

	private int LayerMask { get; set; }

	private Rigidbody RB { get; set; }

	public enum SpeedModeEnum
	{
		Slow,
		Standard,
		Fast
	}

	public SpeedModeEnum SpeedMode { get; private set; }

	void Start()
	{
		StateManager.Player = gameObject;
		RB = gameObject.GetComponent<Rigidbody>();
		FastSpeed = ForwardSpeed + ExtraSpeed;
		StateManager.WeaponEnergy = 100;
		EnemyTargeted = 0;
		LayerMask = 1 << 8;
		InvokeRepeating("RegenerateAmmo", 0, 1);

	}

	private int EnemyTargeted { get; set; }

	void FixedUpdate()
	{
		Move();

		Fire();

		Target();
	}

	private void Target()
	{
		RaycastHit hit;
		Vector3 max_ch_position = transform.position + Vector3.forward.normalized * TargettingRange;
		if (Physics.Raycast(transform.position, Vector3.forward, out hit, TargettingRange, LayerMask))
		{
			if (hit.collider.gameObject.tag == "Enemy")
			{
				EnemyTargeted = hit.collider.gameObject.GetInstanceID();
				//print("ENEMY TARGETED ID : " + EnemyTargeted + " NAME : " + hit.collider.gameObject.name);
				SetEnemyLock(EnemyTargeted, true);
				CrossHair.transform.position = hit.collider.gameObject.transform.position;
			}
			else
			{
				//print("DETECTING : " + hit.collider.gameObject.tag + " " + hit.collider.gameObject.name);
				//print("LOST TARGET : " + EnemyTargeted);
				SetEnemyLock(EnemyTargeted, false);
				EnemyTargeted = 0;
				CrossHair.transform.position = max_ch_position;
				//print("RAY AT : " + hit.collider.gameObject.name);
			}
		}
		else
		{
			//print("NO RAYCAST HIT");
			SetEnemyLock(EnemyTargeted, false);
			EnemyTargeted = 0;
			CrossHair.transform.position = max_ch_position;
		}
	}

	private void SetEnemyLock(int id, bool has_lock)
	{
		if (id != 0)
		{
			EnemyBrain eb;
			if (StateManager.Enemies.TryGetValue(id, out eb))
			{
				eb.IsTargeted = has_lock;
			}
		}
	}

	private void Fire()
	{
		if (Controls.RightTrigger && Time.time > RapidNextFire && StateManager.WeaponEnergy > 0)
		{
			RapidNextFire = Time.time + RapidFireRate;
			FireRapid();
			StateManager.WeaponEnergy -= 2f;
		}
		if (Controls.Button5 && Time.time > SingleNextFire)
		{
			SingleNextFire = Time.time + SingleFireRate;
			FireSingle();
		}
	}

	private Quaternion targetRotation;

	private void Move()
	{
		SetMovement();

		//Vector3 movement = new Vector3(MoveH * StrafeSpeed, MoveV * UpTilt, ForwardSpeed);
		//rb.velocity = movement;

		//transform.position = new Vector3(Mathf.Clamp(transform.position.x, Minposx, Maxposx), Mathf.Clamp(transform.position.y, Minposy, Maxposy), transform.position.z);

		SetSpeed();

		transform.Rotate(
			Controls.LeftJoystickYMovement * Time.deltaTime * VMoveSpeed,
			Controls.LeftJoystickXMovement * Time.deltaTime * HMoveSpeed,
			0,
			Space.Self);


		//Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rb.velocity.x * -Tilt);
		//transform.rotation = new Quaternion(0, Controls.LeftJoystickXMovement * 20, 0, 0);

		//Single move = MoveH * HMoveSpeed;
		//Single rot_y = transform.rotation.y;
		//Single angle = move + rot_y;

		//print(string.Format("MOVE : {0} | ROT Y : {1} | ANGLE : {2}", move, rot_y, angle));

		//if (MoveH != 0)
		//{
		//	Quaternion wanted_rotation = Quaternion.Euler(0, transform.rotation.y + (MoveH * HMoveSpeed), 0);
		//	transform.rotation = new Quaternion(0, wanted_rotation.y, 0, 0);
		//	//Quaternion.Lerp(transform.rotation, wanted_rotation, Time.deltaTime * 10);
		//}

		RB.velocity = transform.forward * ForwardSpeed;

		ResetMovement();
	}
	//for up tilt 
	//rb.velocity.y * -Tilt

	#region Movement

	private void ResetMovement()
	{
		MoveH = 0;
		MoveV = 0;
	}

	private void SetMovement()
	{
		if (Controls.LeftJoystickUP)
		{
			MoveV = 1;
		}
		if (Controls.LeftJoystickDOWN)
		{
			MoveV = -1;
		}
		if (Controls.LeftJoystickLEFT)
		{
			MoveH = -1;
		}
		if (Controls.LeftJoystickRIGHT)
		{
			MoveH = 1;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "CheckPoint")
		{
			ReachedCheckPoint = collider.gameObject;
			//print("CHECKPOINT REACHED : " + ReachedCheckPoint.name);
		}
		else
		{
			//print("TRIGGERED : " + collider.name);
		}
		if (collider.tag == "CamChange")
		{
			if (CameraFollow.ZaxCam == false)
			{
				CameraFollow.ZaxCam = true;
			}
			else
			{
				CameraFollow.ZaxCam = false;
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "PlayerBullet")
		{
			//print("COLLIDED WITH : " + collision.gameObject.name);
			RB.velocity = Vector3.zero;
			RB.angularVelocity = Vector3.zero;
			transform.position = ReachedCheckPoint.transform.position;
			transform.rotation = Quaternion.identity;
			StateManager.Reset();
		}
	}

	private void SetSpeed()
	{
		if (Controls.RightJoystickYMovement == -1)
		{
			ForwardSpeed = CurrentSpeed = StateManager.CurrentPlayerSpeed = FastSpeed;
			SpeedMode = SpeedModeEnum.Fast;
		}
		else if (Controls.RightJoystickYMovement == 1)
		{
			ForwardSpeed = CurrentSpeed = StateManager.CurrentPlayerSpeed = SlowSpeed;
			SpeedMode = SpeedModeEnum.Slow;
		}
		else
		{
			ForwardSpeed = CurrentSpeed = StateManager.CurrentPlayerSpeed = StandardSpeed;
			SpeedMode = SpeedModeEnum.Standard;
		}
	}

	#endregion

	#region Fire

	void FireRapid()
	{
		var shot = Instantiate(BulletRapid, RapidFireGun.transform.position, RapidFireGun.transform.rotation) as Rigidbody;
	}
	void FireSingle()
	{
		var left_shot = Instantiate(BulletSingle, SingleFireLeftGun.transform.position, SingleFireLeftGun.transform.rotation) as Rigidbody;
		var right_shot = Instantiate(BulletSingle, SingleFireRightGun.transform.position, SingleFireRightGun.transform.rotation) as Rigidbody;
	}

	private void RegenerateAmmo()
	{
		if (StateManager.WeaponEnergy < 100)
		{
			StateManager.WeaponEnergy++;
		}
	}

	#endregion

}