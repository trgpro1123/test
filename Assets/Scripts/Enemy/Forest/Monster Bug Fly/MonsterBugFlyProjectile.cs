using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBugFlyProjectile : MonoBehaviour
{
    private int damage;
    private float speed;
    private float lifeTime;

    protected Rigidbody2D rb;
    protected float timer;
    private CharaterStats enemyStats;
    private Sprite iconStatus;
    private string statusNameKey;
    private string statusDescriptionKey;
    private float duration;

    public void SetupProjectile(int _damage,float _speed, float _lifeTime,Sprite _iconStatus,string _statusNameKey,string _statusDescriptionKey,float _duration,CharaterStats _enemyStats){
        damage=_damage;
        speed=_speed;
        lifeTime=_lifeTime;
        timer=lifeTime;
        iconStatus=_iconStatus;
        statusNameKey=_statusNameKey;
        statusDescriptionKey=_statusDescriptionKey;
        duration=_duration;
        enemyStats=_enemyStats;
    }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();

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
                enemyStats.DoPhysicalDamage(playerStats,damage);
                playerStats.SetUpReverseControlsEffect(duration);
                UI.instance.ingameUI.CreateStatus(iconStatus,statusNameKey,statusDescriptionKey,duration);
            }
            Destroy(gameObject);
        }
    }
}
