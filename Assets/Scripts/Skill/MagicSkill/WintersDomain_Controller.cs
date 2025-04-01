using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WintersDomain_Controller : MonoBehaviour
{
    private float duration;
    private float coldPercent;
    private float size;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer=GetComponent<SpriteRenderer>();
        transform.localScale= new Vector3(size,size,1);
        TriggerSkill();
    }

    public void SetWinterDomainEffect(float _duration, float _coldPercent,float _size){
        duration=_duration;
        coldPercent=_coldPercent;
        size=_size;
    }
    public void TriggerSkill(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, spriteRenderer.bounds.size.x/2);
        AudioManager.instance.PlaySFX(19);
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            if(enemy!=null){
                enemy.charaterStats.SetUpColdEffect(duration);
                enemy.ColdFor(duration, coldPercent);
            }
            
        }
        Destroy(gameObject);
    }
}
