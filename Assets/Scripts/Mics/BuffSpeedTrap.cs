using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeedTrap : MonoBehaviour
{
    public int speedBuff = 1;
    public float duration = 5f;
    private LayerMask interactableLayer;
    private PlayerStats playerStast;
    [SerializeField] private Sprite iconStatus;
    [SerializeField] private string statusNameKey;
    [SerializeField] private string statusDescriptionKey;
    public virtual void Start() {
        interactableLayer=LayerMask.GetMask("Player Foot");
        playerStast=PlayerManager.instance.player.GetComponent<PlayerStats>();
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if(((1 << other.gameObject.layer) & interactableLayer) != 0){
            PlayerStats playerStastOntrigger = other.GetComponentInParent<PlayerStats>();
            if(playerStastOntrigger==null) Debug.Log("PlayerStats is null");
            if(playerStastOntrigger != null&&playerStastOntrigger.hadBuffSpeed==false){
                playerStastOntrigger.hadBuffSpeed=true;
                playerStastOntrigger.ApplyFastEffect(duration,speedBuff);
                UI.instance.ingameUI.CreateStatus(iconStatus,statusNameKey,statusDescriptionKey,duration);
                Invoke("ResetBuffSpeed",duration);
                
            }
        }
    }
    public void ResetBuffSpeed(){
        playerStast.hadBuffSpeed=false;
    }
}
