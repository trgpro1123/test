using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoundOfTheArrow_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot woundOfTheArrowButton;
    public bool woundOfTheArrowUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        woundOfTheArrowButton?.GetComponent<Button>().onClick.AddListener(UnlockWoundOfTheArrow);

    }
    public void UnlockWoundOfTheArrow(){
        if(woundOfTheArrowButton.unlock&&woundOfTheArrowUnlocked==false){
            woundOfTheArrowUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockWoundOfTheArrow();
    }
}
