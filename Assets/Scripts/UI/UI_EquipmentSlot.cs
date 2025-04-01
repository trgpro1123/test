using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    [SerializeField] private Sprite defaultImage;
    public EquipmentType equipmentType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(inventoryItem==null||inventoryItem.itemData==null) return;
        AudioManager.instance.PlaySFX(23);
        ItemData tempItemData=inventoryItem.itemData;
        Inventory.instance.UnequipmentItem(inventoryItem.itemData as ItemData_Equipment);
        Inventory.instance.UpdateWeaponsAndRings();
        this.ClearUpSlot();
        Inventory.instance.AddItem(tempItemData as ItemData_Equipment);
        ui.itemTooltip.HideTooltip();
    }
    public override void ClearUpSlot(){
        inventoryItem=null;
        imageItem.sprite=defaultImage;
        itemText.text="";
    }
}
