using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolleyOfArrows_Skill : Skill
{
    public float speed;
    public float lifeTime;

    public int numberProjecttiles;
    [Range(0,359)]  public float angleSpread;
    public float startingDistance=0.2f;
    [SerializeField] private UI_SkillTreeSlot volleyOfArrowsUnlockButton;
    public bool volleyOfArrowsUnlocked{get;private set;}


    protected override void Start()
    {
        base.Start();
        volleyOfArrowsUnlockButton?.GetComponent<Button>().onClick.AddListener(UnlockVolleyOfArrows);
    }
    public void UnlockVolleyOfArrows(){
        if(volleyOfArrowsUnlockButton.unlock){
            volleyOfArrowsUnlocked=true;
        }
    }



    public override void UseSkill()
    {
        base.UseSkill();
        CreateVolleyOfArrows();
        AudioManager.instance.PlaySFX(5);
    }
    public override void ActiveSkill()
    {
        base.ActiveSkill();
        if (CanUseSkill()){
            player.archeryState.SetCurrentSkill(UseSkill);
            player.stateMachine.ChangeState(player.archeryState);
        }

        
    }


    private void CreateVolleyOfArrows()
    {
        float startAngle,currentAngle,angleStep,endAngle;
        TargetConeOfInfluence(out startAngle,out endAngle,out currentAngle,out angleStep);
            for (int j = 0; j < numberProjecttiles; j++)
            {
                Vector2 pos= FindBulletSpawnPoint(currentAngle);
                GameObject newArrow = Instantiate(skillObject, pos, Quaternion.identity);
                newArrow.GetComponent<NormalArrow>().SetNormalArrow(skillDamage, percentExtraDamageOfSkill,speed,lifeTime,StatType.strength);
                newArrow.transform.rotation = Quaternion.Euler(0, 0, currentAngle-90);
                currentAngle+=angleStep;
            }
            currentAngle=startAngle; 
            Debug.Log(numberProjecttiles+" arrows created");
    }

    private void TargetConeOfInfluence(out float startAngle,out float endAngle,out float currentAngle,out float angleStep)
    {
        Vector3 mousePosition=Input.mousePosition;
        mousePosition=Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 targetDirection = mousePosition - player.attackCheck.transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float haftAngleSpread = 0f;
        angleStep = 0f;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (numberProjecttiles - 1);
            haftAngleSpread = angleSpread / 2;
            startAngle = targetAngle - haftAngleSpread;
            endAngle = targetAngle + haftAngleSpread;
            currentAngle = startAngle;

        }
    }


    private Vector2 FindBulletSpawnPoint(float currentAngle){
        float x=player.attackCheck.transform.position.x+startingDistance*Mathf.Cos(currentAngle*Mathf.Deg2Rad);
        float y=player.attackCheck.transform.position.y+startingDistance*Mathf.Sin(currentAngle*Mathf.Deg2Rad);
        
        Vector2 pos=new Vector2(x,y);
        return pos;
    }
    protected override void CheckUnlock()
    {
        UnlockVolleyOfArrows();
    }
}
