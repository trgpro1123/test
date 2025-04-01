
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public enum EquipmentType{

    Weapon,
    WeaponEquipment1,
    WeaponEquipment2,
    Helmet,
    Armor,
    Pant,
    Boots,
    Ring,
    RingEquipment1,
    RingEquipment2
}
[CreateAssetMenu(fileName ="New Item Data",menuName ="Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    [Header("Item Equipment description")]
    public EquipmentType equipmentType;

    public float itemCooldown;

    [Header("Stats")]
    public int health;
    public int strength;
    public int armor;
    public int agility;
    public int moveSpeed;
    public int lifesteal;
    public int critChance;
    public int magicPower;
    public int magicResistance;
    public int magicRegeneration;
    public int luck;
    public int coolDown;


    [Header("Craft Requirements")]
    public List<InventoryItem> craftingMaterials; 


    private int desciptionLength;
    
    public void addTemp(){
        PlayerStats playerStats=PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.magicPower.AddModifier(magicPower);
    }
    public void AddModifiers(){
        PlayerStats playerStats=PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.health.AddModifier(health);
        playerStats.strength.AddModifier(strength);
        playerStats.armor.AddModifier(armor);
        playerStats.agility.AddModifier(agility);
        playerStats.moveSpeed.AddModifier(moveSpeed);
        playerStats.Lifesteal.AddModifier(lifesteal);
        playerStats.critChance.AddModifier(critChance);
        playerStats.magicPower.AddModifier(magicPower);
        playerStats.magicResistance.AddModifier(magicResistance);
        playerStats.magicRegeneration.AddModifier(magicRegeneration);
        playerStats.luck.AddModifier(luck);
        playerStats.coolDown.AddModifier(coolDown);
    }
    public void RemoveModifiers(){
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.health.RemoveModifier(health);
        playerStats.strength.RemoveModifier(strength);
        playerStats.armor.RemoveModifier(armor);
        playerStats.agility.RemoveModifier(agility);
        playerStats.moveSpeed.RemoveModifier(moveSpeed);
        playerStats.Lifesteal.RemoveModifier(lifesteal);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.magicPower.RemoveModifier(magicPower);
        playerStats.magicResistance.RemoveModifier(magicResistance);
        playerStats.magicRegeneration.RemoveModifier(magicRegeneration);
        playerStats.luck.RemoveModifier(luck);
        playerStats.coolDown.RemoveModifier(coolDown);
    }

    public override string GetDesciptrion()
    {
        sb.Length=0;
        desciptionLength=0;
        AddItemDescription(health, GetLocalizeStat("Health"));
        AddItemDescription(strength, GetLocalizeStat("Strength"));
        AddItemDescription(armor, GetLocalizeStat("Armor"));
        AddItemDescription(agility, GetLocalizeStat("Agility"));
        AddItemDescription(moveSpeed, GetLocalizeStat("M.Speed"));
        AddItemDescription(lifesteal, GetLocalizeStat("Lifesteal"));
        AddItemDescription(critChance, GetLocalizeStat("C.Chance"));
        AddItemDescription(magicPower, GetLocalizeStat("M.Power"));
        AddItemDescription(magicResistance, GetLocalizeStat("M.Resistance"));
        AddItemDescription(magicRegeneration, GetLocalizeStat("M.Regeneration"));
        AddItemDescription(luck, GetLocalizeStat("Luck"));
        AddItemDescription(coolDown, GetLocalizeStat("C.Down"));

        if(desciptionLength<5){
            for (int i = 0; i < 5-desciptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }
        
        return sb.ToString();
    }
    public void AddItemDescription(int _value,string _name){
        if(_value!=0){
            if(sb.Length>0){
                sb.AppendLine();
            }
            if(_value>0){
                sb.Append("+"+_value+" "+_name);
            }
            desciptionLength++;
        }
    }
    public string GetLocalizeStat(string _statName){
        return LocalizationSettings.StringDatabase.GetLocalizedString("Stats", _statName);
    }
    
    public string GetLocalizeEquipmentType(){
        return LocalizationSettings.StringDatabase.GetLocalizedString("EquipmentType", equipmentType.ToString());
    }

}
