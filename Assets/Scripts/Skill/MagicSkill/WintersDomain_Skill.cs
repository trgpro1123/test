using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WintersDomain_Skill : Skill
{
    public float duration;
    public float coldPercent;

    [SerializeField] private GameObject effectWintersDomain;


    [SerializeField] private UI_SkillTreeSlot wintersDomainUnlockButton;
    public bool wintersDomainUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        wintersDomainUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockWintersDomain);
    }
    public void UnlockWintersDomain(){
        if(wintersDomainUnlockButton.unlock){
            wintersDomainUnlocked=true;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newEffectWintersDomain= Instantiate(effectWintersDomain,
            player.attackCheck.transform.position, Quaternion.identity);
        newEffectWintersDomain.transform.parent=player.transform;
        GameObject newWintersDomain= Instantiate(skillObject,
            player.attackCheck.transform.position, Quaternion.identity);
        newWintersDomain.GetComponent<WintersDomain_Controller>().SetWinterDomainEffect(duration,coldPercent,skillDistance);
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
        UnlockWintersDomain();
    }
}
