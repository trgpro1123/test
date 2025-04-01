using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot_Skill : Skill
{

    public float speed;
    public float lifeTime;
    public GameObject breakThroughArrow;
    public float breakThroughArrowSpeed;
    public float breakThroughArrowPercentExtraDamage;




    [SerializeField] private UI_SkillTreeSlot shotUnlockButton;
    public bool shotUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        shotUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockShot);
    }
    public void UnlockShot(){
        if(shotUnlockButton.unlock){
            shotUnlocked=true;
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
            newArrow.GetComponent<NormalArrow>().SetNormalArrow(skillDamage, percentExtraDamageOfSkill,speed,lifeTime,StatType.strength);
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
        newArrow.GetComponent<NormalArrow>().SetNormalArrow(skillDamage, percentExtraDamageOfSkill,speed,lifeTime,StatType.strength);
    }
    protected override void CheckUnlock()
    {
        UnlockShot();
    }
    
}
