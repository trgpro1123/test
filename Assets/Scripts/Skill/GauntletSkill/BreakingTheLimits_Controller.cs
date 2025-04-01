using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingTheLimits_Controller : MonoBehaviour
{
    private float healthDecreasePercent;
    private float duration=0;
    private float timer=0;
    private float breakTheLimitsTimer;
    private int healthDecreaseValue;
    

    private PlayerStats playerStats;

    private void Start() {
        playerStats=PlayerManager.instance.player.GetComponent<PlayerStats>();
        healthDecreaseValue= Mathf.RoundToInt(playerStats.health.GetValue()*healthDecreasePercent);
    }
    private void Update() {
        duration-=Time.deltaTime;
        timer-=Time.deltaTime;
        if(duration<0){
            Destroy(gameObject);
        }
        if(timer<0){
            timer=breakTheLimitsTimer;
            playerStats.DecreaseHealthBy(healthDecreaseValue);
        }
    }
    public void SetBreakingTheLimits(float _healthDecreasePercent, float _duration, float _breakTheLimitsTimer)
    {
        healthDecreasePercent = _healthDecreasePercent;
        duration = _duration;
        breakTheLimitsTimer = _breakTheLimitsTimer;
        
    }

}
