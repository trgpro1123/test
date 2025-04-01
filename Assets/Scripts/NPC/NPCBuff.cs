using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBuff : NPC
{
    [SerializeField] private Buff_Effect[] buffEffects;
    public override void Interact()
    {
        base.Interact();
        int randomEffectIndex = Random.Range(0, buffEffects.Length);
        buffEffects[randomEffectIndex].ExcectEffect(null);
        UI.instance.ingameUI.CreateStatus(buffEffects[randomEffectIndex].itemEffectSprite,buffEffects[randomEffectIndex].itemEffectNameKey, buffEffects[randomEffectIndex].itemEffectDescriptionKey, buffEffects[randomEffectIndex].effectDuration);
    }
}
