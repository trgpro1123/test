using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;


public abstract class DialogueActivator : MonoBehaviour,IInteractable
{
    [SerializeField] private string nameNPC="The Sealed One";
    [SerializeField] private GameObject arrowhead;

    private Player player;
    private void Awake() {
        arrowhead.SetActive(false);
    }
    private void Start() {
        player=PlayerManager.instance.player;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() && other.TryGetComponent(out Player player))
        {
            player.Interactable = this;
            arrowhead.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() && other.TryGetComponent(out Player player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
                arrowhead.SetActive(false);
            }
        }
    }
    public void Interact()
    {
        string LocalizationNPCName = LocalizationSettings.StringDatabase.GetLocalizedString(
        "UI", 
        "The Sealed One");
        UI.instance.ingameUI.nameDialogue.text=LocalizationNPCName;
        OpenDialogue();
        
    }
    protected abstract void OpenDialogue();
    
    

}
