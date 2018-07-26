using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBrain : MonoBehaviour
{

	// Use this for initialization
	private Rigidbody bullet;
	public GameObject spark;
	public Single Bulletspeed = 20;
	private Single PlayerSpeed { get; set; }
	void Start()
	{
		bullet = GetComponent<Rigidbody>();
		Destroy(gameObject, 3);
	}

	void Update()
	{
		bullet.velocity = transform.forward * (Bulletspeed + StateManager.CurrentPlayerSpeed);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "PlayerBullet")
		{
			Destroy(gameObject);
			var impact = Instantiate(spark, transform.position, transform.rotation) as GameObject;
		}
	}
}
