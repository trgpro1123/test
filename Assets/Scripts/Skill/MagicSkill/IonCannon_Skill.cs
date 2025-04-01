using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IonCannon_Skill : Skill
{
    public float duration;
    public float ionCannonTimer;

    [SerializeField] private UI_SkillTreeSlot ionCannonUnlockButton;
    public bool ionCannonUnlocked{get;private set;}

    private IonCannon_Controller ionCannon_Controller;


    protected override void Start()
    {
        base.Start();
        ionCannonUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockIonCannon);
    }
    public void UnlockIonCannon(){
        if(ionCannonUnlockButton.unlock){
            ionCannonUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        float angle = player.AnglePlayerToMouse();
        GameObject newIonCannon=Instantiate(skillObject,player.attackCheck.transform.position, Quaternion.Euler(0, 0, angle));
        newIonCannon.GetComponent<IonCannon_Controller>().SetIonCannon(skillDamage, percentExtraDamageOfSkill, skillDistance,duration,ionCannonTimer, angle, newIonCannon.GetComponent<SpriteRenderer>(),newIonCannon.gameObject);        
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.preUseSkillState.SetCurrentSkill(UseSkill);
            player.useSkillState.SetTimeUseSkill(duration);
            player.stateMachine.ChangeState(player.preUseSkillState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockIonCannon();
    }
}
