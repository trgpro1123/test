using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

[CreateAssetMenu(menuName ="Dialogue/Dialogue Object")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] Response[] response;
    [Header("Localization")]
    [SerializeField] private string tableReference = "Dialogues";
    [SerializeField] private string[] dialogueKeys;


    public bool hasResponses => (response != null && response.Length > 0);
    public string[] dialogues =>dialogue;
    public Response[] responses=>response;




    public string[] GetLocalizedDialogues()
    {
        if (dialogueKeys == null || dialogueKeys.Length == 0)
            return dialogue;
            
        string[] localizedDialogues = new string[dialogueKeys.Length];
        
        for (int i = 0; i < dialogueKeys.Length; i++)
        {
            localizedDialogues[i] = GetLocalizedString(dialogueKeys[i]);
        }
        
        return localizedDialogues;
    }
    

    private string GetLocalizedString(string key)
    {
        if (string.IsNullOrEmpty(key))
            return "";
            
        return UnityEngine.Localization.Settings.LocalizationSettings.StringDatabase
            .GetLocalizedString(tableReference, key);
        
    }
}
