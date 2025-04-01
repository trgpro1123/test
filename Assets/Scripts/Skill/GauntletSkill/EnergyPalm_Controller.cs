using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPalm_Controller : MonoBehaviour
{

    private int damage;
    private float percentExtraDamageOfSkill;
    private float flySpeed;


    private Rigidbody2D rb;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity = transform.up*flySpeed;
    }

    public void SetFlySpeed( int _damage, float _percentExtraDamageOfSkill,float _flySpeed)
    {
        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        flySpeed = _flySpeed;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<EnemyStats>()){
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                PlayerManager.instance.player.charaterStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }

        }
    }

}
