using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyPalm_Skill : Skill
{
    public float flySpeed;
    public float duration;

    [SerializeField] private UI_SkillTreeSlot energyPalmUnlockButton;
    public bool energyPalmUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        energyPalmUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockEnergyPalm);
    }
    public void UnlockEnergyPalm(){
        if(energyPalmUnlockButton.unlock){
            energyPalmUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(13);
        GameObject newEnergyPalm= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.Euler(0, 0, player.AnglePlayerToMouse()-90));
        newEnergyPalm.GetComponent<EnergyPalm_Controller>().SetFlySpeed( skillDamage, percentExtraDamageOfSkill,flySpeed);
        Destroy(newEnergyPalm, duration);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.stateMachine.ChangeState(player.energyPalmSkillState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockEnergyPalm();
    }
}
