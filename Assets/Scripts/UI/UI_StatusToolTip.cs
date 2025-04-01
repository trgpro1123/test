using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class UI_StatusToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI statusName;
    [SerializeField] private TextMeshProUGUI statusDescription;


    public void ShowStatTooltip(string _statusName,string _descriptionStatusKey){
        AdjustPosition();
        string localizedName = LocalizationSettings.StringDatabase.GetLocalizedString(
        "Status", 
        _statusName);
        string localizedDescription = LocalizationSettings.StringDatabase.GetLocalizedString(
        "StatusDescription", 
        _descriptionStatusKey);
        statusName.text=localizedName;
        statusDescription.text=localizedDescription;
        gameObject.SetActive(true);
    }
    public void HideStatTooltip(){
        
        gameObject.SetActive(false);
    }
}
