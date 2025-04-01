using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateExplosion_Controller : MonoBehaviour
{
    private int damage;
    private float percentExtraDamageOfSkill;
    private float size;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(size, size, 1);
        TriggerSkill();
    }

    public void SetUltimateExplosion(int _damage, float _percentExtraDamageOfSkill, float _size)
    {
        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        size = _size;

    }
    
    public void TriggerSkill(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, spriteRenderer.bounds.size.x/2);
        AudioManager.instance.PlaySFX(18);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if(enemyStats!=null){
                PlayerManager.instance.player.charaterStats.DoMagicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }
            
        }
        Destroy(gameObject);
    }
    

}
