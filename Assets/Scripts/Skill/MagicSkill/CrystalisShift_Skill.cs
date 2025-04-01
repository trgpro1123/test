using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalisShift_Skill : Skill
{
    public float duration;
    public float radius;
    public GameObject explosionEffect;


    [SerializeField] private UI_SkillTreeSlot crystalisShiftUnlockButton;
    public bool crystalisShiftUnlocked{get;private set;}

    private GameObject currentCrystal;

    


    protected override void Start()
    {
        base.Start();
        currentCrystal=null;
        crystalisShiftUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockCrystalisShift);
    }
    public void UnlockCrystalisShift(){
        if(crystalisShiftUnlockButton.unlock){
            crystalisShiftUnlocked=true;
        }
    }

    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(currentCrystal!=null){
            SwapPosition();
            currentCrystal.GetComponent<CrystalisShift_Controller>().CreateExplosion();
            return;
        }
        if(CanUseSkill()&&currentCrystal==null){
            CreateCrystal();
            UI.instance.ingameUI.CreateStatus(iconStatus,"Crystalis Shift",statusKey,duration);
        }
        
    }
    
    public void CreateCrystal()
    {
        currentCrystal=Instantiate(skillObject,player.attackCheck.transform.position, Quaternion.identity);
        currentCrystal.GetComponent<CrystalisShift_Controller>().SetCrystalisShift(skillDamage, percentExtraDamageOfSkill,duration,radius,explosionEffect);
    }
    public void SwapPosition()
    {
        Vector3 playerPosition=player.transform.position;
        player.transform.position=currentCrystal.transform.position;
        currentCrystal.transform.position=playerPosition;
        Destroy(currentCrystal);
    }
    protected override void CheckUnlock()
    {
        UnlockCrystalisShift();
    }
}
