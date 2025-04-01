using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;


    [SerializeField] private string dataFileName;
    //mode
    // [SerializeField] private string filePath="idbfs/TrungDev1r23214tr321";
    //normal
    // [SerializeField] private string filePath="idbfs/TrungDev1r23214tr321321";
    [SerializeField] private bool encryptData;
    private List<ISaveManager> saveManagers;
    private GameData gameData;
    public List<UI_SkillTreeSlot> skillTreeSlots;

    private FileDataHandler dataHandler;



    [ContextMenu("Delete save file")]
    public void DeleteSaveData(){
        dataHandler=new FileDataHandler(Application.persistentDataPath,dataFileName,encryptData);
        dataHandler.Delete();
    }

    
    private void Awake() {
        if(instance==null) instance=this;
        else Destroy(this.gameObject);
    }


    private void Start() {
        dataHandler=new FileDataHandler(Application.persistentDataPath,dataFileName,encryptData);
        saveManagers=FindAllFileSaveManagers();
        LoadGame();
    }
    public void SaveGame(){
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
        Debug.Log("Game Saved");
    }
    public void LoadGame(){
        gameData=dataHandler.Load();
        
        if(this.gameData==null){
            newGame();
            Debug.Log("No data");
        }
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }


    }
    private void newGame(){
        gameData=new GameData();
    }

    private void OnApplicationQuit() {
       SaveGame();
    }
    private List<ISaveManager> FindAllFileSaveManagers(){

        UI_SkillTreeSlot[] skillTreeSlots = FindObjectsOfType<UI_SkillTreeSlot>(true); // true để tìm cả inactive objects

        List<ISaveManager> managers = new List<ISaveManager>();
        

        foreach (UI_SkillTreeSlot slot in skillTreeSlots)
        {
            if (slot != null)
            {
                slot.UpdateData();
                managers.Add(slot);

                this.skillTreeSlots.Add(slot);

            }
        }


        var otherManagers = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveManager>();
        foreach (var manager in otherManagers)
        {
            if (manager is not UI_SkillTreeSlot) 
            {
                managers.Add(manager);
            }
        }

        return managers;

    }
}
