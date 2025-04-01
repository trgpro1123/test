using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometDrop_EarthCrack : MonoBehaviour
{
    private GameObject earthCrack;
    private float earthCrackDamage;
    private float percentExtraDamageOfSkill;
    private float earthCrackDuration;
    private float explosionSizeEffect;
    private int earthCrackDamageTimer;
    private float timer;
    private Transform transformEffectEarthCrack;
    PlayerStats playerStats;

    private void Awake() {
        transformEffectEarthCrack=GetComponentInChildren<Transform>();
        playerStats=PlayerManager.instance.player.GetComponent<PlayerStats>();
    }
    public void SetEarthCrack(float _earthCrackDamage,float _percentExtraDamageOfSkill, float _earthCrackDuration,int _earthCrackDamageTimer, float _explosionSizeEffect)
    {
        earthCrackDamage=_earthCrackDamage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;
        earthCrackDuration=_earthCrackDuration;
        explosionSizeEffect=_explosionSizeEffect;
        earthCrackDamageTimer=_earthCrackDamageTimer;
        transformEffectEarthCrack.localScale=new Vector3(explosionSizeEffect,explosionSizeEffect,0);
        Destroy(gameObject,earthCrackDuration);
    }
    private void Update() {
        timer-=Time.deltaTime;
        if(timer<=0){
            earthCrackDamagePerSecond();
            timer=earthCrackDamageTimer;
        }
    }
    private void earthCrackDamagePerSecond(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionSizeEffect);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                playerStats.DoMagicalDamage(hit.GetComponent<EnemyStats>(), earthCrackDamageTimer, percentExtraDamageOfSkill);
            }
        }
    }
}
