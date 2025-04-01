using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class ArrowBehaviour : MonoBehaviour
{
    protected float speed;
    protected float lifeTime;
    protected int damage;
    protected float percentExtraDamageOfSkill;

    protected Rigidbody2D rb;
    protected float timer;
    protected PlayerStats playerStats;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerStats=PlayerManager.instance.player.GetComponent<PlayerStats>();

    }

    
    protected virtual void Update()
    {
        rb.velocity = transform.up*speed;
        timer-=Time.deltaTime;
        if(timer<=0){
            Destroy(gameObject);
        }

    }
    
}
