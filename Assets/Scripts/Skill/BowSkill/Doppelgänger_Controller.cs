using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Doppelgänger_Controller : MonoBehaviour
{
    
    private float doppelgängerDuration;
    private float radiusDetect;
    private GameObject arrowObject;
    private int arrowDamage;
    private float percentExtraDamageOfSkill;
    private float arrowSpeed;
    private float arrowLifeTime;
    private float timeCloneAttack;

    private List<Transform> enemyList;
    private bool canAttack=true;
    private bool isDie=false;
    private float timerDuration;


    private float timeDetect=0.2f;
    private Animator animator;
    private Transform closestTarget;
    protected bool facingRight =true;


    private void Start() {
        enemyList = new List<Transform>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        timerDuration-=Time.deltaTime;
        timeDetect-=Time.deltaTime;

        if(timeDetect<=0){
            DetectEmenies();
            timeDetect=0.2f;
        }

        if(timerDuration<=0){
            isDie=true;
            animator.SetTrigger("Die");
        }

        if(enemyList.Count!=0&&canAttack&&!isDie){
            StartCoroutine(AttackAnimation());
        }
    }

    private IEnumerator AttackAnimation()
    {
        animator.SetBool("Attack", true);
        FaceToTarget(closestTarget);
        yield return new WaitForSeconds(timeCloneAttack);
        canAttack = true;
    }

    public void Attack()
    {
        canAttack = false;
        AudioManager.instance.PlaySFX(5);
        Vector3 direction = closestTarget.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        GameObject newArrow = Instantiate(arrowObject, transform.position, Quaternion.Euler(0, 0, angle));
        newArrow.GetComponent<NormalArrow>().SetNormalArrow(arrowDamage, percentExtraDamageOfSkill,arrowSpeed, arrowLifeTime,StatType.strength);
        enemyList.Clear();
    }

    public void SetDoppelgänger(GameObject _arrowObject,int _arrowDamage, float _percentExtraDamageOfSkill,float _arrowSpeed, float _arrowLifeTime,float _doppelgängerDuration,float _radiusDetect,  float _timeCloneAttack)
    {
        arrowObject = _arrowObject;
        arrowDamage = _arrowDamage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        arrowSpeed = _arrowSpeed;
        arrowLifeTime = _arrowLifeTime;
        doppelgängerDuration = _doppelgängerDuration;
        radiusDetect=_radiusDetect;
        timeCloneAttack = _timeCloneAttack;
        timerDuration = doppelgängerDuration;
    }

    private void DetectEmenies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, radiusDetect);
        foreach (Collider2D enemy in hitEnemies)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, enemy.transform.position - transform.position);
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Enemy>() && !enemyList.Contains(hit.transform))
                {
                    enemyList.Add(hit.collider.transform);
                }
            }
        }
        closestTarget=ClosestTarget(enemyList);
    }

    private Transform ClosestTarget(List<Transform> _ListTarget)
    {
        return closestTarget = _ListTarget.OrderBy
                            (target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
    }

    public void AnimationTrigger(){
        animator.SetBool("Attack",false);
    }
    public virtual void Flip(){
        facingRight=!facingRight;
        transform.Rotate(0,180,0);

    }
    public void FaceToTarget(Transform _target){
        
        if(_target.position.x<transform.position.x){
            if(facingRight) Flip();
        }
        else{
            if(!facingRight) Flip();
        }
    }







    // aiData.currentTarget = aiData.targets.OrderBy
    //                 (target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
}
