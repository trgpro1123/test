using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaPulse_Skill : Skill
{

    [SerializeField] private float forceKnockBack;
    [SerializeField] private float knockBackDuration;
    [SerializeField] private float speedExpand;
    [SerializeField] private float maxSize;

    [SerializeField] private UI_SkillTreeSlot manaPulseUnlockButton;
    public bool manaPulseUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        manaPulseUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockManaPulse);
    }
    public void UnlockManaPulse(){
        if(manaPulseUnlockButton.unlock){
            manaPulseUnlocked=true;
        }
    }


    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newManaPulse= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.identity);
            
        newManaPulse.GetComponent<ManaPulse_Controller>().SetManaPulse(forceKnockBack,knockBackDuration,speedExpand,maxSize);
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
        UnlockManaPulse();
    }
}
