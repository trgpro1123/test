using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalisShift_Controller : MonoBehaviour
{
    private int damage;
    private float percentExtraDamage;
    private float duration;
    private float radius;
    private GameObject explosionEffect;
    private float timer;




    public void SetCrystalisShift(int _damage, float _percentExtraDamage, float _duration,float _radius, GameObject _explosionEffect)
    {
        damage = _damage;
        percentExtraDamage = _percentExtraDamage;
        duration = _duration;
        timer=duration;
        radius = _radius;
        explosionEffect = _explosionEffect;
    }

    private void Update() {
        timer-=Time.deltaTime;
        if(timer<=0){
            CreateExplosion();
        }
    }
    public void CreateExplosion()
    {
        GameObject explosion=Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosion.GetComponent<CrystalShiftExplosionEffect>().SetExplosion(damage, percentExtraDamage,radius);
        UI.instance.ingameUI.DeleteStatus("Crystalis Shift");
        Destroy(gameObject);
    }
    

}
