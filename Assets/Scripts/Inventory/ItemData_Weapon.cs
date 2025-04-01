using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType{
    Sword,
    Bow,
    Gauntlet

}
[CreateAssetMenu(fileName ="New Weapon Data",menuName ="Data/Weapon")]
public class ItemData_Weapon : ItemData_Equipment
{
    public WeaponType weaponType;
}
