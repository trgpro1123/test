using UnityEngine;
using System.Text;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

#if UNITY_EDITOR 
using UnityEditor;
#endif

public enum ItemType{
    Material,
    Equipment
}
[CreateAssetMenu(fileName ="New Item Data",menuName ="Data/Item")]
public class ItemData : ScriptableObject
{
    [Header("Item description")]
    public ItemType itemType;
    public string defaultItemName;
    public Sprite icon;
    [Range(0,100)]
    public float dropChance;
    public string itemID;

    [Header("Localization")]
    [SerializeField] private string itemNameKey; 
    [SerializeField] private string itemDescriptionKey; 
    [SerializeField] private string tableReference; 

    public string itemName => GetLocalizedName();

    protected StringBuilder sb=new StringBuilder();

    public string GetLocalizedName()
    {
        if (string.IsNullOrEmpty(itemNameKey))
            return defaultItemName;
            
        string localizedName = LocalizationSettings.StringDatabase.GetLocalizedString(tableReference, itemNameKey);
        return string.IsNullOrEmpty(localizedName) ? defaultItemName : localizedName;
    }

    private void OnValidate() {
#if UNITY_EDITOR 
    string path=AssetDatabase.GetAssetPath(this);
    itemID=AssetDatabase.AssetPathToGUID(path);
#endif
    }
    public virtual string GetDesciptrion(){
        return "";
    }
}
