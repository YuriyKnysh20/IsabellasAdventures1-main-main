﻿using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour 
{
 [SerializeField] private Image health;
 [SerializeField] private float delta;
 private float healthValue;
 private float currentHealth;
 private Player player;
	
	void Start () 
	{
		player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		healthValue= player.GetComponent<Health>().CurrentHealth /100.0f;
	}
	
	
	void Update () 
	{
		currentHealth=player.GetComponent<Health>().CurrentHealth /100.0f;
		if(currentHealth > healthValue)
		healthValue += delta;
		if(currentHealth < healthValue)
		healthValue-=delta;
		if(currentHealth < delta)
		healthValue=currentHealth;

		health.fillAmount=healthValue;
	}
}
