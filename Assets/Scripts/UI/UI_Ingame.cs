using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UI_Ingame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider healBar;
    [SerializeField] private Slider manaBar;

    [Header("In Game Skill UI")]
    [SerializeField] private Transform skillInGameSlot;
    private UI_EquipmentInGameSkillTreeSlot[] inGameSkillSlot;
    
    [Header("In Game Weapon UI")]
    [SerializeField] private Transform inGameWeaponSlotParent;
    private UI_WeaponInGameSlot[] inGameWeaponSlot;

    [Header("In Item Drop UI")]
    [SerializeField] private Transform itemDropSlotParent;
    [SerializeField] private GameObject itemDropSlotPrefab;
    [SerializeField] private float colorLooseSpeed;
    [SerializeField] private float lifeTime;

    [Header("In Game Status UI")]
    public Transform statusInGameParent;
    [SerializeField] private GameObject statusInGamePrefab;
    public TextMeshProUGUI healthTextUI;
    public TextMeshProUGUI manaTextUI;

    [Header("In Game Dialogue UI")]
    [SerializeField] private UI_Dialogue uIDialogue;
    public TextMeshProUGUI nameDialogue;
    [Header("In Game Map UI")]
    [SerializeField] private GameObject miniMapUI;


    private SkillManager skillManager;
    private void Awake() {
        miniMapUI.SetActive(false);
        skillManager=SkillManager.instance;
        inGameWeaponSlot=inGameWeaponSlotParent.GetComponentsInChildren<UI_WeaponInGameSlot>();
        inGameSkillSlot=skillInGameSlot.GetComponentsInChildren<UI_EquipmentInGameSkillTreeSlot>();
        
    }

    
    

    public void UpdateHealth(){
        healBar.maxValue=playerStats.GetMaxHealth();
        playerStats.currentHealth=Mathf.Clamp(playerStats.currentHealth,0,playerStats.GetMaxHealth());
        healBar.value=playerStats.currentHealth;
        healthTextUI.text=playerStats.currentHealth+"/"+playerStats.GetMaxHealth();
        

    }
    public void UpdateMana(){
        manaBar.maxValue=playerStats.GetMaxMana();
        playerStats.currentMana=Mathf.Clamp(playerStats.currentMana,0,playerStats.GetMaxMana());
        manaBar.value=playerStats.currentMana;
        manaTextUI.text=playerStats.currentMana+"/\n"+playerStats.GetMaxMana();

    }
    

    public void UpdateInGameWeapon(){
        List<InventoryItem> equipment = Inventory.instance.GetEquipment();
        var weaponItems = equipment.Where(x => (x.itemData as ItemData_Equipment).equipmentType == EquipmentType.Weapon).ToList();
        if(weaponItems.Count==0){
            inGameWeaponSlot[0].ClearSlot();
            inGameWeaponSlot[1].ClearSlot();
        }
        else if(weaponItems.Count==1){
            inGameWeaponSlot[0].UpdateWeapon(weaponItems[0].itemData as ItemData_Weapon);
            inGameWeaponSlot[1].ClearSlot();
        }else if(weaponItems.Count==2){
        int weapon1Index = equipment.IndexOf(weaponItems[0]);
        int weapon2Index = equipment.IndexOf(weaponItems[1]);
        inGameWeaponSlot[0].UpdateWeapon(weaponItems[weapon1Index].itemData as ItemData_Weapon);
        inGameWeaponSlot[1].UpdateWeapon(weaponItems[weapon2Index].itemData as ItemData_Weapon);
        }

    }
    public void UpdateSlotSkillInUIInGame(){
        for (int i = 0; i < inGameSkillSlot.Length; i++)
        {
            inGameSkillSlot[i].ClearUpSlot();
        }
        SkillInfo[] listSkills = skillManager.GetArraySkillInfo();
        for(int i=0;i<listSkills.Length;i++){
            if(listSkills[i]!=null&&listSkills[i].skill!=null){
                inGameSkillSlot[i].UpdateSlot(listSkills[i]);
            }
        }
    }
    public void CreateItemDrop(Sprite _itemIcon,string _itemName){
        GameObject newItem=Instantiate(itemDropSlotPrefab,itemDropSlotParent);
        newItem.GetComponent<UI_ItemDrop>().SetUpItemDrop(_itemIcon,_itemName,colorLooseSpeed,lifeTime);
    }
    public void CreateStatus(Sprite _statusIcon,string _statusName,string _statusDescription,float _lifeTime){
        GameObject newStatus=Instantiate(statusInGamePrefab,statusInGameParent);
        newStatus.GetComponent<UI_Status>().SetUpStatus(_statusIcon,_statusName,_statusDescription,_lifeTime);
    }
    public UI_Status GetStatus(string _statusName){
        for(int i=0;i<statusInGameParent.childCount;i++){
            UI_Status status=statusInGameParent.GetChild(i).GetComponent<UI_Status>();
            if(status.statusName==_statusName){
                return status;
            }
        }
        return null;
    }
    public void DeleteStatus(string _statusName){
        UI_Status status=GetStatus(_statusName);
        if(status!=null){
            Destroy(status.gameObject);
        }
    }

    public void StartDialogue(DialogueObject _dialogueObject){
        uIDialogue.StartDialogue(_dialogueObject);
    }
    public void CloseDialogue(){
        uIDialogue.CloseDialogue();
    }
    public void RestButton(){
        uIDialogue.GetComponent<ResponseHandler>().DeleteTempResponeButtons();
    }
    public bool IsDialogueOpen(){
        return uIDialogue.isOpen;
    }
    public void SetMiniMapUI(bool _active){
        miniMapUI.SetActive(_active);
    }
    public void UpdateMapLevel(){
        int levelMap=DungeonManager.instance.maxMapNumber-DungeonManager.instance.currentMapNumber;
        int levelRound=DungeonManager.instance.maxRoundNumber-DungeonManager.instance.currentRoundNumber;
        miniMapUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=levelMap+"-"+levelRound;
    }
    
    
}
