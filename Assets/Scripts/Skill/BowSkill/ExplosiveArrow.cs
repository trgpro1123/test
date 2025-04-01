using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : ArrowBehaviour
{
    private bool canMove=true;
    private GameObject explosionEffect;

    protected override void Start() {
        base.Start();
    }
    protected override void Update()
    {
        if(canMove){
            base.Update();
        }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<EnemyStats>()||other.CompareTag("Obstacle")){
            Debug.Log("booom"+damage);
            AudioManager.instance.PlaySFX(6);
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                playerStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }
            GameObject effect=Instantiate(explosionEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);

 
        }
    }
    public void SetExplosiveArrow(GameObject _explosiveEffect,int _damage,  float _percentExtraDamageOfSkill,float _speed,float _lifeTime)
    {
        explosionEffect=_explosiveEffect;
        damage=_damage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;
        speed=_speed;
        lifeTime=_lifeTime;
        timer=lifeTime;


    }

}
