using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking1_Skill : MonoBehaviour
{
    private int damage;
    private float speed;
    private float lifeTime;
    private Animator animator;

    protected Rigidbody2D rb;
    protected float timer;
    public CharaterStats enemyStats;


    public void SetupSkill(int _damage,float _speed, float _lifeTime,CharaterStats _enemyStats){
        damage=_damage;
        speed=_speed;
        lifeTime=_lifeTime;
        timer=lifeTime;
        enemyStats=_enemyStats;
    }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    
    protected virtual void Update()
    {
        rb.velocity = transform.right*speed;
        timer-=Time.deltaTime;
        if(timer<=0){
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<PlayerStats>()||other.CompareTag("Obstacle")){
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if(playerStats!=null){
                enemyStats.DoMagicalDamage(playerStats,damage);
            }
            animator.SetTrigger("Hit");
            Destroy(gameObject);
        }
    }
}
