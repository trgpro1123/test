using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMaterial : NPC
{
    private ItemDrop itemDrop;
    protected override void Start()
    {
        base.Start();
        itemDrop = GetComponent<ItemDrop>();
    }
    public override void Interact()
    {
        base.Interact();
        StartCoroutine(itemDrop.GenerateDropItem());
    }
}
