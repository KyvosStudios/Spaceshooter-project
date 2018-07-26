using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotBrain : MonoBehaviour
{

	private Rigidbody bullet { get; set; }
	public GameObject Impact;
	public Single BulletSpeed { get; set; }
	void Start()
	{
		bullet = GetComponent<Rigidbody>();
		Destroy(gameObject, 3);
	}

	void Update()
	{
		bullet.velocity = transform.forward * BulletSpeed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "EnemyBullet")
		{
			Destroy(gameObject);
			var impact = Instantiate(Impact, transform.position, transform.rotation) as GameObject;
		}
	}
}
