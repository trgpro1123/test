using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResoluteFortress_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot resoluteFortressButton;
    public bool resoluteFortressUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        resoluteFortressButton?.GetComponent<Button>().onClick.AddListener(UnlockResoluteFortress);

    }
    public void UnlockResoluteFortress(){
        if(resoluteFortressButton.unlock&&resoluteFortressUnlocked==false){
            resoluteFortressUnlocked=true;
        }
    }
    public void ActiveStatus(){
        ingameUI.CreateStatus(iconStatus,"Resolute Fortress",statusKey,0);
    }
    public void DeleteStatus(){
        ingameUI.DeleteStatus("Resolute Fortress");
    }
    protected override void CheckUnlock()
    {
        UnlockResoluteFortress();
    }
}
