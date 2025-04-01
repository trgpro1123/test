using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class UI_SkillTooltip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillPrice;
    [SerializeField] private TextMeshProUGUI skillType;
    [SerializeField] private TextMeshProUGUI skillCost;



    public void ShowSkillTreeTooltip(SkillInfo _skillInfo){
        
    string localizedName = LocalizationSettings.StringDatabase.GetLocalizedString(
        _skillInfo.skillData.tableName, 
        _skillInfo.skillData.nameKey);
    
    string localizedDesc = LocalizationSettings.StringDatabase.GetLocalizedString(
        _skillInfo.skillData.tableName, 
        _skillInfo.skillData.descriptionKey);
    
    skillName.text = localizedName;
    skillDescription.text = localizedDesc;
    
    skillPrice.text = string.Format(
        LocalizationSettings.StringDatabase.GetLocalizedString("UI", "Price_Format"), 
        _skillInfo.skillData.skillPrice.ToString());
    
    skillType.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI", "SkillType_" + _skillInfo.skillData.skillType.ToString());
    

    if(_skillInfo.skill != null) {
        skillCost.gameObject.SetActive(true);
        skillCost.text = string.Format(
            LocalizationSettings.StringDatabase.GetLocalizedString("UI", "Mana_Format"),
            _skillInfo.skill.skillCost.ToString());
    } else {
        skillCost.text = "";
        skillCost.gameObject.SetActive(false);
    }
        AdjustPosition();
        gameObject.SetActive(true);
    }
    public void HideSkillTreeTooltip(){
        gameObject.SetActive(false);
    }
}
