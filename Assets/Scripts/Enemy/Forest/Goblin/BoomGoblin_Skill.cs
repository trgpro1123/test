using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomGoblin_Skill : MonoBehaviour
{
    [SerializeField] private GameObject boomAreaEffect;
    [SerializeField] private AnimationCurve animationCurve;
    private GameObject boomEffect;
    private int damage;
    private float duration;
    private float heightY;
    private Transform playerPos;
    private GameObject currentBoomAreaEffect;
    private CharaterStats enemyStats;
    

    public void SetUpBoomGoblinSkill(GameObject _boomEffect, int _damage, float _duration, float _heightY, CharaterStats _enemyStats){
        // animationCurve=_animationCurve;
        boomEffect=_boomEffect;
        damage=_damage;
        duration=_duration;
        heightY=_heightY;
        playerPos=PlayerManager.instance.player.transform;
        currentBoomAreaEffect=Instantiate(boomAreaEffect,playerPos.position,Quaternion.identity);
        enemyStats=_enemyStats;
        StartCoroutine(ProjecBoomCurveRoutine(transform.position,playerPos.position));

    }
    IEnumerator ProjecBoomCurveRoutine(Vector3 startPosition,Vector3 endPosition){
        float timePassed=0f;
        while(timePassed<duration){
            timePassed+=Time.deltaTime;
            float linearT=timePassed/duration;
            float heightT=animationCurve.Evaluate(linearT);
            float height=Mathf.Lerp(0f,heightY,heightT);

            transform.position=Vector2.Lerp(startPosition,endPosition,linearT)+new Vector2(0f,height);


            yield return null;
        }
        GameObject boomGoblinEffect=Instantiate(boomEffect,transform.position,Quaternion.identity);
        boomGoblinEffect.GetComponent<BoomGoblinEffect>().SetUpBoomGoblinEffect(damage,enemyStats);
        Destroy(currentBoomAreaEffect);
        Destroy(gameObject);
    }
}
