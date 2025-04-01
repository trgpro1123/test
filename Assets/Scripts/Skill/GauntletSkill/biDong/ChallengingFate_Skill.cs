using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengingFate_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot challengingFateButton;
    public bool challengingFateUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        challengingFateButton?.GetComponent<Button>().onClick.AddListener(UnlockChallengingFate);

    }
    public void UnlockChallengingFate(){
        if(challengingFateButton.unlock&&challengingFateUnlocked==false){
            challengingFateUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockChallengingFate();
    }
}
