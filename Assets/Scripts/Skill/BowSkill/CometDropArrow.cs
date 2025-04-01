using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometDropArrow : ArrowBehaviour
{
    private Vector2 targetPosition;
    private GameObject explosiveEffect;
    private GameObject earthCrack;
    private float earthCrackDamage;
    private float earthCrackDuration;
    private int earthCrackDamageTimer;
    private float explosionSizeEffect;
    private void FixedUpdate() {
        if(Vector2.Distance(transform.position, targetPosition) <= 0.2)
        {
            CometDropArrowHit();
            Destroy(gameObject);
        }
        
    }
    public void SetCometDropArrow(int _damage, float _percentExtraDamageOfSkill,float _speed,float _lifeTime,Vector2 _targetPosition, GameObject _explosiveEffect,GameObject _earthCrack, float _earthCrackDamage,int _earthCrackDamageTimer, float _earthCrackDuration, float _explosionSizeEffect)
    {
        damage=_damage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;
        speed=_speed;
        lifeTime=9999;
        timer=lifeTime;
        targetPosition=_targetPosition;
        explosiveEffect=_explosiveEffect;
        earthCrack=_earthCrack;
        earthCrackDamage=_earthCrackDamage;
        earthCrackDamageTimer=_earthCrackDamageTimer;
        earthCrackDuration=_earthCrackDuration;
        explosionSizeEffect=_explosionSizeEffect;

    
    }
    public void CometDropArrowHit()
    {
        AudioManager.instance.PlaySFX(6);
        GameObject newExplosiveEffect = Instantiate(explosiveEffect, transform.position, Quaternion.identity);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionSizeEffect);
        foreach (var hit in colliders)
        {
            EnemyStats enemyStats = hit.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                // hit.GetComponent<Enemy>().TakeDamage(damage);
                PlayerManager.instance.player.charaterStats.DoMagicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
                Debug.Log("comet drop hit"+damage);
            }
        }
        GameObject newEarthCrack = Instantiate(earthCrack, transform.position, Quaternion.identity);
        newEarthCrack.GetComponent<CometDrop_EarthCrack>().SetEarthCrack(earthCrackDamage,percentExtraDamageOfSkill, earthCrackDuration,earthCrackDamageTimer, explosionSizeEffect);
        Destroy(gameObject);
    }

    
}
