using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassThroughArmor_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot overwhelmingPowerButton;
    public bool passThroughArmorUnlocked{get; private set;}

    protected override void Start()
    {
        overwhelmingPowerButton?.GetComponent<Button>().onClick.AddListener(UnlockPassThroughArmor);
        base.Start();

    }
    public void UnlockPassThroughArmor(){
        if(overwhelmingPowerButton.unlock){
            passThroughArmorUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockPassThroughArmor();
    }
}
