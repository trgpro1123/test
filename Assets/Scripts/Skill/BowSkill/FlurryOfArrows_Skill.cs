using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlurryOfArrows_Skill : Skill
{

    public float speed;
    public float lifeTime;
    public float skillDuration;
    public float noise;

    [SerializeField] private UI_SkillTreeSlot flurryOfArrowsUnlockButton;
    public bool flurryOfArrowsUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        flurryOfArrowsUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockFlurryOfArrows);
    }
    public void UnlockFlurryOfArrows(){
        if(flurryOfArrowsUnlockButton.unlock){
            flurryOfArrowsUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        player.FlipPlayer();
        GameObject newArrow= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.Euler(0, 0, player.AnglePlayerToMouse()-90+Random.Range(-noise,noise)));
        newArrow.GetComponent<NormalArrow>().SetNormalArrow(skillDamage, percentExtraDamageOfSkill,speed,lifeTime,StatType.strength);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if (CanUseSkill()){
            player.archeryState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.flurryOfArrowsSkillState);
        }

        
    }
    protected override void CheckUnlock()
    {
        UnlockFlurryOfArrows();
    }
}
