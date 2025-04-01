using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWinterSkill2 : MonoBehaviour
{
    private int damage;
    public SpriteRenderer spriteRenderer;
    private CharaterStats charaterStats;

    


    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    public void SetupSkill(int _damage, CharaterStats _charaterStats){
        damage=_damage;
        charaterStats=_charaterStats;
    }
    public void TriggerSkill(){
        spriteRenderer.enabled=false;
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, spriteRenderer.bounds.size.x/2);
        foreach (Collider2D player in hitPlayer)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if(playerStats!=null){
                charaterStats.DoPhysicalDamage(playerStats,damage);
            }
        }
        Destroy(gameObject);
    }
}
