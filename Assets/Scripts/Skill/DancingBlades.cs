using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingBlades : MonoBehaviour
{
    private int damage;
    private float percentExtraDamageOfSkill;

    public void SetBlade(int _damage, float _percentExtraDamageOfSkill)
    {
        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;

    }
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyStats enemyStats = other.GetComponent<EnemyStats>();
        if(enemyStats!=null){
            Debug.Log("Hit enemy"+damage);
            if (enemyStats != null)
            {
                PlayerManager.instance.player.charaterStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }
        }
    }
}
