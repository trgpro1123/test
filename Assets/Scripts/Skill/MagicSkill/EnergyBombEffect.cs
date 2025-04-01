using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBombEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int damage;
    private float percentExtraDamage;
    private float radius;
    private Player player;
    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        player=PlayerManager.instance.player;
        transform.localScale=new Vector3(radius,radius,1);
    }

    public void SetEnergyBombEffect(int _damage, float _percentExtraDamage,float _radius)
    {
        damage = _damage;
        percentExtraDamage = _percentExtraDamage;
        radius=_radius;
    }


    public void TriggerEffect(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, spriteRenderer.bounds.size.x/2);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if(enemyStats!=null){
                PlayerManager.instance.player.charaterStats.DoMagicalDamage(enemyStats, damage, percentExtraDamage);
            }
        }
    }
}
