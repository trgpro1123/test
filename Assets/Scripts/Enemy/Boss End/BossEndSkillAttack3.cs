using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEndSkillAttack3 : MonoBehaviour
{
    private int maxCount;
    private float radius;



    private Player player;
    private GameObject objectToSpawn;
    private GameObject magicSummon;
    private Enemy_BossEnd enemy;

    public void SetUpSkill(GameObject _objectToSpawn,int _maxCount,float _radius,GameObject _magicSummon,Enemy_BossEnd _enemy){
        objectToSpawn=_objectToSpawn;
        maxCount=_maxCount;
        radius=_radius;
        magicSummon=_magicSummon;
        player=PlayerManager.instance.player;
        enemy=_enemy;

    }
    private void Update() {
        if(maxCount>0){
            GameObject newClone = SpawnObjectOnCircle();
            maxCount--;
        }else{
            Destroy(gameObject);
        }

    }
    public GameObject SpawnObjectOnCircle()
    {
        float randomAngle = Random.Range(0f, 360f);
        float angleRad = randomAngle * Mathf.Deg2Rad;
        

        float x = transform.position.x + radius * Mathf.Cos(angleRad);
        float y = transform.position.y + radius * Mathf.Sin(angleRad);
        Vector3 spawnPosition = new Vector3(x, y, 0f);
        
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        Enemy newClone=spawnedObject.GetComponent<Enemy>();
        newClone.onDeath+=OnCloneBossDeath;
        enemy.listCloneBoss.Add(spawnedObject);
        spawnedObject.gameObject.SetActive(false);
        GameObject newMagicSummon=Instantiate(magicSummon,spawnPosition,Quaternion.identity);
        newMagicSummon.GetComponent<MagicSummon>().OnSummon+=spawnedObject.gameObject.SetActive;
        return spawnedObject;
    }
    public void OnCloneBossDeath(Enemy _enemy){
        enemy.listCloneBoss.Remove(_enemy.gameObject);
        if(enemy.listCloneBoss.Count<=0){
            enemy.canUseSkill3=true;
        }
    }
}
