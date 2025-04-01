using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerfectState_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot perfectStateButton;
    public bool perfectStateUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        perfectStateButton?.GetComponent<Button>().onClick.AddListener(UnlockPerfectState);

    }
    public void UnlockPerfectState(){
        if(perfectStateButton.unlock&&perfectStateUnlocked==false){
            perfectStateUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockPerfectState();
    }
}
