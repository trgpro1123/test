using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBlade_Controller : MonoBehaviour
{
    

    private int maxCount;
    private float timeUseSkill;
    private float timeToNextBlink;
    private float distance;
    private int damage;
    private float growSpeed;
    private float percentExtraDamageOfSkill;
    private GameObject animationHit;
    private GameObject areaAttack;

    private float timer;
    private bool canBlink=false;
    private CharaterStats playerStats=>PlayerManager.instance.player.charaterStats;
    private List<Collider2D> enemyList;
    private LayerMask enemyLayerMask;
    private GameObject currentAreaAttack;
    private Player player=>PlayerManager.instance.player;
    

    public void SetBlinkBlade(GameObject _animationHit, int _damage, float _percentExtraDamageOfSkill,float _timeUseSkill, float _timeToNextBlink,int _maxCount,GameObject _areaAttack,float _growSpeed, float _distance,LayerMask _enemyLayerMask)
    {
        animationHit = _animationHit;
        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        timeUseSkill = _timeUseSkill;
        timeToNextBlink = _timeToNextBlink;
        timer = timeToNextBlink;
        maxCount = _maxCount;
        areaAttack = _areaAttack;
        growSpeed = _growSpeed;
        distance = _distance;
        enemyLayerMask = _enemyLayerMask;
        enemyList=new List<Collider2D>();
        currentAreaAttack=Instantiate(areaAttack,transform.position,Quaternion.identity);
        currentAreaAttack.GetComponent<FrozenTimeZoneEffect>().SetFrozenTimeZoneEffect(timeUseSkill,growSpeed,distance);
        
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        timeUseSkill -= Time.deltaTime;
        if (timeUseSkill <= 0&&currentAreaAttack!=null)
        {
            enemyList = currentAreaAttack.GetComponent<FrozenTimeZoneEffect>().GetEnemyInArea();
            Destroy(currentAreaAttack);
            canBlink=true;
            if (enemyList.Count == 0)
            {
                Destroy(gameObject);
            }else{
                player.gameObject.SetActive(false);
            }
        }

        Blink();

    }

    private void Blink()
    {
        if (timer < 0 && canBlink && enemyList.Count>0)
        {
            timer = timeToNextBlink;
            int randomEnemy = Random.Range(0, enemyList.Count);
            GameObject newAnimationHit = Instantiate(animationHit, enemyList[randomEnemy].transform.position, Quaternion.identity);
            EnemyStats enemyStats = enemyList[randomEnemy].GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                AudioManager.instance.PlaySFX(1);
                PlayerManager.instance.player.charaterStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }
            maxCount--;
            if (maxCount <= 0)
            {
                canBlink = false;
                player.transform.position = enemyList[randomEnemy].transform.position;
                Invoke("Reappear", .5f);
            }
        }
    }

    private void Reappear(){
        player.gameObject.SetActive(true);
        Destroy(gameObject);
    }

}
