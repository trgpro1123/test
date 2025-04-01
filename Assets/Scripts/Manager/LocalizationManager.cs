using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class LocalizationManager : MonoBehaviour,ISaveManager
{
    public static LocalizationManager instance;

    public int indexLanguage;
    private void Awake() {
        if(instance!=null&&this.gameObject!=null){
            Destroy(this.gameObject);
        }
        else{
            instance=this;
        }
        if(!gameObject.transform.parent){
            DontDestroyOnLoad(gameObject);
        }
    }
    public IEnumerator SetLocals(int _localeID){
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        indexLanguage=_localeID;
    }
    public void LoadLanguage(){
        StartCoroutine(SetLocals(indexLanguage));
    }
    public void ChangeLanguageToVietnamese(){
        StartCoroutine(SetLocals(1));
    }
    public void ChangeLanguageToEnglish(){
        StartCoroutine(SetLocals(0));
    }

    public void SaveData(ref GameData _data)
    {
        _data.indexLanguage=indexLanguage;
    }

    public void LoadData(GameData _data)
    {
        indexLanguage=_data.indexLanguage;
        LoadLanguage();
    }
}
