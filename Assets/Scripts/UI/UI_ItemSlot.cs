using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] protected Image imageItem;
    [SerializeField] protected TextMeshProUGUI itemText;
    [SerializeField] private Image backGroundStack;

    protected UI ui;

    public InventoryItem inventoryItem;


    protected virtual void Start() {
        ui=GetComponentInParent<UI>();
        
    }
    public void UpdateSlot(InventoryItem _invetory){
            inventoryItem=_invetory;
            imageItem.color=Color.white;
        if(inventoryItem!=null){
            imageItem.sprite=inventoryItem.itemData.icon;
            if(inventoryItem.stackSize>1){
                itemText.text=inventoryItem.stackSize.ToString();
                if(backGroundStack!=null)
                    backGroundStack.gameObject.SetActive(true);
            }else{
                itemText.text="";
                if(backGroundStack!=null)
                    backGroundStack.gameObject.SetActive(false);
            }
        }
    }
    public virtual void ClearUpSlot(){
        inventoryItem=null;
        imageItem.sprite=null;
        imageItem.color=Color.clear;
        itemText.text="";
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(inventoryItem==null) return;
        AudioManager.instance.PlaySFX(23);
        if(Input.GetKey(KeyCode.LeftControl)){
            Inventory.instance.RemoveItem(inventoryItem.itemData);
            ui.itemTooltip.HideTooltip();
            return;
        }
        if(inventoryItem.itemData.itemType==ItemType.Equipment)
            Inventory.instance.EquipItem(inventoryItem.itemData);

        ui.itemTooltip.HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(inventoryItem==null) return;
        if(inventoryItem.itemData is ItemData_Equipment){
            ui.itemTooltip.ShowTooltip(inventoryItem.itemData as ItemData_Equipment);
        }
        else if(inventoryItem.itemData is ItemData){
            ui.stashTooltip.ShowStashTooltip(inventoryItem.itemData);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(inventoryItem==null) return;
        ui.itemTooltip.HideTooltip();
        ui.stashTooltip.HideStashTooltip();
    }
}
