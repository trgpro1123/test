using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeReap_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot lifeReapButton;
    public bool lifeReapUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        lifeReapButton?.GetComponent<Button>().onClick.AddListener(UnlockLifeReap);

    }
    public void UnlockLifeReap(){
        if(lifeReapButton.unlock&&lifeReapUnlocked==false){
            lifeReapUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockLifeReap();
    }
}
