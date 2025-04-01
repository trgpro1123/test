using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthquakeJump_Skill : Skill
{

    public float durationJump;
    public float size;
    public float moveSpeed;
    [SerializeField] private UI_SkillTreeSlot earthquakeJumpUnlockButton;
    public bool earthquakeJumpUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        earthquakeJumpUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockEarthquakeJump);
    }
    public void UnlockEarthquakeJump(){
        if(earthquakeJumpUnlockButton.unlock){
            earthquakeJumpUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(14);
        GameObject newEarthquakeJump = Instantiate(skillObject, player.transform.position, Quaternion.identity);
        newEarthquakeJump.GetComponent<EarthquakeJump_Controller>().SetEarthquakeJump(skillDamage,percentExtraDamageOfSkill,size);

    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.stateMachine.ChangeState(player.earthquakeJumpSkillState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockEarthquakeJump();
    }
}
