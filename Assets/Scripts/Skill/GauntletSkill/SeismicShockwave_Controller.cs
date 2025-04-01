using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeismicShockwave_Controller : MonoBehaviour
{
    private GameObject skillObject;
    private int damage;
    private float percentExtraDamageOfSkill;
    private float originalSize;
    private float percentSizeForEachInitialization;
    private float percentageDamagePerInitialization;
    private int numberOfSeismicShockwave;
    private float timeDelay;


    private float timer;
    private float currentSize;
    private float currentDamage;


    public void SetSeismicShockwave(GameObject _skillObject,float _originalSize, float _percentSizeForEachInitialization, float _percentageDamagePerInitialization,float _timeDelay,int _numberOfSeismicShockwave, int _damage, float _percentExtraDamageOfSkill)
    {
        skillObject=_skillObject;
        originalSize = _originalSize;
        percentSizeForEachInitialization = _percentSizeForEachInitialization;
        percentageDamagePerInitialization = _percentageDamagePerInitialization;
        timeDelay = _timeDelay;
        numberOfSeismicShockwave = _numberOfSeismicShockwave;
        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        currentSize = originalSize;
        currentDamage = damage;

    }


    private void Update() {
        timer-=Time.deltaTime;
        if(timer<0){
            if(numberOfSeismicShockwave>0){
                CreateSeismicShockwave();
                timer=timeDelay;
            }            
        }
        if(numberOfSeismicShockwave==0){
            Destroy(gameObject);
        }
    }

    private void CreateSeismicShockwave()
    {
        numberOfSeismicShockwave--;
        GameObject newSeismicShockwave = Instantiate(skillObject, transform.position, Quaternion.identity);
        newSeismicShockwave.transform.localScale = new Vector3(currentSize, currentSize, 1);
        DealDamage(newSeismicShockwave);
        currentSize += percentSizeForEachInitialization;
        currentDamage += (int)(currentDamage * percentageDamagePerInitialization);
    }
    private void DealDamage(GameObject _objectSR)
    {
        Vector3 position = _objectSR.GetComponent<SpriteRenderer>().bounds.center;
        float radius = _objectSR.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>())
            {
                Debug.Log("Damage" + currentDamage);
                EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    PlayerManager.instance.player.charaterStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
                }
            }
        }
        
    }

}
