using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : NPC
{
    [Range(0,1)][SerializeField] private float percentHealAmount;
    public override void Interact()
    {
        base.Interact();
        int healAmount=Mathf.RoundToInt(player.charaterStats.GetMaxHealth()*percentHealAmount);
        player.charaterStats.IncreaseHealBy(healAmount);
    }
}
