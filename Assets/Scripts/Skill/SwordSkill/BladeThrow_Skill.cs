using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BladeThrow_Skill : Skill
{
    [SerializeField] private float speedRotation;
    [SerializeField] private float swordSpeed;
    [SerializeField] private float duration;
    private float angle;

    [SerializeField] private UI_SkillTreeSlot bladeThrowUnlockButton;
    public bool bladeThrowUnlocked{get;private set;}


    protected override void Start()
    {
        bladeThrowUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockBladeThrow);
        base.Start();
    }
    public void UnlockBladeThrow(){
        if(bladeThrowUnlockButton.unlock){
            bladeThrowUnlocked=true;
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newBlade= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.Euler(0, 0, player.AnglePlayerToMouse()));
        newBlade.GetComponentInChildren<BladeThrow_Controller>().SetBladeThrow(skillDamage,percentExtraDamageOfSkill,speedRotation,swordSpeed);
        Destroy(newBlade,duration);
        AudioManager.instance.PlaySFX(3);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.bladeThrowState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.bladeThrowState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockBladeThrow();
    }
    
}
