using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuddenDeath_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot suddenDeathButton;
    public bool suddenDeathUnlocked{get; private set;}

    protected override void Start()
    {
        suddenDeathButton?.GetComponent<Button>().onClick.AddListener(UnlockSuddenDeath);
        base.Start();

    }
    public void UnlockSuddenDeath(){
        if(suddenDeathButton.unlock){
            suddenDeathUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockSuddenDeath();
    }
}
