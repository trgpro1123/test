using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingArrow : ArrowBehaviour
{

  
    protected override void Start() {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<EnemyStats>()||other.CompareTag("Obstacle")){
            Debug.Log("Hit"+damage);
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                playerStats.DoMagicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }
        }
    }
    public void SetPiercingArrow(int _damage, float _percentExtraDamageOfSkill,float _speed,float _lifeTime)
    {
        damage=_damage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;
        speed=_speed;
        lifeTime=_lifeTime;
        timer=lifeTime;


    }

}
