using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

	Image energyBar;
	float MaxEnergy = 100f;
	void Start()
	{
		energyBar = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		energyBar.fillAmount = StateManager.WeaponEnergy / MaxEnergy;
	}
}
