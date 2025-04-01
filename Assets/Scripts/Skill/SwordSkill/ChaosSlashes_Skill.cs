using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaosSlashes_Skill : Skill
{

    public float duration;
    public float chaosSlashesTimer;
    private ChaosSlashes_Controller chaosSlashes_Controller;


    [SerializeField] private UI_SkillTreeSlot chaosSlashesUnlockButton;
    public bool chaosSlashesUnlocked{get;private set;}


    protected override void Start()
    {
        chaosSlashesUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockChaosSlashes);
        base.Start();
    }
    public void UnlockChaosSlashes(){
        if(chaosSlashesUnlockButton.unlock){
            chaosSlashesUnlocked=true;
        }
    }
    
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if(CanUseSkill()){
            player.stateMachine.ChangeState(player.chaosSlashesState);
        }
        
    }
    public void CreateChaosSlashAnimation()
    {
        float angle = player.AnglePlayerToMouse();
        GameObject newChaosSlash=Instantiate(skillObject, player.attackCheck.transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(skillDistance, 0, 0),
            Quaternion.Euler(0, 0, angle));
        chaosSlashes_Controller=newChaosSlash.GetComponent<ChaosSlashes_Controller>();
        chaosSlashes_Controller.SetChaosSlashes(skillDamage, percentExtraDamageOfSkill, skillDistance,duration,chaosSlashesTimer, angle, chaosSlashes_Controller.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>(),chaosSlashes_Controller.gameObject);
        RoutateSpriteRendererY(newChaosSlash);
    }

    public void RoutateSpriteRendererY(GameObject _object){
        if(player.IsFacingRight()==false){
            _object.transform.Rotate(0,180,0);
            for (int i = 0; i < _object.transform.childCount; i++)
            {
                _object.transform.GetChild(i).GetComponent<SpriteRenderer>().flipY=true;
                _object.transform.GetChild(i).GetComponent<SpriteRenderer>().flipX=true;
            }

        }


    }
    protected override void CheckUnlock()
    {
        UnlockChaosSlashes();
    }
}
