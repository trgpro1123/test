using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosSlashes_Controller : MonoBehaviour
{
    private int damage;
    private float percentExtraDamageOfSkill;
    private float distance;
    private float duration;
    private float chaosSlashesTimer;
    private float timer;
    private float angle;
    private SpriteRenderer spriteRenderer;
    private GameObject objectSkill;


    private Player player=>PlayerManager.instance.player;

    public void SetChaosSlashes(int _damage, float _percentExtraDamageOfSkill, float _distance, float _duration, float _chaosSlashesTimer, float _angle, SpriteRenderer _spriteRenderer,GameObject _objectSkill){
        damage=_damage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;
        distance=_distance;
        duration=_duration;
        chaosSlashesTimer=_chaosSlashesTimer;
        timer=0;
        angle=_angle;
        spriteRenderer=_spriteRenderer;
        objectSkill=_objectSkill;

    }


    private void Update()
    {
        timer -= Time.deltaTime;
        duration -= Time.deltaTime;
        if(player.isRolling||player.charaterStats.isForzerTime){
            Destroy(gameObject);
        }

        if (timer <= 0)
        {
            if (player != null)
            {
                player.PlayerDamageHitBox(StatType.strength,damage, percentExtraDamageOfSkill, angle, spriteRenderer,objectSkill);
            }
            else
            {
                Debug.LogError("player is null");
            }
            timer = chaosSlashesTimer;
        }

        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }
    

}
