using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ItemDrop : MonoBehaviour
{
    [SerializeField] private Image frameImage;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    private float colorLooseSpeed;
    private float lifeTime;

    private float timer;

    public void SetUpItemDrop(Sprite _itemIcon,string _itemName,float _colorLooseSpeed,float _lifeTime){
        itemIcon.sprite=_itemIcon;
        itemName.text=_itemName;
        colorLooseSpeed=_colorLooseSpeed;
        lifeTime=_lifeTime;
    }
    private void Start() {
        frameImage=GetComponent<Image>();
        timer=lifeTime;
    } 

    private void Update() {
        timer-=Time.deltaTime;
        if(timer<0){
            float alpha=frameImage.color.a-colorLooseSpeed*Time.deltaTime;
            frameImage.color=new Color(frameImage.color.r,frameImage.color.g,frameImage.color.b,alpha);
            itemIcon.color=new Color(itemIcon.color.r,itemIcon.color.g,itemIcon.color.b,alpha);
            itemName.color=new Color(itemName.color.r,itemName.color.g,itemName.color.b,alpha);
            if(alpha<=0.3) Destroy(gameObject);
        }
    }
}
