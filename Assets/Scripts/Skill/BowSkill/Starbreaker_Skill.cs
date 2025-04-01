using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starbreaker_Skill : Skill
{

    public float timeCanUseSkill;
    public GameObject skillEffect;
    private int maxArrow=3;
    public int currentArrowLeft=0;
    private bool isCanUseSkill=true;

    [SerializeField] private UI_SkillTreeSlot starbreakerUnlockButton;
    public bool starbreakerUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        currentArrowLeft=maxArrow;
        starbreakerUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockStarbreaker);
    }
    public void UnlockStarbreaker(){
        if(starbreakerUnlockButton.unlock){
            starbreakerUnlocked=true;
        }
    }


    public override bool CanUseSkill()
    {
        if(canUseSkill==true&&CompareWeaponTypes()&&playerStats.currentMana>=skillCost){
            canUseSkill=false;
            if(player.skillManager.manaFreeSkill.manaFreeUnlocked){
                int random=Random.Range(0,100);
                if(random<=20){
                    playerStats.IncreaseManaBy(skillCost);
                }
            }
            playerStats.DecreaseManathBy(skillCost);
            if(player.skillManager.masterfulTechniqueSkill.masterfulTechniqueUnlocked){
                playerStats.IncreaseManaBy(Mathf.RoundToInt(skillCost*0.1f));
            }
            onSkillUsed?.Invoke(CooldownTimeCalculation());
            Invoke("ResetTimeUseSkill",CooldownTimeCalculation());
            isCanUseSkill=true;
            
            ingameUI.CreateStatus(iconStatus,"Starbreaker",statusKey,timeCanUseSkill);
            return true;
        }

        return false;

    }

    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(8);
        player.PlayerCreateAreaAttack(skillObject,StatType.magicPower, skillDamage,percentExtraDamageOfSkill, skillDistance, player.AnglePlayerToMouse());
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(canUseSkill==true){
            Invoke("ResetSkill",timeCanUseSkill);
        }
        if (CanUseSkill()==true||(CanUseSkill()==false&&isCanUseSkill)){
            GameObject newStarbreakerEffect=Instantiate(skillEffect,player.attackCheck.transform.position,Quaternion.identity);
            newStarbreakerEffect.transform.Rotate(0,0,Random.Range(0,360));
            currentArrowLeft--;
            currentArrowLeft=Mathf.Clamp(currentArrowLeft,0,maxArrow);


            player.archeryState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.archeryState);
            if(currentArrowLeft==0){
                ResetSkill();
            }
        }


        
    }
    private void ResetSkill()
    {
        currentArrowLeft=maxArrow;
        isCanUseSkill=false;
        ingameUI.DeleteStatus("Starbreaker");
    }
    protected override void CheckUnlock()
    {
        UnlockStarbreaker();
    }
}
