using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEndButtonEvent : MonoBehaviour
{
    public Button button;
    public InventoryItem soulsCrystal;
    public int soulsCrystalAmount;
    public GameObject bossSummon;
    public DialogueObject dialogueBattle;
    public GameObject enemyInSealedStateGameObject;
    protected virtual void Awake() {
    }
    protected virtual void Start()
    {
        LoadButton();
    }
    
    private void LoadButton(){
        if (this.button != null) return;
        this.button=GetComponent<Button>();
    }
    public void OnClick1(){
        Debug.Log("Event 1");
        UI.instance.ingameUI.CloseDialogue();
        if(Inventory.instance.EnoughtSoulsCrystals(soulsCrystalAmount,soulsCrystal)){
            bossSummon=Instantiate(bossSummon,enemyInSealedStateGameObject.transform.position,Quaternion.identity);
            bossSummon.SetActive(true);
            enemyInSealedStateGameObject.SetActive(false);
        }

        
    }
    public void OnClick2(){
        Debug.Log("Event 2");
        FindObjectOfType<Enemy_BossEnd>().isWaiting=false;
    }
    public void OnClick3(){
        Debug.Log("Event 3");
    }
}
