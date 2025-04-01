using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonCannon_Controller : MonoBehaviour
{
    private int damage;
    private float percentExtraDamageOfSkill;
    private float ionCannonTimer;
    private float duration;
    private float timer;
    private float angle;

    private SpriteRenderer spriteRenderer;
    private GameObject objectSkill;
    private Player player=>PlayerManager.instance.player;
    
    // Update is called once per frame
    private void Start() {
        AudioManager.instance.PlaySFX(16);
    }
    void Update()
    {
        timer -= Time.deltaTime;
        duration -= Time.deltaTime;
        if(player.isRolling||player.charaterStats.isForzerTime){
            AudioManager.instance.StopSFX(16);
            Destroy(gameObject);
        }

        if (timer <= 0)
        {
            ApplyIonCannonDamage();

            timer = ionCannonTimer;
        }

        if (duration <= 0)
        {
            AudioManager.instance.StopSFX(16);
            Destroy(gameObject);
        }

    }

    public void SetIonCannon(int _damage, float _percentExtraDamageOfSkill, float _distance, float _duration, float _ionCannonTimer, float _angle, SpriteRenderer _spriteRenderer,GameObject _objectSkill)
    {

        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        duration=_duration;
        ionCannonTimer=_ionCannonTimer;
        timer=0;
        angle=_angle;
        spriteRenderer=_spriteRenderer;
        objectSkill=_objectSkill;

    }
    private void ApplyIonCannonDamage()
    {
        if (spriteRenderer == null) return;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(
            spriteRenderer.bounds.center, 
            spriteRenderer.bounds.size, 
            angle);
        foreach (var hitCollider in hitColliders)
        {
            EnemyStats enemyStats = hitCollider.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                player.charaterStats.DoMagicalDamage(enemyStats, damage, percentExtraDamageOfSkill);

                Debug.Log("Ion Cannon hit: " + enemyStats.gameObject.name);
            }
        }
    }


    
}
