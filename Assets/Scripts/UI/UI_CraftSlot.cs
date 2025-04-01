using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class UI_CraftSlot : UI_ItemSlot
{
    [SerializeField] private int DefaultfontSize=20;
    protected override void Start()
    {
        base.Start();

    }
    
    public void SetUpCraftSlot(ItemData_Equipment _item){
        inventoryItem.itemData=_item;
        imageItem.sprite=_item.icon;
        itemText.text=_item.itemName;
        if(itemText.text.Length>30){
            itemText.fontSize=16;
        }else{
            itemText.fontSize=DefaultfontSize;
        }
        
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetUpCraftWindow(inventoryItem.itemData as ItemData_Equipment);
    }
}
