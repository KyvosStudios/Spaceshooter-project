using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBrain : MonoBehaviour
{

	public Rigidbody Shot;

	public Single FireRate;

	public bool Random;

	public Single FromRandomFire;

	public Single ToRandomFire;

	private Single FireNext { get; set; }

	public bool Activated { private get; set;}

	public Single BulletSpeed;

	private GameObject Player { get; set; }
	
	void Start()
	{
		Player = GameObject.Find("Player");
	}
	
	public void ActivateWeapons()
	{
		Activated = true;
		//print("WEAPON IS ACTIVE");
	}

	public void DeactivateWeapons()
	{
		Activated = false;
		//print("WEAPON IS NOT ACTIVE");
	}

	void Update()
	{
		if (Activated && Time.time > FireNext)
		{
			Rigidbody shot = Instantiate(Shot, transform.position, transform.rotation) as Rigidbody;
			shot.gameObject.GetComponent<EnemyShotBrain>().BulletSpeed = BulletSpeed;
			//print("FIRED");
			if (!Random)
			{
				FireNext = Time.time + FireRate;
			}
			else
			{
				Single next = UnityEngine.Random.RandomRange(FromRandomFire, ToRandomFire);
				FireNext = Time.time + next;
			}
		}
	}
}
