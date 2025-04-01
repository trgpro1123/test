using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private GameObject button;

    [SerializeField] private string responseTextKey;


    public string ResponeseText=>responseText;
    public DialogueObject DialogueObject=>dialogueObject;
    public GameObject Buttons=>button;





    public string GetLocalizedResponseText(string tableReference = "Dialogues")
    {
        if (string.IsNullOrEmpty(responseTextKey))
            return responseText;
            
        return UnityEngine.Localization.Settings.LocalizationSettings.StringDatabase
            .GetLocalizedString(tableReference, responseTextKey);
    }
}
