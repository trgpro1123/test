using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CometDrop_Skill : Skill
{

    public float speed;
    public float lifeTime;
    public GameObject fakeArrow;
    public GameObject explosionEffect;
    public GameObject earthCrack;
    public float earthCrackDamage;
    public float earthCrackDuration;
    public int earthCrackDamageTimer;
    public float explosionSizeEffect;
    

    private Vector2 mouseWorldPosition;
    
    [SerializeField] private UI_SkillTreeSlot cometDropUnlockButton;
    public bool cometDropUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        cometDropUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockCometDrop);
    }
    public void UnlockCometDrop(){
        if(cometDropUnlockButton.unlock){
            cometDropUnlocked=true;
        }
    }




    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(5);
        GameObject newArrow= Instantiate(fakeArrow,
            player.attackCheck.transform.position, Quaternion.identity);
        newArrow.GetComponent<FakeArrow>().SetFakeArrow(speed,lifeTime);
        Invoke("DropArrow", 0.5f);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if (CanUseSkill()){
            player.archeryState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.archeryState);
            Vector2 mouseScreenPosition = Input.mousePosition;
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            
        }

        
    }
    

    public void DropArrow()
    {
        GameObject newArrow= Instantiate(skillObject,
            mouseWorldPosition+new Vector2(0,10), Quaternion.Euler(0,0,180));
        newArrow.GetComponent<CometDropArrow>().SetCometDropArrow(skillDamage, percentExtraDamageOfSkill,speed,lifeTime,
            mouseWorldPosition,explosionEffect,earthCrack,earthCrackDamage,earthCrackDamageTimer,earthCrackDuration,explosionSizeEffect);
    }
    protected override void CheckUnlock()
    {
        UnlockCometDrop();
    }
}
