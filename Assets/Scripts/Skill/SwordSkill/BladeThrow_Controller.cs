using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeThrow_Controller : MonoBehaviour
{
    private float speedRotation;
    private float swordSpeed;
    private int damage;
    private float percentExtraDamageOfSkill;
    [SerializeField] private LayerMask obstaclesLayerMask;
    private Rigidbody2D rb;


    private void Start() {
        rb = GetComponentInParent<Rigidbody2D>();
    }



    public void SetBladeThrow(int _damage,float _percentExtraDamageOfSkill,float _speedRotation, float _swordSpeed)
    {
        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        speedRotation = _speedRotation;
        swordSpeed = _swordSpeed;
    }
    void Update()
    {
        transform.Rotate(0,0,speedRotation*Time.deltaTime);
        rb.velocity = transform.parent.right*swordSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Enemy>()){
            Debug.Log("damage" + damage);
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                PlayerManager.instance.player.charaterStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }
        }
    }

}
