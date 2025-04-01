using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop;
    [SerializeField] private Sprite currencyIcon;
    [SerializeField] private string currencyNameKey;
    [SerializeField] private Vector2 currencyDropRange;
    [SerializeField] private int possibleCurrencyDrop;
    [SerializeField] private ItemData[] possibleDrop;
    [SerializeField] private float dropExtra;
    [SerializeField] private ItemData[] possibleSpecialDrop;
    [SerializeField] private float specialDropExtra;

    public IEnumerator GenerateDropItem(){
        for(int i=0;i<possibleItemDrop;i++){
            float dropChance=possibleDrop[i].dropChance+PlayerManager.instance.player.GetComponent<PlayerStats>().luck.GetValue()+dropExtra;
            if(Random.Range(0,100)<=dropChance){
                UI.instance.ingameUI.CreateItemDrop(possibleDrop[i].icon,possibleDrop[i].itemName);
                Inventory.instance.AddItem(possibleDrop[i]);
                yield return new WaitForSeconds(0.2f);
            }
        }
        for(int i=0;i<possibleSpecialDrop.Length;i++){
            float dropChance=possibleSpecialDrop[i].dropChance+PlayerManager.instance.player.GetComponent<PlayerStats>().luck.GetValue()+specialDropExtra;
            dropChance=Mathf.Clamp(dropChance,0,100);
            if(Random.Range(0,100)<=dropChance){
                UI.instance.ingameUI.CreateItemDrop(possibleSpecialDrop[i].icon,possibleSpecialDrop[i].itemName);
                Inventory.instance.AddItem(possibleSpecialDrop[i]);
                yield return new WaitForSeconds(0.2f);
            }
        }
        yield return new WaitForSeconds(0.15f);
        if(Random.Range(0,100)<=possibleCurrencyDrop){
            int randomCurrency=Random.Range((int)currencyDropRange.x,(int)currencyDropRange.y);
            string localizedCurrencyName=LocalizationSettings.StringDatabase.GetLocalizedString(
            "UI", 
            currencyNameKey);
            UI.instance.ingameUI.CreateItemDrop(currencyIcon,localizedCurrencyName+" x"+randomCurrency);
            PlayerManager.instance.currency+=randomCurrency;
        }
        
    }
    
}
