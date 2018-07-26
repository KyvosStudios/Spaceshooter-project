using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts;

public class SwarmBrain : MonoBehaviour, IResetable
{

	public Single MoveSpeed;
	public Single ActivationDistance;
	private GameObject Player { get; set; }
	private List<EnemyBrain> SwarmMembers { get; set; }

	private Vector3 OriginalPosition { get; set; }

	private bool SwarmIsActivated { get; set; }

	private bool SwarmIsAlive
	{
		get
		{
			return SwarmMembers.Count > DeadSwarmMembersCount;
		}
	}

	public int DeadSwarmMembersCount { get; private set; }

	public void IDied(EnemyBrain eb)
	{
		DeadSwarmMembersCount++;
	}

	void Start()
	{
		Player = GameObject.Find("Player");
		OriginalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		SelfRegisterAsResetable();
		GetSwarmMembers();
	}

	private void GetSwarmMembers()
	{
		SwarmMembers = new List<EnemyBrain>();
		for (int i = 0; i < transform.childCount; i++)
		{
			SwarmMembers.Add(transform.GetChild(i).gameObject.GetComponent<EnemyBrain>());
		}

		SwarmMembers = (from item in SwarmMembers
						orderby item.name
						select item).ToList();
	}

	void Update()
	{
		if (SwarmIsAlive)
		{
			//print("SWARM LIVES");
			Movement();
		}
		//else
		//{
		//	print("SWARM DIED");
		//}
	}

	private void Movement()
	{
		Single distance = gameObject.transform.position.z - Player.transform.position.z;

		if (distance < ActivationDistance && distance > -100)
		{
			if (!SwarmIsActivated)
			{
				SwarmIsActivated = true;
			}
			transform.Translate(Vector3.forward * (MoveSpeed * -1) * Time.deltaTime);
			//print("SWARM IS MOVING");
		}
		else
		{
			if (SwarmIsActivated)
			{
				DeActivateSwarm();
				SwarmIsActivated = false;
			}
		}
	}

	private void DeActivateSwarm()
	{
		foreach (var swarm_member in SwarmMembers)
		{
			EnemyBrain eb = swarm_member.GetComponent<EnemyBrain>();
			eb.Reset();
		}
	}

	#region IResetable

	public void Reset()
	{
		gameObject.transform.position = OriginalPosition;
		DeadSwarmMembersCount = 0;
		SwarmMembers.ForEach(it => it.Reset());
	}

	public void SelfRegisterAsResetable()
	{
		StateManager.ThingsToReset.Add(this);
	}

	#endregion



}
