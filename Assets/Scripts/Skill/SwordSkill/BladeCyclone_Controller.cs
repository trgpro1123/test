using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeCyclone_Controller : MonoBehaviour
{

    private int damage;
    private float percentExtraDamageOfSkill;
    private float growSpeed;
    private float moveSpeed;
    private float speedRotation;
    private float duration;
    private float damageTimer;
    private float maxSize;


    private bool canMove=true;
    private bool canGrow=false;
    private float timeLife;
    private float timer;
    private Rigidbody2D rb;
    private float currentSizeOfBladeCyclone;
    private Vector2 currentSize;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        timer-=Time.deltaTime;
        timeLife-=Time.deltaTime;
        if(timeLife<=0){
            Destroy(transform.parent.gameObject);
        }
        transform.Rotate(0,0,speedRotation*Time.deltaTime);
        if(canMove){
            rb.velocity = transform.parent.right*moveSpeed;
        }
        if(canGrow){
            currentSize=Vector2.Lerp(transform.localScale,new Vector2(maxSize,maxSize),growSpeed*Time.deltaTime);
            transform.localScale=currentSize;
            currentSizeOfBladeCyclone=GetCurrentSizeOfBlade();
            if(timer<=0){
                DealDamage(damage);
                timer=damageTimer;
            }
        }
        
        
    }

    public void SetBladeCyclone(int _damage,float _percentExtraDamageOfSkill,float _damageTimer,float _moveSpeed,float _growSpeed,float _speedRotation,float _timeLife,float _duration,float _maxSize){
        damage=_damage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;
        damageTimer=_damageTimer;
        timer=0;
        moveSpeed=_moveSpeed;
        growSpeed=_growSpeed;
        speedRotation=_speedRotation;
        timeLife=_timeLife;
        duration=_duration;
        maxSize=_maxSize;
    }
    public void DealDamage(int damage){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, currentSizeOfBladeCyclone);
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<Enemy>()){
                EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    PlayerManager.instance.player.charaterStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Obstacle"||other.GetComponent<Enemy>()){
            timeLife=float.MaxValue;
            this.GetComponentInParent<CircleCollider2D>().enabled=false;
            canMove=false;
            canGrow=true;
            speedRotation*=1.5f;
            rb.velocity=Vector2.zero;
            Destroy(transform.parent.gameObject,duration);
        }
    }
    public float GetCurrentSizeOfBlade(){
        return GetComponentInParent<SpriteRenderer>().bounds.size.y;
    }
    
}
