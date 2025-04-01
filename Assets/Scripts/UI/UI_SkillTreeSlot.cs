using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SkillTreeSlot : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler,ISaveManager
{

    [SerializeField] protected UI_SkillTreeSlot[] shouldBeUnlock;
    [SerializeField] protected Skill baseSkill;

    public SkillInfo skillInfo;
    public bool unlock;


    public Image image;
    public UI ui;

    
    protected virtual void Awake() {
        GetComponent<Button>().onClick.AddListener(()=> UnlockSkillSLot());
    }
    protected virtual void Start() {
        image=GetComponent<Image>();
        ui=GetComponentInParent<UI>();

        image.color=Color.gray;
        if(unlock)
            image.color=Color.white;
        
        baseSkill=GetComponent<Skill>();

        if(baseSkill!=null){
            Skill skill = (Skill)SkillManager.instance.GetComponent(baseSkill.GetType());
            skillInfo.skill=skill;
        }



    }

    public void UpdateData(){
        baseSkill=GetComponent<Skill>();
        if(baseSkill!=null){
            Skill skill = (Skill)SkillManager.instance.GetComponent(baseSkill.GetType());
            skillInfo.skill=skill;
        }
    }
    public virtual void UnlockSkillSLot(){
        if(PlayerManager.instance.HaveEnoughMoney(skillInfo.skillData.skillPrice)==false){
            AudioManager.instance.PlaySFX(22);
            return;
        } 
        AudioManager.instance.PlaySFX(23);
        bool canUnlock=false;
        for (int i = 0; i < shouldBeUnlock.Length; i++)
        {
            if(shouldBeUnlock[i].unlock==true) canUnlock=true;
        }
        if(canUnlock==true||shouldBeUnlock.Length==0){
            unlock=true;
            image.color=Color.white;
        }


    }
    

    public virtual void OnPointerDown(PointerEventData eventData)
    {

        if (skillInfo==null) return;
        if(Input.GetKey(KeyCode.Mouse0)){

            return;
        }
        if(Input.GetKey(KeyCode.Mouse1)&&skillInfo.skillData.skillType==SkillType.Active){
            if(unlock==false) return;
            AudioManager.instance.PlaySFX(23);
            SkillInfo newSKillInfo=new SkillInfo(skillInfo.skillData,skillInfo.skill);
            SkillManager.instance.AddSkill(newSKillInfo);
            return;
        }

    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (skillInfo==null||skillInfo.skillData==null) return;
            ui.skillTooltip.ShowSkillTreeTooltip(skillInfo);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        ui.skillTooltip.HideSkillTreeTooltip();
    }

    public virtual void SaveData(ref GameData _data)
    {

        if (skillInfo?.skillData == null)
        {
            return;
        }
        if(_data.skillTree.TryGetValue(skillInfo.skillData.skillName, out bool value)){

            _data.skillTree.Remove(skillInfo.skillData.skillName);
            _data.skillTree.Add(skillInfo.skillData.skillName,unlock);
        }
        else
            _data.skillTree.Add(skillInfo.skillData.skillName,unlock);



    }

    public virtual void LoadData(GameData _data)
    {
        if (skillInfo?.skillData == null)
        {
            Debug.LogWarning($"SkillInfo or SkillData is null for {gameObject.name}");
            return;
        }
        string skillName = skillInfo.skillData.skillName;
        if(_data.skillTree.TryGetValue(skillName,out bool value)){
            unlock=value;
        }
    }
    
}
