using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeismicShockwave_Skill : Skill
{

    public GameObject SeismicShockwaveObject;
    public float originalSize;
    public float percentSizeForEachInitialization;
    public float percentageDamagePerInitialization;
    public float timeDelay;
    public int numberOfSeismicShockwave;
    public float moveSpeed;
    public float durationJump;

    [SerializeField] private UI_SkillTreeSlot seismicShockwaveUnlockButton;
    public bool seismicShockwaveUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        seismicShockwaveUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockSeismicShockwave);
    }
    public void UnlockSeismicShockwave(){
        if(seismicShockwaveUnlockButton.unlock){
            seismicShockwaveUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(14);
        GameObject newSeismicShockwave = Instantiate(skillObject, player.transform.position, Quaternion.identity);
        newSeismicShockwave.GetComponent<SeismicShockwave_Controller>().SetSeismicShockwave(SeismicShockwaveObject,originalSize,percentSizeForEachInitialization,percentageDamagePerInitialization,timeDelay,numberOfSeismicShockwave,skillDamage,percentExtraDamageOfSkill);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.stateMachine.ChangeState(player.seismicShockwaveSkillState);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockSeismicShockwave();
    }
}
