using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBomb_Controller : MonoBehaviour
{
    private int damage;
    private float percentExtraDamageOfSkill;
    private float flySpeed;
    private float sizeSkill;
    private GameObject energyBombEffect;

    private Rigidbody2D rb;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(sizeSkill, sizeSkill, 1);
    }

    void Update()
    {
        rb.velocity = transform.right*flySpeed;
    }

    public void SetEnergyBomb(float _flySpeed,float _sizeSkill, int _damage, float _percentExtraDamageOfSkill, GameObject _energyBombEffect)
    {
        flySpeed = _flySpeed;
        sizeSkill = _sizeSkill;
        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        energyBombEffect = _energyBombEffect;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<EnemyStats>()!=null||other.GetComponent<WallDetector>()!=null){
            AudioManager.instance.PlaySFX(6);
            GameObject newEnergyBombEffect=Instantiate(energyBombEffect,transform.position,Quaternion.identity);
            newEnergyBombEffect.GetComponent<EnergyBombEffect>().SetEnergyBombEffect(damage,percentExtraDamageOfSkill,sizeSkill);
            Destroy(gameObject);
        }
    }
}
