using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Dialogue : MonoBehaviour
{
    public TextMeshProUGUI nameNPC;
    [SerializeField] private TextMeshProUGUI textConversation;
    [SerializeField] private float typewriterSpeed=0.1f;
    private bool canSkip = false;

    private ResponseHandler responseHandler;

    private IEnumerator typingCoroutine;
    private Player player;

    public bool isOpen { get; private set; }



    private void Start() {
        player=PlayerManager.instance.player;
        isOpen=false;
        responseHandler=GetComponent<ResponseHandler>();
        CloseDialogue();

    }
    
    public void StartDialogue(DialogueObject _dialogueObject){


        if(_dialogueObject.dialogues==null) CloseDialogue();
        isOpen = true;
        this.gameObject.SetActive(true);
        player.isBusy=true;
        player.ZeroVelocity();
        player.stateMachine.ChangeState(player.idleState);
        UI.instance.canOpenSetting=false;
        StartCoroutine(StepThroughDialogue(_dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogue){

        string[] localizedDialogues = dialogue.GetLocalizedDialogues();
        for(int i=0;i<localizedDialogues.Length;i++){

            yield return StartCoroutine(TypingSentence(localizedDialogues[i]));
            yield return new WaitForSeconds(0.2f);
            if(dialogue.dialogues.Length-1==i&&dialogue.hasResponses) break;
            yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.F));
        }
        if(dialogue.hasResponses){
            responseHandler.ShowResponse(dialogue.responses);
        }
        else{
            CloseDialogue();
        } 
            
            
    }

    private IEnumerator TypingSentence(string _sentence){
        textConversation.text=string.Empty;
        canSkip = false;
        float t = 0;
        int charIndex = 0;
        yield return new WaitForSeconds(0.2f);
        canSkip = true;
        while (charIndex < _sentence.Length)
        {
            if(Input.GetKeyDown(KeyCode.F)){
                textConversation.text=_sentence;
                yield break;
            }
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typewriterSpeed;

            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, _sentence.Length);
            textConversation.text = _sentence.Substring(0, charIndex);
            yield return null;
        }
    }

    public void CloseDialogue(){

        isOpen = false;
        nameNPC.text=string.Empty;
        textConversation.text=string.Empty;
        this.gameObject.SetActive(false);
        player.isBusy=false;
        UI.instance.canOpenSetting=true;
    }

    public void SetCanSkillTrue(){
        canSkip = true;
    }

}
