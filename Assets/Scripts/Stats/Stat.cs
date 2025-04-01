using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stat 
{
    [SerializeField] private int baseValue;
    public List<int> modifiers=new List<int>();
    public int GetValue(){
        int finalValue=baseValue;
        foreach (int value in modifiers)
        {
            if(this==PlayerManager.instance.player.charaterStats.coolDown){
                if(finalValue>=50){
                    return finalValue;
                }
                finalValue+=value;
                finalValue=Mathf.Clamp(finalValue,0,50);
                continue;
            }
            if(this==PlayerManager.instance.player.charaterStats.agility){
                if(finalValue>=50){
                    return finalValue;
                }
                finalValue+=value;
                finalValue=Mathf.Clamp(finalValue,0,50);
                continue;
            }
            if(this==PlayerManager.instance.player.charaterStats.critChance){
                if(finalValue>=100){
                    return finalValue;
                }
                finalValue+=value;
                finalValue=Mathf.Clamp(finalValue,0,100);
                continue;
            }
           
            finalValue+=value;
        }
        return finalValue;
    }
    public void SetDefaultValue(int _value){
        baseValue=_value;
    }
    public void AddModifier(int _modifiers){
        modifiers.Add(_modifiers);
    }
    public void RemoveModifier(int _modifiers){
        if(modifiers.Count>0) modifiers.Remove(_modifiers);
    }
}
