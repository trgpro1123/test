using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageBarrage_Controller : MonoBehaviour
{
    private int damage;
    private float percentExtraDamageOfSkill;
    private float distance;
    private float duration;
    private float savageBarrageTimer;
    private float timer;
    private float angle;
    [HideInInspector] public bool canDestroy=false;
    private SpriteRenderer spriteRenderer;
    private GameObject objectSkill;


    private Player player=>PlayerManager.instance.player;

    public void SetSavageBarrage(int _damage, float _percentExtraDamageOfSkill, float _distance, float _duration, float _savageBarrageTimer, float _angle, SpriteRenderer _spriteRenderer,GameObject _objectSkill){
        damage=_damage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;
        distance=_distance;
        duration=_duration;
        savageBarrageTimer=_savageBarrageTimer;
        timer=savageBarrageTimer;
        angle=_angle;
        spriteRenderer=_spriteRenderer;
        objectSkill=_objectSkill;

    }
    private void Update()
    {
        timer -= Time.deltaTime;
        duration -= Time.deltaTime;

        if (timer <= 0)
        {
            if (player != null)
            {
                AudioManager.instance.PlaySFX(9);
                player.PlayerDamageHitBox(StatType.strength,damage, percentExtraDamageOfSkill, angle, spriteRenderer,objectSkill);
            }
            else
            {
                Debug.LogError("player is null");
            }
            timer = savageBarrageTimer;
        }

        if (duration <= 0)
        {
            canDestroy = true;
        }
    }
}
