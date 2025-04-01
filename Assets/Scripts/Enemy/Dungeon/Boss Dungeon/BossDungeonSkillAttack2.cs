using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDungeonSkillAttack2 : MonoBehaviour
{
    private float size;
    private int damage;
    public SpriteRenderer spriteRenderer;
    private CharaterStats charaterStats;

    


    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    public void SetupSkill(int _damage,float _size, CharaterStats _charaterStats){
        damage=_damage;
        size=_size;
        charaterStats=_charaterStats;
        transform.localScale=new Vector3(size,size,1);
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
