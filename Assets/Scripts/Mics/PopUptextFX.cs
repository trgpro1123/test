using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class PopUptextFX : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float colorLooseSpeed;
    [SerializeField] private float speedDesappear;
    [SerializeField] private float lifeTime;

    private TextMeshProUGUI popUpText;
    private float textTimer;
    private float textTimerRealTime;
    private bool isRealTime;

    private void Start() {
        popUpText=GetComponent<TextMeshProUGUI>();
        
    }
    private void Update() {
        textTimer-=Time.deltaTime;
        textTimerRealTime-=Time.unscaledDeltaTime;
        if(isRealTime){
            transform.position=Vector2.MoveTowards(transform.position,
            new Vector2(transform.position.x,transform.position.y+1),speed*Time.unscaledDeltaTime);

            if(textTimerRealTime<0){
                float alpha=popUpText.color.a-colorLooseSpeed*Time.unscaledDeltaTime;
                popUpText.color=new Color(popUpText.color.r,popUpText.color.g,popUpText.color.b,alpha);
                if(popUpText.color.a<50){
                    speed=speedDesappear;

                }
                if(popUpText.color.a<=0) Destroy(gameObject);
            }

        }else{
            transform.position=Vector2.MoveTowards(transform.position,
            new Vector2(transform.position.x,transform.position.y+1),speed*Time.deltaTime);

            if(textTimer<0){
                float alpha=popUpText.color.a-colorLooseSpeed*Time.deltaTime;
                popUpText.color=new Color(popUpText.color.r,popUpText.color.g,popUpText.color.b,alpha);
                if(popUpText.color.a<50){
                    speed=speedDesappear;

                }
                if(popUpText.color.a<=0) Destroy(gameObject);
            }
        }
    }
    public void SetUpText(string _text,bool _isRealTime,float _speed,float _colorLooseSpeed,float _speedDesappear,float _lifeTime){
        popUpText=GetComponent<TextMeshProUGUI>();
        string localizedName= LocalizationSettings.StringDatabase.GetLocalizedString(
        "PopUptext", 
        _text);
        popUpText.text =localizedName;
        isRealTime=_isRealTime;
        textTimer=lifeTime;
        speed=_speed;
        colorLooseSpeed=_colorLooseSpeed;
        speedDesappear=_speedDesappear;
        lifeTime=_lifeTime;
        float xOffset=Random.Range(-1,1);
        float yOffset=Random.Range(1,2);
        Vector3 randomPositoon=new Vector3(xOffset,yOffset,0);
        transform.position+=randomPositoon;
    }
    
}
