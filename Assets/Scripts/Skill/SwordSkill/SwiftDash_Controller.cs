using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftDash_Controller : MonoBehaviour
{
    private float SwiftDashSpeed;
    private float SwiftDashTime;
    private int damage;
    private float percentExtraDamageOfSkill;


    private Player player;
    public void SetSwiftDash(Player _player,int _damage,float _percentExtraDamageOfSkill, float _swiftDashSpeed, float _swiftDashTime)
    {
        player=_player;
        SwiftDashSpeed=_swiftDashSpeed;
        SwiftDashTime=_swiftDashTime;
        damage=_damage;
        percentExtraDamageOfSkill=_percentExtraDamageOfSkill;

    }
    private void Update() {
        SwiftDashTime-=Time.deltaTime;
        if(SwiftDashTime<0){
            Destroy(gameObject);
        }
        else{
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Enemy>()){
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if(enemyStats!=null){
                if (enemyStats != null)
                {
                    PlayerManager.instance.player.charaterStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
                }
            }
        }
    }
}
