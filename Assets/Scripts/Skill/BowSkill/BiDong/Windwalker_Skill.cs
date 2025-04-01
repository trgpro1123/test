using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Windwalker_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot windwalkerButton;
    public bool windwalkerUnlocked{get; private set;}
    private Button button;

    protected override void Start()
    {
        base.Start();
        button=GetComponent<Button>();
        button?.GetComponent<Button>().onClick.AddListener(UnlockWindwalker);

    }
    public void UnlockWindwalker(){
        if(button==null) return;
        if(windwalkerButton.unlock&&windwalkerUnlocked==false){
            player.charaterStats.moveSpeed.AddModifier(1);
            Inventory.instance.UpdateStatsUI();
            windwalkerUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockWindwalker();
    }
}
