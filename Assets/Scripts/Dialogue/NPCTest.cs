using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTest : DialogueActivator
{
    [SerializeField] private DialogueObject dialogueObject;
    protected override void OpenDialogue()
    {
        UI.instance.ingameUI.StartDialogue(dialogueObject);
    }
}
