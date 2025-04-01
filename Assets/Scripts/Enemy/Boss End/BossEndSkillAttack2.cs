using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEndSkillAttack2 : MonoBehaviour
{
    private int maxCount;
    private int damage;
    private float attackSize;
    private float attackDistance;
    private float radius;
    private float timeToNextCreateClone;
    private float timer=0;
    private Enemy_BossEnd enemy;
    private Player player;
    private GameObject objectToSpawn;

    public void SetUpSkill(GameObject _objectToSpawn,int _damage,float _attackSize,float _attackDistance,int _maxCount,float _radius,float _timeToNextCreateClone,Enemy_BossEnd _enemy){
        objectToSpawn=_objectToSpawn;
        damage=_damage;
        attackSize=_attackSize;
        attackDistance=_attackDistance;
        maxCount=_maxCount;
        radius=_radius;
        timeToNextCreateClone=_timeToNextCreateClone;
        enemy=_enemy;
        player=PlayerManager.instance.player;

    }
    private void Update() {
        timer-=Time.deltaTime;
        if(maxCount<=0){
            enemy.gameObject.SetActive(true);
            Destroy(gameObject);
        }
        if(timer<=0){
            timer=timeToNextCreateClone;
            GameObject newClone = SpawnObjectOnCircle();
            newClone.GetComponent<BossEndCloneSkillAttack3>().SetUpSkill(enemy,damage,attackSize,attackDistance);
            maxCount--;
        }
    }
    public GameObject SpawnObjectOnCircle()
    {
        float randomAngle = Random.Range(0f, 360f);
        float angleRad = randomAngle * Mathf.Deg2Rad;
        

        float x = player.transform.position.x + radius * Mathf.Cos(angleRad);
        float y = player.transform.position.y + radius * Mathf.Sin(angleRad);
        Vector3 spawnPosition = new Vector3(x, y, 0f);
        
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        return spawnedObject;
    }
}
