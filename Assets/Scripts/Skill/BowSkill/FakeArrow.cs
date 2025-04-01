using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeArrow : ArrowBehaviour
{
    protected override void Start() {
        base.Start();
        
    }
    protected override void Update()
    {
        base.Update();

    }
    public void SetFakeArrow(float _speed,float _lifeTime)
    {
        speed=_speed;
        lifeTime=_lifeTime;
        timer=lifeTime;


    }

}
