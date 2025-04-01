using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyAbsorptio_Skill : Skill
{

    [SerializeField] private UI_SkillTreeSlot energyAbsorptioUnlockButton;
    public bool energyAbsorptioUnlocked{get;private set;}



    protected override void Start()
    {
        base.Start();
        energyAbsorptioUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockEnergyAbsorptio);
    }
    public void UnlockEnergyAbsorptio(){
        if(energyAbsorptioUnlockButton.unlock){
            energyAbsorptioUnlocked=true;
        }
    }


    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            AudioManager.instance.PlaySFX(15);
            playerStats.IncreaseManaBy(Mathf.RoundToInt(playerStats.GetMaxMana()*0.1f));
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockEnergyAbsorptio();
    }
}
