﻿using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    //  [SerializeField] private float delta;
    //  private float healthValue;
    private float currentHealth;
    private float maxValue;
    //  private Player player;

    // void Start () 
    // {
    // 	player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    // 	healthValue= player.GetComponent<Health>().CurrentHealth /100.0f;
    // }


    // void Update () 
    // {
    // 	currentHealth=player.GetComponent<Health>().CurrentHealth /100.0f;
    // 	if(currentHealth > healthValue)
    // 	healthValue += delta;
    // 	if(currentHealth < healthValue)
    // 	healthValue-=delta;
    // 	if(currentHealth < delta)
    // 	healthValue=currentHealth;

    // 	health.fillAmount=healthValue;
    // }

    public void SetupHealthBar(float maxHealthValue)
    {
        maxValue = maxHealthValue;
        currentHealth = maxHealthValue;
        health.fillAmount = 1;
    }

    public void UpdateValue(float healtValue)
    {
        currentHealth = healtValue;
        health.fillAmount = healtValue / maxValue;
    }

}
