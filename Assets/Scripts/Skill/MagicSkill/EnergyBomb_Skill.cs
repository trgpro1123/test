using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBomb_Skill : Skill
{
    public float flySpeed;
    public float sizeSkill;
    public GameObject energyBombEffect;

    [SerializeField] private UI_SkillTreeSlot energyBombUnlockButton;
    public bool energyBombUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        energyBombUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockEnergyBomb);
    }
    public void UnlockEnergyBomb(){
        if(energyBombUnlockButton.unlock){
            energyBombUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newEnergyBomb=Instantiate(skillObject, player.attackCheck.position + Quaternion.Euler(0, 0, player.AnglePlayerToMouse()) * new Vector3(skillDistance, 0, 0), Quaternion.Euler(0, 0, player.AnglePlayerToMouse()));
        newEnergyBomb.GetComponent<EnergyBomb_Controller>().SetEnergyBomb(flySpeed,sizeSkill, skillDamage, percentExtraDamageOfSkill, energyBombEffect);
        if(newEnergyBomb!=null){
            Destroy(newEnergyBomb, 5f);
        }       
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.preUseSkillState.SetCurrentSkill(UseSkill);
            player.useSkillState.SetTimeUseSkill(0.2f);
            player.stateMachine.ChangeState(player.preUseSkillState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockEnergyBomb();
    }
}
