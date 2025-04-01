using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour,ISaveManager
{
    public static UI instance;
    
    public UI_ItemTooltip itemTooltip;
    public UI_StashTooltip stashTooltip;
    public UI_StatTooltip statTooltip;
    public UI_CraftWindow craftWindow;
    public UI_SkillTooltip skillTooltip;
    public UI_StatusToolTip statusToolTip;
    [SerializeField] private GameObject chacraterUIGameObject;
    [SerializeField] private GameObject skillTreeUIGameObject;
    [SerializeField] private GameObject craftUIGameObject;
    [SerializeField] private GameObject optionsUIGameObject;
    [SerializeField] private GameObject ingameUIGameObject;
    public UI_Ingame ingameUI;
    [Space]
    [Header("Soul Fragment info")]
    [SerializeField] private TextMeshProUGUI currency;
    [SerializeField] private float soulFragmentAmount;
    [SerializeField] private float increaserate;
    
    
    [SerializeField] private GameObject diedText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject swordListSkillGameObject;
    [SerializeField] private GameObject bowListSkillGameObject;
    [SerializeField] private GameObject gauntleListSkillGameObject;
    [SerializeField] private GameObject magicListSkillGameObject;
    [Header("Pop Up Text")]
    [SerializeField] private GameObject popUpTextFXPrefab;
    public Transform tranformCraftButton;
    [SerializeField] private float speed;
    [SerializeField] private float colorLooseSpeed;
    [SerializeField] private float speedDesappear;
    [SerializeField] private float lifeTime;
    public UI_FadeSceen fadeSceen;
    [Header("End Screen")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private Button endScreenButton;

    private Player player;
    public bool canOpenSetting=true;
    
    [Header("Audio Setting")]
    [SerializeField] private UI_VolumeSlider[] volumeSliders;
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
        ingameUI=ingameUIGameObject.GetComponent<UI_Ingame>();
    }

    private void Start() {
        player=PlayerManager.instance.player;
        StartCoroutine(DefaultSkillList());

    }
    private void Update() {
        UpdateSoulUI();
        if(canOpenSetting==false) return;
        if(Input.GetKeyDown(KeyCode.I)){
            SwitchWithKeyTo(chacraterUIGameObject);
        }
        if(Input.GetKeyDown(KeyCode.O)){
            SwitchWithKeyTo(optionsUIGameObject);
        }
        if(Input.GetKeyDown(KeyCode.C)){
            SwitchWithKeyTo(craftUIGameObject);
        }
        if(Input.GetKeyDown(KeyCode.K)){
            SwitchWithKeyTo(skillTreeUIGameObject);
        }

    }
   public void SwitchTo(GameObject _menu){

        AudioManager.instance.PlaySFX(23);
        for (int i = 0; i < transform.childCount; i++)
        {
            bool isfadeSceen=transform.GetChild(i).GetComponent<UI_FadeSceen>()!=null;
            if(isfadeSceen==false){

                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if(_menu!=null){
            //AudioManager.instance.PlaySFX(7);
            _menu.gameObject.SetActive(true);
        }
        if(itemTooltip!=null){
            itemTooltip.HideTooltip();
        }
        if(stashTooltip!=null){
            stashTooltip.HideStashTooltip();
        }
        if(statTooltip!=null){
            statTooltip.HideStatTooltip();
        }
        if(skillTooltip!=null){
            skillTooltip.HideSkillTreeTooltip();
        }
        if(statusToolTip!=null){
            statusToolTip.HideStatTooltip();
        }

        if(GameManager.instance!=null){
            if(_menu==ingameUIGameObject){
                GameManager.instance.PauseGame(false);
                player.isBusy=false;
            }else{
                GameManager.instance.PauseGame(true);
                player.isBusy=true;
            }
        }
   }
   public void SwitchWithKeyTo(GameObject _menu){
        if(_menu!=null&&_menu.activeSelf){
            _menu.SetActive(false);
            CheckForInGame();
            return;
        }
        SwitchTo(_menu);
   }
   private void CheckForInGame(){
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf&&transform.GetChild(i).GetComponent<UI_FadeSceen>()==null)
                return;
            
        }
        SwitchTo(ingameUIGameObject);
        ingameUI.UpdateInGameWeapon();
        ingameUI.UpdateSlotSkillInUIInGame();
    }
    public void SwitchOnEndScreen(){
        fadeSceen.FadeOut();
        StartCoroutine(EndScreenCoroutine());

    }

    private void UpdateSoulUI()
    {
        if (soulFragmentAmount < PlayerManager.instance.GetCurrency())
        {
            soulFragmentAmount += increaserate * Time.unscaledDeltaTime;
        }
        else
        {
            soulFragmentAmount = PlayerManager.instance.GetCurrency();
        }
        currency.text = ((int)soulFragmentAmount).ToString("#,#");
        if (soulFragmentAmount <= 0)
        {
            currency.text = "0";
        }
    }
    public IEnumerator DefaultSkillList(){
        SwitchTo(skillTreeUIGameObject);
        swordListSkillGameObject.SetActive(true);
        bowListSkillGameObject.SetActive(true);
        gauntleListSkillGameObject.SetActive(true);
        magicListSkillGameObject.SetActive(true);
        SkillManager.instance.UpdateSlotSkill();
        yield return new WaitForEndOfFrame();
        bowListSkillGameObject.SetActive(false);
        gauntleListSkillGameObject.SetActive(false);
        magicListSkillGameObject.SetActive(false);
        SwitchTo(ingameUIGameObject);
        

    }
    public void CreatePopUpUISettingText(string _text,Transform _target,bool _isRealTime){
        GameObject newPopUpText=Instantiate(popUpTextFXPrefab,_target.transform.position,Quaternion.identity,this.transform);
        newPopUpText.GetComponent<PopUptextFX>().SetUpText(_text,_isRealTime,speed,colorLooseSpeed,speedDesappear,lifeTime);

    }
    public void CreatePopUpText(string _text,Transform _target,bool _isRealTime,float _speed,float _colorLooseSpeed,float _speedDesappear,float _lifeTime){
        GameObject newPopUpText=Instantiate(popUpTextFXPrefab,_target.transform.position,Quaternion.identity,this.transform);
        newPopUpText.GetComponent<PopUptextFX>().SetUpText(_text,_isRealTime,_speed,_colorLooseSpeed,_speedDesappear,_lifeTime);

    }
    private IEnumerator EndScreenCoroutine(){
        yield return new WaitForSeconds(1.5f);
        diedText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
        
    }
    public void RestartGameButton(){
        DungeonManager.instance.LoadBaseMap();
        fadeSceen.FadeIn();
        diedText.SetActive(false);
        restartButton.SetActive(false);
        player.isBusy=false;
        player.canUseGate=true;
        canOpenSetting=true;
        player.charaterStats.ResetDie();
        player.charaterStats.UpdateBeforeStart();
        player.stateMachine.ChangeState(player.idleState);
    }
    public IEnumerator EndGameCoroutine(){
        fadeSceen.FadeOut();
        yield return new WaitForSeconds(1f);
        endScreen.SetActive(true);
        endScreenButton.onClick.AddListener(EndGameButton);

    }
    public void EndGameButton(){
        SaveManager.instance.DeleteSaveData();
        SceneManager.LoadScene("MainMenuSceen");
        Invoke("DeleteOnDontDestroy",0.5f);

    }
    public void DeleteOnDontDestroy(){
        Destroy(UI.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
    }

    public void SaveData(ref GameData _data)
    {
        _data.audioSetting.Clear();
        foreach (UI_VolumeSlider item in volumeSliders)
        {
            _data.audioSetting.Add(item.parametr,item.slider.value);
        }
        _data.canOpenSetting=canOpenSetting;
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string,float> pair in _data.audioSetting)
        {
            foreach (UI_VolumeSlider item in volumeSliders)
            {
                if(pair.Key==item.parametr){
                    item.LoadSlider(pair.Value);
                }
        
            }
        }
        canOpenSetting=_data.canOpenSetting;
        
    }
    // public void SaveAndGoBackMainMenu(){
    //     SaveManager.instance.SaveGame();
    //     Application.Quit();
    // }

    
}
