using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Enemy
{
    protected override void Start()
    {

    }
    protected override void Update()
    {

    }
    public override void Die()
    {

        Destroy(gameObject);
    }
}
