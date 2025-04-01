using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakthroughShot_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot breakthroughShotButton;
    public bool breakthroughShotUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        breakthroughShotButton?.GetComponent<Button>().onClick.AddListener(UnlockBreakthroughShot);

    }
    public void UnlockBreakthroughShot(){
        if(breakthroughShotButton.unlock&&breakthroughShotUnlocked==false){
            breakthroughShotUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockBreakthroughShot();
    }
}
