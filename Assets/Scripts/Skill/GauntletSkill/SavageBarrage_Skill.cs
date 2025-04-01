using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavageBarrage_Skill : Skill
{

    public float duration;
    public float savageBarrageTimer;
    private SavageBarrage_Controller savageBarrage_Controller;

    [SerializeField] private UI_SkillTreeSlot savageBarrageUnlockButton;
    public bool savageBarrageUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        savageBarrageUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockSavageBarrage);
    }
    public void UnlockSavageBarrage(){
        if(savageBarrageUnlockButton.unlock){
            savageBarrageUnlocked=true;
        }
    }
    
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.stateMachine.ChangeState(player.savageBarrageSkillState);
        }
        
    }
    public void CreateSavageBarrageAnimation()
    {
        float angle = player.AnglePlayerToMouse();
        GameObject newSavageBarrage=Instantiate(skillObject, player.attackCheck.transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(skillDistance, 0, 0),
            Quaternion.Euler(0, 0, angle));
        savageBarrage_Controller=newSavageBarrage.GetComponent<SavageBarrage_Controller>();
        savageBarrage_Controller.SetSavageBarrage(skillDamage, percentExtraDamageOfSkill, skillDistance,duration,savageBarrageTimer, angle, savageBarrage_Controller.transform.GetChild(4).GetComponentInChildren<SpriteRenderer>(),savageBarrage_Controller.gameObject);
        RoutateSpriteRendererY(newSavageBarrage);
    }
    public bool SkillCompleted(){
        if(savageBarrage_Controller.gameObject!=null&&savageBarrage_Controller.GetComponent<SavageBarrage_Controller>().canDestroy==true){
            return true;
        }
        return false;
    }
    public void DestroySavageBarrage(float _time=0){
        if(savageBarrage_Controller.gameObject!=null)
            Destroy(savageBarrage_Controller.gameObject,_time);
    }
    public void RoutateSpriteRendererY(GameObject _object){
        if(player.IsFacingRight()==false){
            _object.transform.Rotate(0,180,0);
            for (int i = 0; i < _object.transform.childCount; i++)
            {
                _object.transform.GetChild(i).GetComponent<SpriteRenderer>().flipY=true;
            }

        }


    }
    protected override void CheckUnlock()
    {
        UnlockSavageBarrage();
    }
}
