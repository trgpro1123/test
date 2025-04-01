using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiercingShot_Skill : Skill
{

    public float speed;
    public float lifeTime;

    [SerializeField] private UI_SkillTreeSlot piercingShotUnlockButton;
    public bool piercingShotUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        piercingShotUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockPiercingShot);
    }
    public void UnlockPiercingShot(){
        if(piercingShotUnlockButton.unlock){
            piercingShotUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(5);
        GameObject newArrow= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.Euler(0, 0, player.AnglePlayerToMouse()-90));
        newArrow.GetComponent<PiercingArrow>().SetPiercingArrow(skillDamage, percentExtraDamageOfSkill,speed,lifeTime);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if (CanUseSkill()){
            player.archeryState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.archeryState);
        }

        
    }
    protected override void CheckUnlock()
    {
        UnlockPiercingShot();
    }
}
