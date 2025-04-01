using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakthroughArrow : ArrowBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<EnemyStats>()){
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                playerStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }
        }
    }
    public void SetBreakthroughArrow(int _damage, float _percentExtraDamageOfSkill,float _speed,float _lifeTime)
    {
        damage=_damage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;
        speed=_speed;
        lifeTime=_lifeTime;
        timer=lifeTime;


    }
}
