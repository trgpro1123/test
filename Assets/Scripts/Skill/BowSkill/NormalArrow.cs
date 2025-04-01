using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalArrow : ArrowBehaviour
{

    private bool canMove=true;
    private StatType statType;
    protected override void Start() {
        base.Start();
        
    }
    protected override void Update()
    {
        if(canMove){
            base.Update();
        }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<EnemyStats>()||other.CompareTag("Obstacle")){
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                if(statType==StatType.magicPower)
                    playerStats.DoMagicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
                else if(statType==StatType.strength)
                    playerStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }

            StuckInto(other);
        }
    }
    public void SetNormalArrow(int _damage, float _percentExtraDamageOfSkill,float _speed,float _lifeTime,StatType _statType)
    {
        damage=_damage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;
        speed=_speed;
        lifeTime=_lifeTime;
        timer=lifeTime;
        statType=_statType;


    }
    private void StuckInto(Collider2D other)
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<CapsuleCollider2D>().enabled=false;
        transform.parent=other.transform;
        canMove=false;
        Destroy(this.gameObject,10);
    }
}

