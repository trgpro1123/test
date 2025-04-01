using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverwhelmingPower_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot overwhelmingPowerButton;
    public bool overwhelmingPowerUnlocked{get; private set;}

    protected override void Start()
    {
        overwhelmingPowerButton?.GetComponent<Button>().onClick.AddListener(UnlockOverwhelmingPower);
        base.Start();

    }
    public void UnlockOverwhelmingPower(){
        if(overwhelmingPowerButton.unlock){
            overwhelmingPowerUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockOverwhelmingPower();
    }
}
