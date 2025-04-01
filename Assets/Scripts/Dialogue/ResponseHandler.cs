using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private Transform BoxButton;
    [SerializeField] private GameObject responseButtonTemplate;
    

    private UI_Dialogue uI_Dialogue;
    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start() {
        uI_Dialogue=GetComponent<UI_Dialogue>();
    }

    public void ShowResponse(Response[] response){
        
        for(int i=0;i<response.Length;i++){
            Response x=response[i];
            if(response[i].Buttons!=null){
                GameObject enventButton= Instantiate(response[i].Buttons,BoxButton);
                enventButton.gameObject.SetActive(true);
                enventButton.GetComponentInChildren<TextMeshProUGUI>().text = x.GetLocalizedResponseText();
                tempResponseButtons.Add(enventButton);
                continue;
            }
            GameObject newResResponseButton= CreateResponseButton(x);
            tempResponseButtons.Add(newResResponseButton);

        }
        BoxButton.gameObject.SetActive(true);
    }
    public void OnPickedResponse(Response _response){
        
        DeleteTempResponeButtons();
        AudioManager.instance.PlaySFX(23);
        if(_response.DialogueObject){
            uI_Dialogue.StartDialogue(_response.DialogueObject);
        }else{
            uI_Dialogue.CloseDialogue();
        }
    }
    public void DeleteTempResponeButtons(){
        this.gameObject.SetActive(false);
        if(tempResponseButtons.Count>0){
            foreach(GameObject button in tempResponseButtons){
                Destroy(button);
            }
            tempResponseButtons.Clear();
        }
    }

    private GameObject CreateResponseButton(Response response)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate, BoxButton);
            responseButton.GetComponentInChildren<TMP_Text>().text = 
                response.GetLocalizedResponseText(); // Dùng text đã được localize

            Button button = responseButton.GetComponent<Button>();
            button.onClick.AddListener(()=> OnPickedResponse(response));
            return responseButton;
        }



}
