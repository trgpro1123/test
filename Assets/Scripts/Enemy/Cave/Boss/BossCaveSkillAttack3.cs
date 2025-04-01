using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCaveSkillAttack3 : MonoBehaviour
{
    private int damage;
    private float size;
    private Enemy_BossCave enemy;




    public void SetProjectile(int _damage,float _size, Enemy_BossCave _enemy)
    {
        damage = _damage;
        size = _size;
        transform.localScale = new Vector3(size, size, 1);
        enemy = _enemy;
        

    }
    public void ChangeState() {
        Destroy(gameObject);
        enemy.stateMachine.ChangeState(enemy.battleState);
        Debug.Log("ChangeState");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()){
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                enemy.charaterStats.DoPhysicalDamage(playerStats, damage);
            }
        }
    }
    // public void TriggerSkill(){

    //     Debug.Log("enemyDamageHitBox");
        
    //     Collider2D[] colliders = Physics2D.OverlapBoxAll(spriteRenderer.bounds.center, spriteRenderer.size, enemy.angleToPlayer);
        
    //     foreach (var item in colliders)
    //     {
            
    //         PlayerStats playerStats = item.GetComponent<PlayerStats>();
    //         if (playerStats != null)
    //         {
    //             // EnemyStats enemyStats = item.GetComponent<EnemyStats>();
    //             Debug.Log("enemyDamageHitBox");
    //             enemy.charaterStats.DoPhysicalDamage(playerStats, damage);
    //             break;
    //         }
    //     }
    // }
}
