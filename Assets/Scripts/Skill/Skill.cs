using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillWeaponType
{
    Sword,
    Gauntlet,
    Bow,
    Magic
}
public class Skill : MonoBehaviour
{
    public System.Action<float> onSkillUsed;
    public GameObject skillObject;
    public int skillDamage;
    public float percentExtraDamageOfSkill;
    public int skillCost;
    public int skillDistance;
    public float cooldown;
    public SkillWeaponType skillWeaponType;
    public bool canUseSkill=true;
    public Sprite iconStatus;
    [TextArea(3,3)]
    public string statusDescription;
    public string statusKey;


    
    protected Player player;
    protected SkillManager skillManager;
    protected PlayerStats playerStats;
    protected UI_Ingame ingameUI;

    protected virtual void Awake()
    {
        Getvalue();

    }

    private void Getvalue()
    {
        player = PlayerManager.instance.player;
        skillManager = SkillManager.instance;
        playerStats = player.GetComponent<PlayerStats>();
    }

    protected virtual void Start() {
        Invoke("CheckUnlock",0.1f);
        canUseSkill=true;
        ingameUI=UI.instance.ingameUI;
    }
    protected virtual void Update() {

    }
    public virtual bool CanUseSkill(){

        if(player==null||playerStats==null||player.skillManager==null) Getvalue();
        if(canUseSkill==true&&CompareWeaponTypes()&&playerStats.currentMana>=skillCost){
            canUseSkill=false;
            if(player.skillManager.manaFreeSkill.manaFreeUnlocked){
                int random=Random.Range(0,100);
                if(random<=20){
                    playerStats.IncreaseManaBy(skillCost);
                }
            }
            playerStats.DecreaseManathBy(skillCost);
            if(player.skillManager.masterfulTechniqueSkill.masterfulTechniqueUnlocked){
                playerStats.IncreaseManaBy(Mathf.RoundToInt(skillCost*0.1f));
            }
            onSkillUsed?.Invoke(CooldownTimeCalculation());
            Invoke("ResetTimeUseSkill",CooldownTimeCalculation());
            return true;
        }
        AudioManager.instance.PlaySFX(22);
        return false;
    }
    public virtual void UseSkill(){
        
    }
    public virtual void ActiveSkill(){
        player.FlipPlayer();
    }
    public Transform FindClosestEnemy(Transform _transform){
        Collider2D []colliders=Physics2D.OverlapCircleAll(_transform.position,25);
        float closestDistance=Mathf.Infinity;
        Transform closestEnemy=null;
        foreach(var hit in colliders){
            if(hit.GetComponent<Enemy>()!=null){
                float distanceToEnemy=Vector2.Distance(_transform.position,hit.transform.position);
                if(distanceToEnemy<closestDistance){
                    closestDistance=distanceToEnemy;
                    closestEnemy=hit.transform;

                }
            }
        }
        return closestEnemy;
    }
    public virtual void ResetTimeUseSkill(){
        canUseSkill=true;
    }
    protected virtual void CheckUnlock(){

    }
    public bool CompareWeaponTypes()
    {

        if(player==null) player=PlayerManager.instance.player;
        if(skillWeaponType==SkillWeaponType.Magic){
            return true;
        }
        if(player.currentWeapon==CurrentWeapon.None) return false;
        return player.currentWeapon.ToString()==skillWeaponType.ToString();
    }
    public float CooldownTimeCalculation(){
        return cooldown-cooldown*((float)playerStats.coolDown.GetValue()/100);
    }

}
