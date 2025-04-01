using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCaveProjectle : MonoBehaviour
{
    private float speed;
    private float lifeTime;
    private int damage;

    private Rigidbody2D rb;
    private float timer;
    private CharaterStats charaterStats;
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        // capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    
    private void Update()
    {
        rb.velocity = transform.right*speed;
        timer-=Time.deltaTime;
        if(timer<=0){
            gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()){
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                charaterStats.DoPhysicalDamage(playerStats, damage);
            }
        }
    }
    public void SetProjectile(int _damage,float _speed,float _lifeTime,CharaterStats _charaterStats)
    {
        damage=_damage;
        speed=_speed;
        lifeTime=_lifeTime;
        timer=lifeTime;
        charaterStats=_charaterStats;


    }
}
