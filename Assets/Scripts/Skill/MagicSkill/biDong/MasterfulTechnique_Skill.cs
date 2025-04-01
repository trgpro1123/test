using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterfulTechnique_Skill : PassiveSkill
{
    [SerializeField] private UI_SkillTreeSlot masterfulTechniqueButton;
    public bool masterfulTechniqueUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        masterfulTechniqueButton?.GetComponent<Button>().onClick.AddListener(UnlockMasterfulTechnique);

    }
    public void UnlockMasterfulTechnique(){
        if(masterfulTechniqueButton.unlock&&masterfulTechniqueUnlocked==false){
            masterfulTechniqueUnlocked=true;
        }
    }
    protected override void CheckUnlock()
    {
        UnlockMasterfulTechnique();
    }
}
