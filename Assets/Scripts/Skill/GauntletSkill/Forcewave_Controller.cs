using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcewave_Controller : MonoBehaviour
{
    private float stunDuration;
    private float slowPercent;
    private float slowDuration;
    private float size;

    private SpriteRenderer spriteRenderer;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TriggerEffect();
    }


    public void SetForcewave(float _stunDuration,float _slowPercent,float _slowDuration,float _size)
    {
        stunDuration = _stunDuration;
        slowPercent = _slowPercent;
        slowDuration = _slowDuration;
        size = _size;
        transform.localScale = new Vector3(size, size, 1);
    }
    public void TriggerEffect(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, spriteRenderer.bounds.size.x/2);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if(enemyStats!=null){
                enemyStats.ApplyStunEffect(stunDuration);
                enemyStats.ApplySlowEffect(slowDuration,Mathf.RoundToInt(enemyStats.moveSpeed.GetValue()*slowPercent));
            }
        }
    }
}
