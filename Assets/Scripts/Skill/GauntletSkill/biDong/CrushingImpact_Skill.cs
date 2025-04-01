using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrushingImpact_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot crushingImpactButton;
    public bool crushingImpactUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        crushingImpactButton?.GetComponent<Button>().onClick.AddListener(UnlockCrushingImpact);

    }
    public void UnlockCrushingImpact(){
        if(crushingImpactButton.unlock&&crushingImpactUnlocked==false){
            crushingImpactUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockCrushingImpact();
    }
}
