using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowManProjectile : MonoBehaviour
{
    private int damage;
    private float speed;
    private float lifeTime;

    protected Rigidbody2D rb;
    protected float timer;
    private bool canMove=true;
    private CharaterStats enemyStats;

    public void SetupProjectile(int _damage,float _speed, float _lifeTime,CharaterStats _enemyStats){
        damage=_damage;
        speed=_speed;
        lifeTime=_lifeTime;
        timer=lifeTime;
        enemyStats=_enemyStats;
    }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();

    }

    
    protected virtual void Update()
    {
        if(canMove){
            rb.velocity = transform.up*speed;
            timer-=Time.deltaTime;
            if(timer<=0){
                Destroy(gameObject);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<PlayerStats>()||other.CompareTag("Obstacle")){
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if(playerStats!=null){
                enemyStats.DoPhysicalDamage(playerStats,damage);
            }
            StuckInto(other);
        }
    }

    private void StuckInto(Collider2D other)
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<CapsuleCollider2D>().enabled=false;
        transform.parent=other.transform;
        canMove=false;
        Destroy(this.gameObject,10);
    }
}
