using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwiftReflexes_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot swiftReflexesButton;
    public bool swiftReflexesUnlocked{get; private set;}
    private Button button;

    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.GetComponent<Button>().onClick.AddListener(UnlockSwiftReflexes);

    }
    public void UnlockSwiftReflexes(){
        if(button==null) return;
        if(swiftReflexesButton.unlock&&swiftReflexesUnlocked==false){
            player.charaterStats.agility.AddModifier(10);
            Inventory.instance.UpdateStatsUI();
            swiftReflexesUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockSwiftReflexes();
    }
}
