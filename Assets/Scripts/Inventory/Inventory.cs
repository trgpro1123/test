using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
public class Inventory : MonoBehaviour,ISaveManager
{
    public static Inventory instance;
    public ItemEffect weapon1;
    public ItemEffect weapon2;
    public List<ItemData> startingItem;
    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment,InventoryItem> equipmentDictionary;
    public List<InventoryItem> inventory;
    public Dictionary<ItemData,InventoryItem> inventoryDictionary;
    public List<InventoryItem> stash;
    public Dictionary<ItemData,InventoryItem> stashDictionary;


    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipSlotParent;
    [SerializeField] private Transform statSlotParent;
    private UI_ItemSlot[] inventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipItemSlot;
    private UI_StatSlot[] statSlot;

    
    [Header("Items Cooldown")]
    private float lastTimeUseFlask;
    public float flaskCooldown{get;private set;}
    private float lastTimeUseArmor;
    private float armoCooldown;


    [Header("Data base")]
    public List<ItemData> itemDataBase;
    public List<InventoryItem> loadItems;
    public List<ItemData_Equipment> loadedEquiment;

    public int currentNumberWeapon=0;
    public int currentNumberRing=0;

    private PlayerStats playerStats;
    private Player player;
    



    private void Awake() {
        if(instance==null) instance=this;
        else Destroy(gameObject);



        
        statSlot=statSlotParent.GetComponentsInChildren<UI_StatSlot>();
    }
 
    private void Start() {
        player=PlayerManager.instance.player;
        playerStats=player.GetComponent<PlayerStats>();
        inventory=new List<InventoryItem>();
        stash=new List<InventoryItem>();
        equipment=new List<InventoryItem>();
        inventoryDictionary=new Dictionary<ItemData, InventoryItem>();
        stashDictionary=new Dictionary<ItemData, InventoryItem>();
        equipmentDictionary=new Dictionary<ItemData_Equipment, InventoryItem>();
        inventoryItemSlot=inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot=stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipItemSlot=equipSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        Invoke("AddStartingItem",0.01f);

    }
    public void AddStartingItem(){
        if(loadedEquiment.Count>0){
            
            foreach (ItemData_Equipment item in loadedEquiment)
            {
                EquipItem(item);
            }
        }
        if(loadItems.Count>0){
            foreach (InventoryItem item in loadItems)
            {
                for (int i = 0; i < item.stackSize; i++)
                {
                    AddItem(item.itemData);
                }
            }
            return;
        }
        for (int i = 0; i < startingItem.Count; i++)
        {
            AddItem(startingItem[i]);
        }
    }

    







    public void EquipItem(ItemData _item){
        ItemData_Equipment newEquipItem=_item as ItemData_Equipment;
        InventoryItem newItem=new InventoryItem(newEquipItem);

        ItemData_Equipment oldItem=null;
        if(newEquipItem.equipmentType==EquipmentType.Weapon&&currentNumberWeapon<2){
            if(equipItemSlot[0].inventoryItem!=null&&equipItemSlot[0].inventoryItem.itemData==newItem.itemData){
                return;

            }
            currentNumberWeapon++;
            UpdateEquipmentDictionary(_item, newEquipItem, newItem);           
            return;

        }
        if(newEquipItem.equipmentType==EquipmentType.Ring&&currentNumberRing<2){
            if(equipItemSlot[2].inventoryItem!=null&&equipItemSlot[2].inventoryItem.itemData==newItem.itemData){
                return;

            }
            currentNumberRing++;
            UpdateEquipmentDictionary(_item, newEquipItem, newItem);
            return;

        }


        foreach (KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDictionary)
        {
            if(item.Key.equipmentType==newEquipItem.equipmentType){
                oldItem=item.Key;
            }
        }
        if(oldItem!=null)
        {
            return;
        }

            UpdateEquipmentDictionary(_item, newEquipItem, newItem);

    }

    private void UpdateEquipmentDictionary(ItemData _item, ItemData_Equipment _newEquipItem, InventoryItem _newItem)
    {
        equipment.Add(_newItem);
        equipmentDictionary.Add(_newEquipItem, _newItem);
        RemoveItem(_item);
        UpdateSlotUI();
        
    }

    public void UnequipmentItem(ItemData_Equipment _oldItem)
    {
        if (equipmentDictionary.TryGetValue(_oldItem, out InventoryItem value))
        {
            if(_oldItem.equipmentType==EquipmentType.Weapon){
                currentNumberWeapon--;
            }
            if(_oldItem.equipmentType==EquipmentType.Ring){
                currentNumberRing--;
            }
            if(_oldItem.equipmentType!=EquipmentType.WeaponEquipment2)
                _oldItem.RemoveModifiers();
            equipment.Remove(value);
            equipmentDictionary.Remove(_oldItem);
        }
        UpdateSlotUI();
    }

    public void UpdateWeaponsAndRings(){
        for(int i=0;i<equipItemSlot.Length;i++){
            foreach(KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDictionary)
            {
                if(equipItemSlot[i].inventoryItem!=null){
                    
                    if(equipItemSlot[i].equipmentType==EquipmentType.WeaponEquipment1||equipItemSlot[i].equipmentType==EquipmentType.RingEquipment1){
                        item.Key.RemoveModifiers();
                        break;
                    }                   
                }
                else {break;}
            }
            

        }

    }


    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment&&!CanAddItem()) AddToInventory(_item);
        else AddToStash(_item);
  
       UpdateSlotUI();
    }
    public bool CanAddItem(){

        return inventory.Count >= inventoryItemSlot.Length;
    }
    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }
    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }


    public void RemoveItem(ItemData _item){
        if(inventoryDictionary.TryGetValue(_item,out InventoryItem value)){
            if(value.stackSize<=1){
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else{
                value.RemoveStack();
            }
        }
        if(stashDictionary.TryGetValue(_item,out InventoryItem stashValue)){
            if(stashValue.stackSize<=1){
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else{
                stashValue.RemoveStack();
            }
        }
        UpdateSlotUI();
    }


    
    

 
    public void SwapWeapons()
    {
        var weaponItems = equipment.Where(x => 
            x.itemData is ItemData_Weapon && 
            (x.itemData as ItemData_Equipment).equipmentType == EquipmentType.Weapon
        ).ToList();
        if (weaponItems.Count == 2)
        {
            int weapon1Index = equipment.IndexOf(weaponItems[0]);
            int weapon2Index = equipment.IndexOf(weaponItems[1]);

            var tempEquipmentSwap = equipment[weapon1Index];
            equipment[weapon1Index] = equipment[weapon2Index];
            equipment[weapon2Index] = tempEquipmentSwap;

            UpdateSlotUI();

        }
    }


    public void UpdateSlotUI()
    {
        ItemData weaponHasEquip = null;
        ItemData RingHasEquip = null;
        var inventoryItemsSorted = equipmentDictionary.OrderBy(x => equipment.FindIndex(e => e.itemData == x.Key)).ToList();

        DeleteStats();
        player.ResetWeaponActive();
        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].ClearUpSlot();
        }

        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].ClearUpSlot();
        }
        for (int i = 0; i < equipItemSlot.Length; i++)
        {

            equipItemSlot[i].ClearUpSlot();
        }

        for (int i = 0; i < equipItemSlot.Length; i++)
        {

            for (int j = 0; j < inventoryItemsSorted.Count; j++)
            {
                var key = inventoryItemsSorted[j].Key;
                var value = inventoryItemsSorted[j].Value;

                if(equipItemSlot[i].inventoryItem==null){
                    if(equipItemSlot[i].equipmentType==EquipmentType.WeaponEquipment1||equipItemSlot[i].equipmentType==EquipmentType.WeaponEquipment2){
                        if(key.equipmentType==EquipmentType.Weapon){
                            if(value.itemData!=weaponHasEquip){
                                if(weaponHasEquip==null){
                                    weaponHasEquip=value.itemData;
                                }
                                    equipItemSlot[i].UpdateSlot(value);
                                    break;
                                    
                            }
                        }
                    }

                    if(equipItemSlot[i].equipmentType==EquipmentType.RingEquipment1||equipItemSlot[i].equipmentType==EquipmentType.RingEquipment2){
                        if(key.equipmentType==EquipmentType.Ring){
                            if(value.itemData!=RingHasEquip){
                                if(RingHasEquip==null){
                                    RingHasEquip=value.itemData;
                                }
                                    equipItemSlot[i].UpdateSlot(value);
                                    break;
                            }
                        }
                    }

                    if (key.equipmentType == equipItemSlot[i].equipmentType&&key.equipmentType!=EquipmentType.Weapon&&key.equipmentType!=EquipmentType.Ring)
                    {
                        equipItemSlot[i].UpdateSlot(value);
                        break;
                    }

                }
            }
        }
        


        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }
        if(equipItemSlot[0].inventoryItem!=null){
            player.WeaponChange((equipItemSlot[0].inventoryItem.itemData as ItemData_Weapon).weaponType);
        }
        AddStats();
        UpdateStatsUI();
        UI.instance.ingameUI.UpdateInGameWeapon();
        UI.instance.ingameUI.UpdateSlotSkillInUIInGame();
        UI.instance.ingameUI.UpdateHealth();
        UI.instance.ingameUI.UpdateMana();
        

    }

    public void UpdateStatsUI()
    {
        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValueUI();
        }
        UI.instance.ingameUI.UpdateHealth();
        UI.instance.ingameUI.UpdateMana();
    }
    public List<InventoryItem> GetEquipment(){
        var weaponItems = equipment.Where(x => (x.itemData as ItemData_Equipment).equipmentType == EquipmentType.Weapon).ToList();
        return weaponItems;

    }
    public void DeleteStats(){
        for(int i=0;i<equipItemSlot.Length;i++){
            foreach(KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDictionary)
            {
                if(equipItemSlot[i].inventoryItem!=null){
                    
                    if(equipItemSlot[i].equipmentType==EquipmentType.WeaponEquipment2){
                        break;
                    }
                    if(equipItemSlot[i].inventoryItem.itemData==item.Key){
                        item.Key.RemoveModifiers();
                        break;
                    }
                    
                    
                }
                else {break;}
            }
            

        }
        
    }
    public void AddStats(){
        for(int i=0;i<equipItemSlot.Length;i++){
            foreach(KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDictionary)
            {
                if(equipItemSlot[i].inventoryItem!=null){
                    
                    if(equipItemSlot[i].equipmentType==EquipmentType.WeaponEquipment2){
                        
                        break;
                    }
                    if(equipItemSlot[i].inventoryItem.itemData==item.Key){
                        item.Key.AddModifiers();
                        break;
                    }
                    
                    
                }
            }
            

        }
    }
    




    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requiredMaterials ){
        List<InventoryItem> materialsToRemove=new List<InventoryItem>();
        for(int i=0;i<_requiredMaterials.Count;i++){
            if(stashDictionary.TryGetValue(_requiredMaterials[i].itemData,out InventoryItem stashValue)){
                if(stashValue.stackSize<_requiredMaterials[i].stackSize){
                    UI.instance.CreatePopUpUISettingText("Not Enought Materials",UI.instance.tranformCraftButton,true);
                    return false;
                }else{
                    InventoryItem itemRemove=new InventoryItem(stashValue.itemData);
                    itemRemove.stackSize=_requiredMaterials[i].stackSize;
                    materialsToRemove.Add(itemRemove);
                }
            }else{
                UI.instance.CreatePopUpUISettingText("Not Enought Materials",UI.instance.tranformCraftButton,true);
                return false;
            }
        }
        if(CanAddItem()==true){
            UI.instance.CreatePopUpUISettingText("Not Enought Space",UI.instance.tranformCraftButton,true);
            return false;
        }

        foreach (var materialToRemove in materialsToRemove)
        {
            for (int i = 0; i < materialToRemove.stackSize; i++)
            {
                RemoveItem(materialToRemove.itemData);
            }
        }

        AddItem(_itemToCraft);
        UI.instance.CreatePopUpUISettingText("Craft successful",UI.instance.tranformCraftButton,true);

        return true;
    }
    public bool EnoughtSoulsCrystals(int _soulsCrystals,InventoryItem _requiredMaterials){
        if(stashDictionary.TryGetValue(_requiredMaterials.itemData,out InventoryItem stashValue)){
            if(stashValue.stackSize<_soulsCrystals){
                return false;
            }else{
                return true;
            }
        }
        return false;
        
    }
    public List<InventoryItem> GetEquimetList => equipment;
    public List<InventoryItem> GetStashList => stash;
    public ItemData_Equipment GetEquiment(EquipmentType _type){
        ItemData_Equipment equimentItem=null;
        foreach (KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDictionary)
        {
            if(item.Key.equipmentType==_type){
                equimentItem=item.Key;
            }
        }
        return equimentItem;
    }


    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equiment.Clear();
        foreach (KeyValuePair<ItemData,InventoryItem> pair in inventoryDictionary)
        {
            _data.inventory.Add(pair.Key.itemID,pair.Value.stackSize);
        }
        foreach (KeyValuePair<ItemData,InventoryItem> pair in stashDictionary)
        {
            _data.inventory.Add(pair.Key.itemID,pair.Value.stackSize);
        }
        foreach (KeyValuePair<ItemData_Equipment,InventoryItem> pair in equipmentDictionary)
        {
            _data.equiment.Add(pair.Key.itemID);
        }
    }

    public void LoadData(GameData _data)
    {

        foreach (KeyValuePair<string,int> pair in _data.inventory)
        {
            foreach (var item in itemDataBase)
            {
                if(item!=null&&item.itemID==pair.Key ){
                    InventoryItem itemToLoad=new InventoryItem(item);
                    itemToLoad.stackSize=pair.Value;
                    loadItems.Add(itemToLoad);
                }
            }
        }
        foreach (string loadedItemID in _data.equiment)
        {
            foreach (var item in itemDataBase)
            {
                if(item!=null&&item.itemID==loadedItemID){
                    loadedEquiment.Add(item as ItemData_Equipment);
                }
            }
        }
    }
   

#if UNITY_EDITOR 
    [ContextMenu("Fill up item DataBase")]
    private void FillUpItemDataBase()=> itemDataBase=new List<ItemData>(GetItemDataBase());

    private List<ItemData> GetItemDataBase(){
        string[] asstetNames=AssetDatabase.FindAssets("",new[]{"Assets/Data/Items"});
        List<ItemData> itemDatabase=new List<ItemData>();
        foreach (string SOName in asstetNames)
        {
            var SOPath=AssetDatabase.GUIDToAssetPath(SOName);
            var itemData=AssetDatabase.LoadAssetAtPath<ItemData>(SOPath);
            itemDatabase.Add(itemData);
        }
        return itemDatabase;
    }

#endif
}
