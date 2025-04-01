using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class UI_EquipmentSkillTreeSlot : UI_SkillTreeSlot
{
    public int indexEquipmentSkillTreeSlot;
    protected override void Awake()
    {
        //xóa Button Action Awake class cha
    }
    protected override void Start()
    {
        base.Start();
        image.color=Color.clear;
    }
    
    public void UpdateSlot(SkillInfo _skillInfo){
        skillInfo=_skillInfo;
        if(skillInfo!=null){
            if(image==null){
                image=GetComponent<Image>();
            }
            if(skillInfo.skillData==null){
                Debug.Log("SkillData is null");
            }
            image.sprite=skillInfo.skillData.skillIcon;
            image.color=Color.white;

        }
    }
    public void ClearUpSlot(){

        skillInfo=null;
        if(image!=null){
            image.sprite=null;
            image.color=Color.clear;
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetKey(KeyCode.Mouse0)){
            if(skillInfo==null||skillInfo.skillData==null) return;
            if(skillInfo.skill.canUseSkill){
                AudioManager.instance.PlaySFX(23);
                SkillManager.instance.RemoveSkill(skillInfo);
                ui.skillTooltip.HideSkillTreeTooltip();
            }
            return;
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (skillInfo==null||skillInfo.skillData == null) return;
            ui.skillTooltip.ShowSkillTreeTooltip(skillInfo);
    }
    public SkillInfo GetSkillInfo(string _skillName){
        foreach (var item in SaveManager.instance.skillTreeSlots)
        {
            if(item.skillInfo!=null&&item.skillInfo.skillData!=null&&item.skillInfo.skillData.skillName==_skillName){
                return item.skillInfo;
            }
        }
        return null;

    }
    public override void SaveData(ref GameData _data)
    {
        
        if (skillInfo?.skillData == null)
        {
            if(_data.equipmentSkillTreeSlot.TryGetValue(indexEquipmentSkillTreeSlot, out string skillName)){
                _data.equipmentSkillTreeSlot.Remove(indexEquipmentSkillTreeSlot);
            }
            return;
        }
        string nameOfSkill=skillInfo.skillData.skillName;
        if (skillInfo?.skillData != null)
        {
            if(_data.equipmentSkillTreeSlot.TryGetValue(indexEquipmentSkillTreeSlot, out string skillName)){
                _data.equipmentSkillTreeSlot.Remove(indexEquipmentSkillTreeSlot);
                _data.equipmentSkillTreeSlot.Add(indexEquipmentSkillTreeSlot,nameOfSkill);
                Debug.Log($"Saved equipped skill {skillInfo.skillData.skillName} at slot {indexEquipmentSkillTreeSlot}");
            }else{
                _data.equipmentSkillTreeSlot.Add(indexEquipmentSkillTreeSlot,nameOfSkill);
                Debug.Log($"Saved equipped skill {skillInfo.skillData.skillName} at slot {indexEquipmentSkillTreeSlot}");
        
            }
        }


    }

    public override void LoadData(GameData _data)
    {

        
        foreach(KeyValuePair<int,string> pair in _data.equipmentSkillTreeSlot){
            if(pair.Key == indexEquipmentSkillTreeSlot)
            {
                SkillInfo newSkillInfo = GetSkillInfo(pair.Value);
                if(newSkillInfo != null)
                {

                    SkillInfo clonedSkillInfo = new SkillInfo(newSkillInfo.skillData, newSkillInfo.skill);

                    for(int i=0;i<SkillManager.instance.listSkills.Length;i++){
                        if(i==indexEquipmentSkillTreeSlot){
                            SkillManager.instance.listSkills[i]=clonedSkillInfo;
                        }
                    }
                    SkillManager.instance.AddSkill(clonedSkillInfo);

                }
                break; 
            }
        }

    }

}
