using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;


public class ItemEffect : ScriptableObject
{
    public Sprite itemEffectSprite;
    [TextArea]
    public string itemEffectDescription;
    [Header("Localization")]
    public string itemEffectNameKey;
    public string itemEffectDescriptionKey;
    public virtual void ExcectEffect(Transform _target){
        Debug.Log("Effect is used!");
    }
    public string GetLocalizeItemEffectDescription(){
        return LocalizationSettings.StringDatabase.GetLocalizedString("StatusDescription",itemEffectDescriptionKey);
    }
    public string GetLocalizeItemEffectNameKey(){
        Debug.Log("Status"+itemEffectNameKey);
        return LocalizationSettings.StringDatabase.GetLocalizedString("Status",itemEffectNameKey);
    }
}
