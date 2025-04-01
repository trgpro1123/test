using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChronoControl_Skill : Skill
{
    public float skillSpeed;
    public float duration;
    [SerializeField] private GameObject effectChronoControl;


    [SerializeField] private UI_SkillTreeSlot chronoControlUnlockButton;
    public bool chronoControlUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        chronoControlUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockChronoControl);
    }
    public void UnlockChronoControl(){
        if(chronoControlUnlockButton.unlock){
            chronoControlUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(17);
        GameObject newEffectChronoControl= Instantiate(effectChronoControl,
            player.attackCheck.transform.position, Quaternion.identity);
        newEffectChronoControl.transform.parent=player.transform;
        GameObject newChronoControl= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.identity);
        newChronoControl.GetComponent<FrozenTimeZoneEffect>().SetFrozenTimeZoneEffect(duration,skillSpeed,skillDistance);
        newChronoControl.transform.parent=player.transform;
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            UseSkill();
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockChronoControl();
    }
}
