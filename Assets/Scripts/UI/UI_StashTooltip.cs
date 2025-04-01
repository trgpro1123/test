using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StashTooltip :  UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI stashNameText;


    public void ShowStashTooltip(ItemData _item){
        
        stashNameText.text=_item.itemName;
        AdjustPosition();
        gameObject.SetActive(true);
    }
    public void HideStashTooltip(){
        
        gameObject.SetActive(false);
    }
}
