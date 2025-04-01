using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private string sceneNameToLoad="Base";
    [SerializeField] private GameObject continueButton;
    [SerializeField] private UI_FadeSceen fadeSceen;
    [SerializeField] private GameObject howToPlay;

    [Header("Save Data")]
    [SerializeField] private string dataFileName;
    //mode
    // [SerializeField] private string filePath="idbfs/TrungDev1r23214tr321";
    //normal
    // [SerializeField] private string filePath="idbfs/TrungDev1r23214tr321321";
    [SerializeField] private bool encryptData;
    private GameData gameData;
    private FileDataHandler dataHandler;
    [Header("Language")]
    // [SerializeField] private Button buttonVietnamese;
    // [SerializeField] private Button buttonEnglish;
    // [SerializeField] private GameObject tickButtonVietnamese;
    // [SerializeField] private GameObject tickButtonEnglish;
    [SerializeField] private Button buttonLanguageSetting;
    [SerializeField] private GameObject languageSetting;
    public int indexLanguage;

    


    private void Start() {
        dataHandler=new FileDataHandler(Application.persistentDataPath,dataFileName,encryptData);
        LoadGame();
        indexLanguage=gameData.indexLanguage;
        LoadLanguage();
        if(HasSaveFile()==false){
            continueButton.SetActive(false);
        }
        buttonLanguageSetting.onClick.AddListener(()=>{
            languageSetting.SetActive(true);
        });
    }
    public void Continue(){
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    public void NewGame(){
        DeleteSaveData();
        gameData=new GameData();
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    ///////////////////////////////////////////////////////////////////

    public void SaveGame(){
        gameData.indexLanguage=indexLanguage;
        dataHandler.Save(gameData);
        Debug.Log("Game Saved");
    }
    public void LoadGame(){
        gameData=dataHandler.Load();
        
        if(this.gameData==null){
            gameData=new GameData();
            Debug.Log("No data");
        }
    }
    ///////////////////////////////////////////////////////////////////


    public void ExitGame(){
        Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay){
        fadeSceen.FadeOut();
        SaveGame();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneNameToLoad);
    }
    public void HowToPlay(){
        if(howToPlay.activeSelf==true){
            howToPlay.SetActive(false);
        }
        else{
            howToPlay.SetActive(true);
        }
    }
    public void LanguageSetting(){
        if(languageSetting.activeSelf==true){
            languageSetting.SetActive(false);
        }
        else{
            languageSetting.SetActive(true);
        }
    }


    public bool HasSaveFile(){
        if(dataHandler.Load()!=null){
            return true;
        }
        else{
            return false;
        }
    }
    public void DeleteSaveData(){
        dataHandler=new FileDataHandler(Application.persistentDataPath,dataFileName,encryptData);
        dataHandler.Delete();
    }



    ///////////////////////////////////////////////////////////////////
    // public void ChangeLanguageToVietnamese(){
    //     StartCoroutine(SetLocals(1));
    //     SetTickButton(true);
    // }
    // public void ChangeLanguageToEnglish(){
    //     StartCoroutine(SetLocals(0));
    //     SetTickButton(false);
    // }
    // public void SetTickButton(bool _status){
    //     AudioManager.instance.PlaySFX(23);
    //     tickButtonVietnamese.SetActive(_status);
    //     tickButtonEnglish.SetActive(!_status);
    // }

    public IEnumerator SetLocals(int _localeID){
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        indexLanguage=_localeID;
    }
    public void LoadLanguage(){
        StartCoroutine(SetLocals(indexLanguage));
    }
    // private void OnEnable()
    // {
    //     if(indexLanguage==1){
    //         SetTickButton(true);
    //     }else{
    //         SetTickButton(false);
    //     }
    // }


}
