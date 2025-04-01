using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateExplosion_Skill : Skill
{

    public float duration;
    public GameObject UltimateExplosionEffect;

    [SerializeField] private UI_SkillTreeSlot ultimateExplosionUnlockButton;
    public bool ultimateExplosionUnlocked{get;private set;}

    private GameObject newUltimateExplosionEffect;



    protected override void Start()
    {
        base.Start();
        ultimateExplosionUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockUltimateExplosion);
    }
    public void UnlockUltimateExplosion(){
        if(ultimateExplosionUnlockButton.unlock){
            ultimateExplosionUnlocked=true;
        }
    }



    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.stateMachine.ChangeState(player.ultimateExplosionSkillState);
            CreateUltimateExplosionEffect();
        }
        
    }
    public void CreateUltimateExplosion(){
        GameObject newUltimateExplosion=Instantiate(skillObject,player.attackCheck.transform.position,Quaternion.identity);
        newUltimateExplosion.GetComponent<UltimateExplosion_Controller>().SetUltimateExplosion(skillDamage,percentExtraDamageOfSkill,skillDistance);        
    }
    public void CreateUltimateExplosionEffect(){
        GameObject newUltimateExplosionEffect=Instantiate(UltimateExplosionEffect,player.attackCheck.transform.position,Quaternion.identity);
        newUltimateExplosionEffect.GetComponent<UltimateExplosionEffect>().SetUlUltimateExplosionEffect(duration);
    }
    protected override void CheckUnlock()
    {
        UnlockUltimateExplosion();
    }

}
