using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

using TMPro;

public class UI_StatTooltip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI statName;
    private string statTableReference="StatsDescription";
    public void ShowStatTooltip(string _descriptionStatKey){
        string localizedName = LocalizationSettings.StringDatabase.GetLocalizedString(
            statTableReference, 
            _descriptionStatKey);
        statName.text=localizedName;
        AdjustPosition();
        gameObject.SetActive(true);
    }

    public void HideStatTooltip(){
        
        gameObject.SetActive(false);
    }
}
