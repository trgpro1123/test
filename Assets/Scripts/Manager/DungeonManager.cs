using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{

    public static DungeonManager instance;

    public int maxMapNumber;
    public int maxRoundNumber;
    public List<string> mapName;
    public int currentRoundNumber;
    public int currentMapNumber;
    public List<string> mapNameTemp;

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


        InitializtionDungeonValue();
    }
    public void LoadNameMap(){
        for (int i = 0; i < mapName.Count; i++)
        {
            mapNameTemp.Add(mapName[i]);
        }
        
    }
    public void LoadMap(){
        currentMapNumber-=1;
        currentRoundNumber-=1;
        if(mapNameTemp.Count<=0){
            Debug.Log("Map rong");
            return;
        }
        
        int randomMap=Random.Range(0,mapNameTemp.Count);
        SceneManager.LoadScene(mapNameTemp[randomMap]);
        UI.instance.ingameUI.SetMiniMapUI(true);
        UI.instance.ingameUI.UpdateMapLevel();
        mapNameTemp.RemoveAt(randomMap);
        

    }
    public void LoadRound(){

        
        if(currentRoundNumber<=0){

            if(currentMapNumber<=0){
                LoadBaseMap();
            }else{
                UI.instance.canOpenSetting=false;
                currentRoundNumber=maxRoundNumber;
                LoadMap();
            }
        }
        else{
            currentRoundNumber-=1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameManager.instance.enemyInSealedStateGameObject.SetActive(false);
        }
        UI.instance.ingameUI.UpdateMapLevel();
        
    }
    public void LoadBaseMap(){
        UI.instance.canOpenSetting=true;
        SceneManager.LoadScene("Base");
        AudioManager.instance.PlayBMG(0);
        InitializtionDungeonValue();
        UI.instance.ingameUI.SetMiniMapUI(false);
        GameManager.instance.enemyInSealedStateGameObject.SetActive(true);
    }
    public IEnumerator LoadRoundWithDelay(float _delay){
        UI.instance.fadeSceen.FadeOut();
        yield return new WaitForSeconds(_delay);
        LoadRound();
        UI.instance.fadeSceen.FadeIn();
    }
    public IEnumerator LoadMapWithDelay(float _delay){
        UI.instance.fadeSceen.FadeOut();
        yield return new WaitForSeconds(_delay);
        LoadMap();
        UI.instance.fadeSceen.FadeIn();
        AudioManager.instance.PlayBMG(1);
    }
    public void InitializtionDungeonValue(){
        currentRoundNumber=maxRoundNumber;
        currentMapNumber=maxMapNumber;
    }
    public int GetRoundNumberDungeon(){
        return currentRoundNumber;
    }

}
