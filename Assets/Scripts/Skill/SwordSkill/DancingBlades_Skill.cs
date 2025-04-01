using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DancingBlades_Skill : Skill
{

    public GameObject blade; 
    public int numberOfBlades;
    public float rotationSpeed;

    public float duration;


    [SerializeField] private UI_SkillTreeSlot dancingBladesUnlockButton;
    public bool dancingBladesUnlocked{get;private set;}


    protected override void Start()
    {
        dancingBladesUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockDancingBlades);
        base.Start();
    }
    public void UnlockDancingBlades(){
        if(dancingBladesUnlockButton.unlock){
            dancingBladesUnlocked=true;
        }
    }

    
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            GameObject newDancingBlades = Instantiate(skillObject, player.attackCheck.position, Quaternion.identity);
            newDancingBlades.GetComponent<DancingBlades_Controller>().SetDancingBlades(player.attackCheck,blade,numberOfBlades, rotationSpeed, skillDistance, skillDamage, percentExtraDamageOfSkill);
            newDancingBlades.transform.parent = player.transform;
            Destroy(newDancingBlades, duration);
        }
        
    }
    protected override void CheckUnlock()
    {
        UnlockDancingBlades();
    }
}
