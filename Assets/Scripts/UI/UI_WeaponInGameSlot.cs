using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponInGameSlot : MonoBehaviour
{
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Image imageWeapon;

    private void Start() {
        imageWeapon=transform.GetChild(0).GetComponentInChildren<Image>();
    }
    public void UpdateWeapon(ItemData_Weapon weaponData){
        if(weaponData==null){
            imageWeapon.sprite=defaultImage;
            return;
        }
        imageWeapon.sprite=weaponData.icon;
    }

    public void ClearSlot(){
        imageWeapon.sprite=defaultImage;
    }
}
