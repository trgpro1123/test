using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PassiveSkill : MonoBehaviour
{
    protected Player player;
    protected UI_Ingame ingameUI;
    public Sprite iconStatus;
    [TextArea(3,3)]
    public string statusDescription;
    public string statusKey;
    protected virtual void Start() {
        player=PlayerManager.instance.player;
        ingameUI=UI.instance.ingameUI;
        Invoke("CheckUnlock",.1f);
    }
    protected virtual void Update() {
        
    }
    protected virtual void CheckUnlock(){

    }
}
