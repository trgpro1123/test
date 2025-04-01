using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Entity entity;
    private CharaterStats charaterStats;
    private RectTransform rectTransform;
    public Slider slider;


    private void Awake() {
        entity=GetComponentInParent<Entity>();
        charaterStats=GetComponentInParent<CharaterStats>();
    }

    private void Start() {

        rectTransform=GetComponent<RectTransform>();
        slider=GetComponentInChildren<Slider>();


        
        UpdateHealth();

    }
    

    public void UpdateHealth(){
        slider.maxValue=charaterStats.GetMaxHealth();
        slider.value=charaterStats.currentHealth;

    }

    private void FlipUI(){
        rectTransform.Rotate(0,180,0);
    }
    private void OnEnable() {
        if(entity!=null)
            entity.onFlipped+=FlipUI;
        charaterStats.onHealthChanged+=UpdateHealth;
        
    }


    private void OnDisable() {
        if(entity!=null)
            entity.onFlipped-=FlipUI;
        if(charaterStats!=null)
            charaterStats.onHealthChanged-=UpdateHealth;
    }

}
