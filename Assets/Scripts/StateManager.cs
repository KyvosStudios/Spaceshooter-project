using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
	public static class StateManager
	{
		public static GameObject Player { get; set; }
		public static Single CurrentPlayerSpeed { get; set; }

		private static PlayerController _PlayerController;
		public static PlayerController PlayerController
		{
			get
			{
				if (_PlayerController == null)
				{
					_PlayerController = Player.GetComponent<PlayerController>();
				}
				return _PlayerController;
			}
		}

		public static Single WeaponEnergy { get; set; }

		private static List<IResetable> _ThingsToReset;

		public static List<IResetable> ThingsToReset
		{
			get 
			{
				if (_ThingsToReset == null)
				{
					_ThingsToReset = new List<IResetable>();
				}
				return _ThingsToReset;
			}
			set 
			{
				ThingsToReset = value;
			}
		}

		public static void Reset()
		{
			ThingsToReset.ForEach(it => it.Reset());
		}

		private static Dictionary<int, EnemyBrain> _Enemies;
		public static Dictionary<int, EnemyBrain> Enemies 
		{ 
			get
			{
				if (_Enemies == null)
				{
					Enemies = new Dictionary<int, EnemyBrain>();
				}
				return _Enemies;
			}
			set
			{
				_Enemies = value;
			}
		}
		
	}
}
