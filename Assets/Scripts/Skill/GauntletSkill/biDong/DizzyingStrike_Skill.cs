using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DizzyingStrike_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot dizzyingStrikeButton;
    public bool dizzyingStrikeUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        dizzyingStrikeButton?.GetComponent<Button>().onClick.AddListener(UnlockDizzyingStrike);

    }
    public void UnlockDizzyingStrike(){
        if(dizzyingStrikeButton.unlock&&dizzyingStrikeUnlocked==false){
            dizzyingStrikeUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockDizzyingStrike();
    }
}
