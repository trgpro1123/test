using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    public System.Action OnAttackTrigger;
    private Player player =>GetComponentInParent<Player>();

    private void AnimationTrigger(){
        player.AnimatorTrigger();
    }
    public void AttackTrigger(){
        OnAttackTrigger?.Invoke();
    }

    public void AttackOpenTrigger(){
        player.AttackOpenTrigger();
    }
    public void AttackEndTrigger(){
        player.AttackEndTrigger();
    }
    
}
