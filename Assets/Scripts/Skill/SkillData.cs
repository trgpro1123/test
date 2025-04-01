using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;



public enum SkillType
{
    Passive,
    Active
}

[CreateAssetMenu(fileName ="New Skill Data",menuName ="Data/Skill Data")]
public class SkillData : ScriptableObject
{

    [Header("Skill description")]
    public SkillType skillType;
    public Sprite skillIcon;
    public string skillName;
    public int skillPrice;
    [TextArea]
    public string skillDescription;

    [Header("Localization")]
    public string nameKey;
    public string descriptionKey;
    public string tableName = "Skills";



    

}
