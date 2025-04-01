using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrenziedFury_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot frenziedFuryButton;
    public bool frenziedFuryUnlocked{get; private set;}

    protected override void Start()
    {
        frenziedFuryButton?.GetComponent<Button>().onClick.AddListener(UnlockFrenziedFury);
        base.Start();

    }
    public void UnlockFrenziedFury(){
        if(frenziedFuryButton.unlock){
            frenziedFuryUnlocked=true;
        }
    }
    public void ActiveStatus(){
        ingameUI.CreateStatus(iconStatus,"Frenzied Fury",statusKey,0);
    }
    public void DeleteStatus(){
        ingameUI.DeleteStatus("Frenzied Fury");
    }
    protected override void CheckUnlock()
    {
        UnlockFrenziedFury();
    }
}
