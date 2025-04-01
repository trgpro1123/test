using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForestSkillAttack2 : MonoBehaviour
{
    private int damage;

    private Enemy_BossForest enemy;


    public void SetupSkill(int _damage,Enemy_BossForest _enemy)
    {
        damage = _damage;
        enemy=_enemy;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            enemy.charaterStats.DoPhysicalDamage(playerStats,damage);
        }
        if(other.gameObject.layer==LayerMask.NameToLayer("Obstacle")){
            enemy.stateMachine.ChangeState(enemy.battleState);
            Destroy(gameObject);
        }
        
    }
}
