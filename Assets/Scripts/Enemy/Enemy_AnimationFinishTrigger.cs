using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationFinishTrigger : MonoBehaviour
{
    Enemy enemy=>GetComponentInParent<Enemy>();
    public void AnimationTrigger(){
        enemy.AnimatorTrigger();
    }
    
    public void AttackTrigger1(){
        enemy.Attack1();
    }
    public void AttackTrigger2(){
        enemy.Attack2();
    }
    public void AttackTrigger3(){
        enemy.Attack3();
    }
    public void AttackTrigger4(){
        enemy.Attack4();
    }
    public void SetAttackArea(float _attackSize,float _attackDistance){
        enemy.SetAttackArea(_attackSize,_attackDistance);
        // OpenAttackArea();
    }
    public void SpecialAttackTrigger(){
        // enemy.AnimationSpecialAttackTrigger();
    }
    public void SetAnglePlayer(){
        enemy.SetAngleToPlayer();
    }
    public void SetAngleSpecial(){
        enemy.SetAngleSpecial();
    }

    public void OpenAttackArea(){
        enemy.OpenAttackArea();
    }
    public void CloseAttackArea(){
        enemy.CloseAttackArea();
    }
    public void CloseAttackHitBox(){
        enemy.CloseAttackHitBox();
    }


}