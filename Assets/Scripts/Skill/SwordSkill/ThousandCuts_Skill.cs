using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThousandCuts_Skill : Skill
{
    public float damageTimer;
    public float skillSize;
    public float duration;
    [SerializeField] private UI_SkillTreeSlot thousandCutsUnlockButton;
    public bool thousandCutsUnlocked{get;private set;}


    protected override void Start()
    {
        thousandCutsUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockThousandCuts);
        base.Start();
    }
    public void UnlockThousandCuts(){
        if(thousandCutsUnlockButton.unlock){
            thousandCutsUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        GameObject newThousandsCuts = Instantiate(skillObject, player.transform.position, Quaternion.identity);
        newThousandsCuts.GetComponent<ThousandCuts_Controller>().SetThousandCuts(skillDamage, percentExtraDamageOfSkill, damageTimer, skillSize);
        Destroy(newThousandsCuts, duration);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.stateMachine.ChangeState(player.thousandCutsState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockThousandCuts();
    }
}
