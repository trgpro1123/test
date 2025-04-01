using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkBlade_Skill : Skill
{

    public int maxCount;
    public float timeUseSkill;
    public float growSpeed;
    public float timeToNextBlink;
    public GameObject animationHit;
    public GameObject areaAttack;
    public LayerMask enemyLayerMask;



    [SerializeField] private UI_SkillTreeSlot blinkBladeUnlockButton;
    public bool blinkBladeUnlocked{get;private set;}


    protected override void Start()
    {
        blinkBladeUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockBlinkBlade);
        base.Start();
    }
    public void UnlockBlinkBlade(){
        if(blinkBladeUnlockButton.unlock){
            blinkBladeUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        GameObject newBlinkBlade=Instantiate(skillObject,player.attackCheck.position,Quaternion.identity);
        newBlinkBlade.GetComponent<BlinkBlade_Controller>().SetBlinkBlade(animationHit,skillDamage,percentExtraDamageOfSkill,timeUseSkill,timeToNextBlink,maxCount,areaAttack,growSpeed,skillDistance,enemyLayerMask);
    }



    
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.stateMachine.ChangeState(player.blinkBladeState);
            UseSkill();
        }
        
        
    }
    protected override void CheckUnlock()
    {
        UnlockBlinkBlade();
    }
}
