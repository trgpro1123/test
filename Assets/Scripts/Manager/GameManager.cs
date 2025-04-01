using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class GameManager : MonoBehaviour,ISaveManager
{

    public static GameManager instance;
    public NavMeshSurface Surface2D;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    [Header("Boss End")]
    public InventoryItem soulsCrystal;
    public int soulsCrystalAmount;
    public GameObject bossSummon;
    public DialogueObject dialogueBattle;
    public DialogueObject dialogueEnd;
    public GameObject enemyInSealedStateGameObject;
    private GameObject bossSummonTemp;
    private Player player;

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
    void Start()
    {
        player=PlayerManager.instance.player;
        AudioManager.instance.PlayBMG(0);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.L)){
            AudioManager.instance.PlayBMG(0);
        }
    }
    public void PauseGame(bool _pause){
        if(_pause){
            Time.timeScale=0;
        }else{
            Time.timeScale=1;
        }
    }
    public void PauseGameForSeconds(float _seconds){
        StartCoroutine(PauseGameForSecondsCoroutine(_seconds));
    }
    private IEnumerator PauseGameForSecondsCoroutine(float _seconds){
        Time.timeScale=0;
        yield return new WaitForSecondsRealtime(_seconds);
        Time.timeScale=1;
    }
    public void SetCinemachineVirtualCamera(){
        cinemachineVirtualCamera.Follow=PlayerManager.instance.player.transform;
    }
    public void StartDialogueEnd(){
        UI.instance.ingameUI.StartDialogue(dialogueEnd);
    }

    public void OnClick1(){
        Debug.Log("Event 1");
        if(Inventory.instance.EnoughtSoulsCrystals(soulsCrystalAmount,soulsCrystal)){
            bossSummonTemp=Instantiate(bossSummon,enemyInSealedStateGameObject.transform.position,Quaternion.identity);
            bossSummonTemp.SetActive(true);
            Surface2D.BuildNavMeshAsync();
            enemyInSealedStateGameObject.SetActive(false);
            UI.instance.ingameUI.RestButton();
            UI.instance.ingameUI.CloseDialogue();
            UI.instance.ingameUI.StartDialogue(dialogueBattle);
            PlayerManager.instance.player.canUseGate=false;
            AudioManager.instance.PlayBMG(2);

            string LocalizationNPCName = LocalizationSettings.StringDatabase.GetLocalizedString(
            "UI", 
            "The Sealed One");
            UI.instance.ingameUI.nameDialogue.text=LocalizationNPCName;
            AudioManager.instance.PlaySFX(23);
        }
        else{
            AudioManager.instance.PlaySFX(22);
        }

        
    }
    public void OnClick2(){
        Debug.Log("Event 2");
        AudioManager.instance.PlaySFX(23);
        UI.instance.ingameUI.RestButton();
        UI.instance.ingameUI.CloseDialogue();
        FindObjectOfType<Enemy_BossEnd>().isWaiting=false;
        UI.instance.canOpenSetting=false;
    }
    public void OnClick3(){
        AudioManager.instance.PlaySFX(23);
        UI.instance.ingameUI.RestButton();
        UI.instance.ingameUI.CloseDialogue();
        StartCoroutine(UI.instance.EndGameCoroutine());

    }

    public void SaveData(ref GameData _data)
    {
        if(player==null) player=PlayerManager.instance.player;
        _data.canUseGate=player.canUseGate;
        
    }

    public void LoadData(GameData _data)
    {
        if(player==null) player=PlayerManager.instance.player;
        player.canUseGate=_data.canUseGate;
    }
}
