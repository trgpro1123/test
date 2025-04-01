using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomGoblinEffect : MonoBehaviour
{
    private int damage;
    private SpriteRenderer spriteRenderer;
    private CharaterStats enemyStats;
    private void Start() {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    public void SetUpBoomGoblinEffect(int _damage,CharaterStats _enemyStats){
        damage=_damage;
        enemyStats=_enemyStats;
    }
    public void TriggerBoomGoblinEffect(){
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, spriteRenderer.bounds.size.x/2);
        foreach (Collider2D item in hitPlayer)
        {
            PlayerStats playerStats = item.GetComponent<PlayerStats>();
            if(playerStats!=null){
                enemyStats.DoPhysicalDamage(playerStats,damage);
            }
        }
        
    }
}
