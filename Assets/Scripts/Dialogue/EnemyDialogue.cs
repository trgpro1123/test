using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDialogue : DialogueActivator,ISaveManager
{
    [SerializeField] private DialogueObject dialogueObjectStaring;
    [SerializeField] private DialogueObject dialogueObjectNomarl;
    public bool startringDialogue = true;

    public void LoadData(GameData _data)
    {
        startringDialogue = _data.startringDialogue;
    }

    public void SaveData(ref GameData _data)
    {
        _data.startringDialogue = startringDialogue;
    }

    protected override void OpenDialogue()
    {
        if(startringDialogue){
            UI.instance.canOpenSetting=true;
            PlayerManager.instance.player.canUseGate=true;
            UI.instance.ingameUI.StartDialogue(dialogueObjectStaring);
            startringDialogue=false;
            return;
        }
        if(!startringDialogue)
            UI.instance.ingameUI.StartDialogue(dialogueObjectNomarl);
    }
}
