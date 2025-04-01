using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quickdraw_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot quickdrawButton;
    public bool quickdrawUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        quickdrawButton?.GetComponent<Button>().onClick.AddListener(UnlockQuickdraw);

    }
    public void UnlockQuickdraw(){
        if(quickdrawButton.unlock&&quickdrawUnlocked==false){
            quickdrawUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockQuickdraw();
    }
}
