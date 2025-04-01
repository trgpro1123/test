using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaFree_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot manaFreeButton;
    public bool manaFreeUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        manaFreeButton?.GetComponent<Button>().onClick.AddListener(UnlockManaFree);

    }
    public void UnlockManaFree(){
        if(manaFreeButton.unlock&&manaFreeUnlocked==false){
            manaFreeUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockManaFree();
    }
}
