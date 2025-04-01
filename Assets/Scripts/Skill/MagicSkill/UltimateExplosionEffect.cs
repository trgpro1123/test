using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateExplosionEffect : MonoBehaviour
{
    private Player player;
    private PlayerStats playerStats;
    private float duration;
    private float timer;
    void Start()
    {
        player=PlayerManager.instance.player;
        playerStats=player.GetComponent<PlayerStats>();
        Destroy(gameObject,duration);
    }

    public void SetUlUltimateExplosionEffect(float _duration){
        duration=_duration;
        timer=duration;
        
    }
    void Update()
    {
        timer-=Time.deltaTime;
        if(player.isRolling||player.charaterStats.isForzerTime){
            Destroy(gameObject);
        }
        if(timer<=0){
            Destroy(gameObject);
        }
    }
    private void OnDestroy() {
        playerStats.DecreaseHealthBy(Mathf.RoundToInt(playerStats.GetMaxHealth()*0.33f));
    }
}
