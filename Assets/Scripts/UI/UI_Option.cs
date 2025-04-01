using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Option : MonoBehaviour,ISaveManager
{

    [Header("Language")]
    [SerializeField] private Button buttonVietnamese;
    [SerializeField] private Button buttonEnglish;
    [SerializeField] private GameObject tickButtonVietnamese;
    [SerializeField] private GameObject tickButtonEnglish;

    [Header("Detail Health And Mana")]
    [SerializeField] private Button buttonDetailHealthAndMana;
    [SerializeField] private GameObject detailHealth;
    [SerializeField] private GameObject detailMana;
    [SerializeField] private GameObject tickButtonDetailHealthAndMana;
    [SerializeField] private bool isDetailHealthAndMana;
    [Header("Quit and Save")]
    [SerializeField] private Button buttonQuitAndSave;
    [SerializeField] private Button buttonSave;
    [SerializeField] private GameObject notificationSave;
    [Header("How To Play")]
    [SerializeField] private Button buttonHowToPlay;
    [SerializeField] private GameObject howToPlay;

    private void OnEnable()
    {
        if(LocalizationManager.instance==null) return;
        if(LocalizationManager.instance.indexLanguage==1){
            SetTickButton(true);
        }else{
            SetTickButton(false);
        }
    }
    
    private void Start()
    {
        buttonVietnamese.onClick.AddListener(ChangeLanguageToVietnamese);
        buttonEnglish.onClick.AddListener(ChangeLanguageToEnglish);
        buttonDetailHealthAndMana.onClick.AddListener(()=>SetDetailHealthAndMana(!isDetailHealthAndMana));
        buttonQuitAndSave.onClick.AddListener(()=>{
            SaveManager.instance.SaveGame();
            Application.Quit();
        });
        buttonSave.onClick.AddListener(SaveGame);
        buttonHowToPlay.onClick.AddListener(()=>{
            AudioManager.instance.PlaySFX(23);
            howToPlay.SetActive(true);
        });
    }
    public void ChangeLanguageToVietnamese(){
        LocalizationManager.instance.ChangeLanguageToVietnamese();
        SetTickButton(true);
    }
    public void ChangeLanguageToEnglish(){
        LocalizationManager.instance.ChangeLanguageToEnglish();
        SetTickButton(false);
    }
    public void SetTickButton(bool _status){
        AudioManager.instance.PlaySFX(23);
        tickButtonVietnamese.SetActive(_status);
        tickButtonEnglish.SetActive(!_status);
    }
    public void SetDetailHealthAndMana(bool _status){
        if(detailHealth==null||detailMana==null){
            detailHealth=UI.instance.ingameUI.healthTextUI.gameObject;
            detailMana=UI.instance.ingameUI.manaTextUI.gameObject;
        }
        else{
            Debug.Log("detailHealth and detailMana is not null");
        }
        AudioManager.instance.PlaySFX(23);
        isDetailHealthAndMana=_status;
        detailHealth.SetActive(_status);
        detailMana.SetActive(_status);
        tickButtonDetailHealthAndMana.SetActive(_status);
    }
    public void LoadDetailHealthAndMana(){
        SetDetailHealthAndMana(isDetailHealthAndMana);
    }
    public void SaveGame(){
        AudioManager.instance.PlaySFX(23);
        // StartCoroutine(SaveGameClick());
        SaveManager.instance.SaveGame();
        notificationSave.SetActive(true);
    }

    public void SaveData(ref GameData _data)
    {
        _data.isDetailHealthAndMana=isDetailHealthAndMana;
    }

    public void LoadData(GameData _data)
    {
        isDetailHealthAndMana=_data.isDetailHealthAndMana;
        LoadDetailHealthAndMana();
    }
}
