using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int currency;
    public int indexLanguage;
    public bool isDetailHealthAndMana;
    public bool startringDialogue;
    public bool canOpenSetting=false;
    public bool canUseGate=false;
    public SerializableDictionary<string,int> inventory;
    public SerializableDictionary<string,bool> skillTree;
    public SerializableDictionary<int,string> equipmentSkillTreeSlot;
    public SerializableDictionary<string,float> audioSetting;
    public List<string> equiment;


    public GameData()
    {
        currency = 0;
        indexLanguage=0;
        isDetailHealthAndMana=true;
        startringDialogue=true;
        canUseGate=false;
        canOpenSetting=false;
        inventory=new SerializableDictionary<string,int>();
        skillTree=new SerializableDictionary<string,bool>();
        equipmentSkillTreeSlot=new SerializableDictionary<int,string>();
        audioSetting=new SerializableDictionary<string,float>();
        equiment=new List<string>();

    }
}
