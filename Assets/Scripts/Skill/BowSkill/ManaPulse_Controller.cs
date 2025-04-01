using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPulse_Controller : MonoBehaviour
{

    private float forceKnockBack;
    private float knockBackDuration;
    private float speedExpand;
    private float maxSize;



    private Vector2 currentSize;



    void Update()
    {
        if(currentSize.x>=maxSize-0.5){
            Destroy(transform.gameObject);
        }
        currentSize=Vector2.Lerp(transform.localScale,new Vector2(maxSize,maxSize),speedExpand*Time.deltaTime);
        transform.localScale=currentSize;

    }

    public void SetManaPulse(float _forceKnockBack,float _knockBackDuration,float _speedExpand,float _maxSize){
        forceKnockBack=_forceKnockBack;
        knockBackDuration=_knockBackDuration;
        speedExpand=_speedExpand;
        maxSize=_maxSize;
    }
 
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Enemy>()){
            other.GetComponent<Entity>().DamageImpact(PlayerManager.instance.player.gameObject,forceKnockBack,knockBackDuration);
        }
    }


    
}
