using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doppelgänger_Skill : Skill
{

    public float doppelgängerDuration;
    public GameObject arrowObject;
    public float arrowSpeed;
    public float arrowLifeTime;
    public float timeCloneAttack;

    [SerializeField] private UI_SkillTreeSlot doppelgängerUnlockButton;
    public bool doppelgängerUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        doppelgängerUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockDoppelgänger);
    }
    public void UnlockDoppelgänger(){
        if(doppelgängerUnlockButton.unlock){
            doppelgängerUnlocked=true;
        }
    }

    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            GameObject newDoppelgänger =Instantiate(skillObject, player.transform.position, Quaternion.identity);
            newDoppelgänger.GetComponent<Doppelgänger_Controller>().SetDoppelgänger(arrowObject,
                    skillDamage,percentExtraDamageOfSkill,arrowSpeed,arrowLifeTime,doppelgängerDuration,skillDistance,timeCloneAttack);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockDoppelgänger();
    }
}
