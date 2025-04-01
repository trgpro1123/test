using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class LanguageSetting : MonoBehaviour
{
    [SerializeField] private Button buttonVietnamese;
    [SerializeField] private Button buttonEnglish;
    [SerializeField] private GameObject tickButtonVietnamese;
    [SerializeField] private GameObject tickButtonEnglish;
    private int indexLanguage;
    public UI_MainMenu uiMainMenu;
    private void Start() {
        uiMainMenu=GetComponentInParent<UI_MainMenu>();
        buttonVietnamese.onClick.AddListener(ChangeLanguageToVietnamese);
        buttonEnglish.onClick.AddListener(ChangeLanguageToEnglish);
        // LoadLanguage();
    }

    public void ChangeLanguageToVietnamese(){
        StartCoroutine(uiMainMenu.SetLocals(1));
        SetTickButton(true);
    }
    public void ChangeLanguageToEnglish(){
        StartCoroutine(uiMainMenu.SetLocals(0));
        SetTickButton(false);
    }
    public void SetTickButton(bool _status){
        tickButtonVietnamese.SetActive(_status);
        tickButtonEnglish.SetActive(!_status);
    }


    private void OnEnable()
    {
        if(uiMainMenu==null){
            uiMainMenu=GetComponentInParent<UI_MainMenu>();
        }
        if(uiMainMenu.indexLanguage==1){
            SetTickButton(true);
        }else{
            SetTickButton(false);
        }
    }
}
