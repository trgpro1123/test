using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forcewave_Skill : Skill
{
    public float stunnDuration;
    [Range(0,1)]
    public float slowPercent;
    public float slowDuration;
    public float size;

    [SerializeField] private UI_SkillTreeSlot forcewaveUnlockButton;
    public bool forcewaveUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        forcewaveUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockForcewave);
    }
    public void UnlockForcewave(){
        if(forcewaveUnlockButton.unlock){
            forcewaveUnlocked=true;
        }
    }


    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newForcewave= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.identity);
        newForcewave.GetComponent<Forcewave_Controller>().SetForcewave(stunnDuration,slowPercent,slowDuration,size);
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
        UnlockForcewave();
    }
}
