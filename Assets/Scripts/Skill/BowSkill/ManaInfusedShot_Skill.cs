using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaInfusedShot_Skill : Skill
{

    public float speed;
    public float lifeTime;
    public GameObject breakThroughArrow;
    public float breakThroughArrowSpeed;
    public float breakThroughArrowPercentExtraDamage;

    [SerializeField] private UI_SkillTreeSlot manaInfusedShotUnlockButton;
    public bool manaInfusedShotUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        manaInfusedShotUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockManaInfusedShot);
    }
    public void UnlockManaInfusedShot(){
        if(manaInfusedShotUnlockButton.unlock){
            manaInfusedShotUnlocked=true;
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
        if(SkillManager.instance.breakthroughShotSkill.breakthroughShotUnlocked&& Random.Range(0,100)<=30){
            BreakThroughShot();
            AudioManager.instance.PlaySFX(5);
        }
        else{
            CreateArrow();
            AudioManager.instance.PlaySFX(5);
        }

        if(SkillManager.instance.quickdrawSkill.quickdrawUnlocked){
            Invoke("RandomCreateArrow",0.15f);
        }
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if (CanUseSkill()){
            player.archeryState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.archeryState);
        }

        
    }
    public void RandomCreateArrow(){
        if(Random.Range(0,100)<=30){
            AudioManager.instance.PlaySFX(5);
            GameObject newArrow= Instantiate(skillObject,
                player.attackCheck.transform.position, Quaternion.Euler(0, 0, player.AnglePlayerToMouse()-90));
            newArrow.GetComponent<NormalArrow>().SetNormalArrow(skillDamage, percentExtraDamageOfSkill,speed,lifeTime,StatType.magicPower);
        }
    }
    public void BreakThroughShot(){
        GameObject newArrow= Instantiate(breakThroughArrow,
            player.attackCheck.transform.position, Quaternion.Euler(0, 0, player.AnglePlayerToMouse()-90));
        newArrow.GetComponent<BreakthroughArrow>().SetBreakthroughArrow(skillDamage, breakThroughArrowPercentExtraDamage,breakThroughArrowSpeed,lifeTime);
    }
    public void CreateArrow(){
        GameObject newArrow= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.Euler(0, 0, player.AnglePlayerToMouse()-90));
        newArrow.GetComponent<NormalArrow>().SetNormalArrow(skillDamage, percentExtraDamageOfSkill,speed,lifeTime,StatType.magicPower);
    }
    protected override void CheckUnlock()
    {
        UnlockManaInfusedShot();
    }
}
