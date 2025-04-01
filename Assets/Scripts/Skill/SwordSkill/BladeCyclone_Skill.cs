using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BladeCyclone_Skill : Skill
{
    [SerializeField] private float growSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedRotation;
    [SerializeField] private float duration;
    [SerializeField] private float maxSize;
    [SerializeField] private float damageTimer;
    [SerializeField] private float timeLife;



    [SerializeField] private UI_SkillTreeSlot bladeCycloneUnlockButton;
    public bool bladeCycloneUnlocked{get;private set;}


    protected override void Start()
    {
        bladeCycloneUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockBladeCyclone);
        base.Start();
    }
    public void UnlockBladeCyclone(){
        if(bladeCycloneUnlockButton.unlock){
            bladeCycloneUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newBlade= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.Euler(0, 0, player.AnglePlayerToMouse()));
        newBlade.GetComponentInChildren<BladeCyclone_Controller>().SetBladeCyclone(skillDamage,percentExtraDamageOfSkill,damageTimer,moveSpeed,growSpeed,speedRotation,timeLife,duration,maxSize);
        AudioManager.instance.PlaySFX(3);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.bladeCycloneState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.bladeCycloneState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockBladeCyclone();
    }
}
