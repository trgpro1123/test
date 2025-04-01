using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : EntityFX
{
    

    public void TakeDamageFX(float _time)
    {
        InvokeRepeating("TakeDamageColor",0,0.1f);
        Invoke("CanncelTakeDamageFX",_time);
    }
    public void CanncelTakeDamageFX()
    {
        CancelInvoke();
        spriteRenderer.color=Color.white;

    }
    public void TakeDamageColor()
    {
        if(spriteRenderer.color==Color.clear)
        {
            spriteRenderer.color=Color.white;
        }
        else
        {
            spriteRenderer.color=Color.clear;
        }
    }
}
