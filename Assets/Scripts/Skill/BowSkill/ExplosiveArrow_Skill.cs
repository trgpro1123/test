using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosiveArrow_Skill : Skill
{

    public float speed;
    public float lifeTime;
    public GameObject explosiveEffect;

    [SerializeField] private UI_SkillTreeSlot explosiveArrowUnlockButton;
    public bool explosiveArrowUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        explosiveArrowUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockExplosiveArrow);
    }
    public void UnlockExplosiveArrow(){
        if(explosiveArrowUnlockButton.unlock){
            explosiveArrowUnlocked=true;
        }
    }



    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(5);
        GameObject newArrow= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.Euler(0, 0, player.AnglePlayerToMouse()-90));
        newArrow.GetComponent<ExplosiveArrow>().SetExplosiveArrow(explosiveEffect,skillDamage, percentExtraDamageOfSkill,speed,lifeTime);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if (CanUseSkill()){
            player.archeryState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.archeryState);
        }

        
    }
    protected override void CheckUnlock()
    {
        UnlockExplosiveArrow();
    }
}
