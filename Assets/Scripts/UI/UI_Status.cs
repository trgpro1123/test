using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Status : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    [SerializeField] private Image statusIcon;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject backGroundColor;
    public string statusName{get;private set;}
    private string statusDescriptionKey;
    private float lifeTime;

    public float timer=0;

    

    public void SetUpStatus(Sprite _statusIcon,string _statusName,string _statusDescription,float _lifeTime){
        lifeTime=_lifeTime;
        UI_Status oldStatus=UI.instance.ingameUI.GetStatus(_statusName);
        if(oldStatus!=null){
            oldStatus.timer+=lifeTime;
            if(oldStatus.timer!=0)
                oldStatus.timerText.text=oldStatus.timer.ToString("0");
            Destroy(gameObject);
            return;
        }
        statusIcon.sprite=_statusIcon;
        statusName=_statusName;
        statusDescriptionKey=_statusDescription;
        if(lifeTime==0){
            backGroundColor.SetActive(false);
        }
    }
    private void Start() {
        timer=lifeTime;
        timerText.text="";
    } 

    private void Update() {
        if(timer>0){
            timer-=Time.deltaTime;
            timerText.text=timer.ToString("0");
        }
        if(timer<0){
            Destroy(gameObject);
        }
    }
    private void OnDestroy() {
        UI.instance.statusToolTip.HideStatTooltip();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(statusIcon!=null){
            UI.instance.statusToolTip.ShowStatTooltip(statusName,statusDescriptionKey);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.instance.statusToolTip.HideStatTooltip();
    }

    
}
