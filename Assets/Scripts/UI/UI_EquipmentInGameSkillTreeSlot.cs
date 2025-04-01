using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipmentInGameSkillTreeSlot : MonoBehaviour
{

    public SkillInfo skillInfo;
    public Animator animator;
    public Image frameSkill;
    public Sprite frameSkillCanUse;
    public Sprite frameSkillCantNotUse;
    private Image skillIcon;
    private Image coolDownImage;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI skillCostText;
    public float timer=0;
    public bool isCooldown=false;
    private float skillCoolDown;
    private PlayerStats playerStats;
    private void Awake()
    {


        skillIcon=transform.GetChild(0).GetComponent<Image>();
        coolDownImage=transform.GetChild(1).GetComponent<Image>();
        timerText=transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        skillCostText=transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        skillIcon.color=new Color32(25,25,25,255);
        animator=GetComponent<Animator>();
        frameSkill=GetComponent<Image>();
        frameSkill.sprite=frameSkillCantNotUse;
        // isCooldown=false;
        timerText.text="";
        skillCostText.text="";
        
    }
    private void Start() {
        playerStats=PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.onManaUIChanged+=UIEnoughUseSkill;
        Invoke("UIEnoughUseSkill",0.1f);
        
    }
    private void Update() {
        if(timer>0&&isCooldown){
            timer-=Time.deltaTime;
            CheckCoolDown(skillCoolDown);
            if(timer>1){
                timerText.text=timer.ToString("0");
            }else{

                timerText.text=timer.ToString("0.0");
            }
        }
        else if(timer<0){
            timer=0;
            if(skillInfo.skill.CompareWeaponTypes())
                skillIcon.color=Color.white;
            timerText.text="";
            isCooldown=false;
            playerStats.onManaUIChanged?.Invoke();
        }
    }
    
    public void UpdateSlot(SkillInfo _skillInfo){
        if(skillInfo != null){
            skillInfo.skill.onSkillUsed -= StartCooldown;
        }
        skillInfo=_skillInfo;
        
        if(skillInfo!=null){
            skillInfo.skill.onSkillUsed += StartCooldown;
            UpdateSkillVisuals();
            
        }

        
    }
    public void ClearUpSlot(){
        if(skillInfo?.skill != null)
        {
            skillInfo.skill.onSkillUsed -= StartCooldown;
        }

        skillInfo = null;
        skillCostText.text="";
        if (skillIcon != null)
        {
            skillIcon.color = new Color32(25, 25, 25, 255);
            skillIcon.sprite = null;
            // skillCoolDown=0;
            frameSkill.sprite = frameSkillCantNotUse;
            // CleanUpSlotUI();
        }
    }
    
    private void UpdateSkillVisuals(){
        skillIcon.sprite=skillInfo.skillData.skillIcon;
        skillCostText.text=skillInfo.skill.skillCost.ToString();
        if(skillInfo.skill.CompareWeaponTypes())
        {
            CanUseSkillUI();
        }
        else{
            CanNotUseSkillUI();
        }
        UIEnoughUseSkill();
        
    }

    private void CanUseSkillUI()
    {
        frameSkill.sprite = frameSkillCanUse;
        animator.SetBool("CanUseSkill", true);
        if (isCooldown == false)
            skillIcon.color = Color.white;
    }
    private void CanNotUseSkillUI()
    {
        frameSkill.sprite = frameSkillCantNotUse;
        animator.SetBool("CanUseSkill", false);
        skillIcon.color = new Color32(25, 25, 25, 255);
    }

    private void SetCoolDown(){
        if(coolDownImage.fillAmount<=0)
            coolDownImage.fillAmount=1;
    }
    private void CheckCoolDown(float _cooldown){
        if(coolDownImage.fillAmount>0){
            coolDownImage.fillAmount-=(1/_cooldown)*Time.deltaTime;
        }
    }

    private void StartCooldown(float _cooldown)
    {
        SetCoolDown();
        skillIcon.color=new Color32(25,25,25,255);
        skillCoolDown=_cooldown;
        timer=_cooldown;
        isCooldown=true;

    }
    private void UIEnoughUseSkill(){
        if(skillInfo?.skill!=null&&isCooldown==false){
            if(skillInfo.skill.skillCost<=PlayerManager.instance.player.GetComponent<PlayerStats>().currentMana){
                coolDownImage.fillAmount=0;
            }
            else{
                coolDownImage.fillAmount=1;
            }
        }
    }
    private void OnEnable() {
        // playerStats.onManaUIChanged+=UIEnoughUseSkill;
    }
    private void OnDisable() {
        // playerStats.onManaUIChanged-=UIEnoughUseSkill;
    }
    
    
}
