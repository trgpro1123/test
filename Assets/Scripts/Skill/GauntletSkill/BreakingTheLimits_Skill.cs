using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakingTheLimits_Skill : Skill
{
    [Range(0,1)]
    public float healthDecreasePercent;
    [Range(0,1)]
    public float increaseAllStatsByPercent;
    public float duration;
    public float breakTheLimitsTimer;
    public bool isUsingSkill=false;

    [SerializeField] private UI_SkillTreeSlot breakingTheLimitsUnlockButton;
    public bool breakingTheLimitsUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        breakingTheLimitsUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockBreakingTheLimits);
    }
    public void UnlockBreakingTheLimits(){
        if(breakingTheLimitsUnlockButton.unlock){
            breakingTheLimitsUnlocked=true;
        }
    }


    
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            AudioManager.instance.PlaySFX(10);
            InceraseALlStats();
            UI.instance.ingameUI.CreateStatus(iconStatus,"Breaking The Limits",statusKey,duration);
            GameObject newBreakingTheLimits = Instantiate(skillObject,player.transform.position,Quaternion.identity);
            newBreakingTheLimits.transform.parent=player.transform;
            newBreakingTheLimits.GetComponent<BreakingTheLimits_Controller>().SetBreakingTheLimits(healthDecreasePercent,duration,breakTheLimitsTimer);
        }
        
    }
   
    private void InceraseALlStats(){
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.health.GetValue()),duration,playerStats.health);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.strength.GetValue()),duration,playerStats.strength);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.armor.GetValue()),duration,playerStats.armor);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.agility.GetValue()),duration,playerStats.agility);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.moveSpeed.GetValue()),duration,playerStats.moveSpeed);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.Lifesteal.GetValue()),duration,playerStats.Lifesteal);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.critChance.GetValue()),duration,playerStats.critChance);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.magicPower.GetValue()),duration,playerStats.magicPower);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.magicResistance.GetValue()),duration,playerStats.magicResistance);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.magicRegeneration.GetValue()),duration,playerStats.magicRegeneration);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.luck.GetValue()),duration,playerStats.luck);
        playerStats.IncreaseStatBy(Mathf.RoundToInt(increaseAllStatsByPercent*playerStats.coolDown.GetValue()),duration,playerStats.coolDown);
    }
    protected override void CheckUnlock()
    {
        UnlockBreakingTheLimits();
    }
    
}
