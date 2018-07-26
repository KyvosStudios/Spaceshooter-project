using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Assets.Scripts;

public class EnemyBrain : MonoBehaviour, IWeaponized
{
	public Single ActivationDistance;

	public List<string> Animations;

	private Animator Anim { get; set; }

	public Single AnimInterval;

	public bool AimPlayer;

	public int InitialHitPoints;
	public GameObject Explosion;
	private int HitPoints { get; set; }

	private GameObject Player { get; set; }

	private SwarmBrain Swarm { get; set; }

	private List<EnemyWeaponBrain> Weapons { get; set; }

	private GameObject EnemySelf { get; set; }

	private MeshRenderer TargetedMeshRenderer { get; set; }

	public bool IsTargeted { get; set; }

	private bool IsAlive { get; set; }

	void Start()
	{
		SetAnim();

		HitPoints = InitialHitPoints;
		IsAlive = true;

		//print("MY NAME IS : " + gameObject.name);

		Player = GameObject.Find("Player");
		//Weapons = GetChildObjects(); //wtf fix
		InitRelationships();
		if (EnemySelf != null)
		{
			StateManager.Enemies.Add(EnemySelf.GetInstanceID(), this);
		}

		//StartAnimationSequence();
		//print ("ANIMATIONS : " + Animations.Count.ToString());
	}

	private void InitRelationships()
	{
		Weapons = gameObject.GetComponentsInChildren<EnemyWeaponBrain>().ToList();
		TargetedMeshRenderer = (from item in gameObject.GetComponentsInChildren<MeshRenderer>()
								where item.tag == "HasTargetedMR"
								select item).FirstOrDefault();

		EnemySelf = (from item in gameObject.GetComponentsInParent<Transform>()
					 where item.gameObject.tag == "Enemy"
					 select item.gameObject).FirstOrDefault();

		Swarm = (from item in gameObject.GetComponentsInParent<SwarmBrain>()
				 select item).FirstOrDefault();

		//print("HAS MR : " + (TargetedMeshRenderer != null).ToString() + " PARENT IS : " + TargetedMeshRenderer.transform.parent.gameObject.name);
		//if (TargetedMeshRenderer != null)
		//{
		//	print(TargetedMeshRenderer.gameObject.name);
		//	TargetedMeshGameObject = TargetedMeshRenderer.transform.parent.gameObject;
		//}

	}

	private void SetAnim()
	{
		Transform t = transform.Find("AnimationHolder");
		if (t != null)
		{
			Anim = t.GetComponent<Animator>();
		}
	}

	public void StartAnimationSequence()
	{
		InvokeRepeating("PlayAnimations", 0.0f, AnimInterval);
	}

	public bool AnimationStarted { get; set; }
	private bool WeaponsActivated { get; set; }
	void Update()
	{
		if (IsAlive)
		{
			Single distance = gameObject.transform.position.z - Player.transform.position.z;

			if (EnemySelf != null)
			{
				TargetedMeshRenderer.enabled = IsTargeted;
			}

			ProcessAnimationAndWeaponsActivation(distance);
		}
	}

	private void ProcessAnimationAndWeaponsActivation(Single distance)
	{
		if (distance < ActivationDistance && distance > 0)
		{
			if (AimPlayer)
			{
				transform.LookAt(Player.transform);
			}
			if (Anim != null)
			{
				if (!AnimationStarted)
				{
					AnimationStarted = true;
					StartAnimationSequence();
					ActivateWeapons();
				}
			}
			else if (!WeaponsActivated)
			{
				//print("WEAPONS ACTIVATED");
				ActivateWeapons();
			}
		}
		else
		{
			DeactivateWeapons();
		}
	}

	private int animation_index { get; set; }
	private void PlayAnimations()
	{
		if (Anim != null)
		{
			//print("PA CALLED IDX = " + animation_index + " FOR : " + gameObject.name);
			animation_index =
				AnimationIndexNotReachedEnd()
				? animation_index += 1
				: 0;

			DisableOtherAnimations();

			Anim.SetBool(Animations[animation_index], true);
			//print("IDX = " + animation_index + " FOR : " + gameObject.name);
		}
	}

	private void DisableOtherAnimations()
	{
		List<string> anims_to_disable = (from item in Animations
										 where item != Animations[animation_index]
										 select item).Distinct().ToList();
		anims_to_disable.ForEach(a => Anim.SetBool(a, false));
	}

	private void DisableAllAnimations()
	{
		Animations.ForEach(a => Anim.SetBool(a, false));
	}

	public void Reset()
	{
		gameObject.SetActive(true);
		//reset anims
		if (Anim != null)
		{
			CancelInvoke();
			Anim.StopPlayback();
			AnimationStarted = false;


			DisableAllAnimations();
			Anim.SetBool("Idle", true);
			
			animation_index = 0;
		}
		//reset weapons
		DeactivateWeapons();
		HitPoints = InitialHitPoints;
		IsAlive = true;
		//print("I HAVE BEEN RESET");
	}

	private bool AnimationIndexNotReachedEnd()
	{
		return animation_index < Animations.Count - 1;
	}

	public void ActivateWeapons()
	{
		WeaponsActivated = true;
		Weapons.ForEach(it => it.ActivateWeapons());
	}

	public void DeactivateWeapons()
	{
		WeaponsActivated = false;
		Weapons.ForEach(it => it.DeactivateWeapons());
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "PlayerBullet")
		{
			print(collision.gameObject.name);
			HitPoints--;
			print("HP : " + HitPoints);
			if (HitPoints == 0)
			{
				IsAlive = false;
				Swarm.IDied(this);
				DisableAllAnimations();
				Anim.gameObject.transform.position = new Vector3(0, 0, 0);
				Anim.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

				var explosion = Instantiate(Explosion, transform.position, transform.rotation);
				DeactivateWeapons();
				if (TargetedMeshRenderer != null)
				{
					TargetedMeshRenderer.enabled = false;
				}
				print("I DIED");
				gameObject.SetActive(false);
			}
		}
	}
}
