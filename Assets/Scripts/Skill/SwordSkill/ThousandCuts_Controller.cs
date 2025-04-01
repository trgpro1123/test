using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThousandCuts_Controller : MonoBehaviour
{
    private int skillDamage;
    private float percentExtraDamageOfSkill;
    private float damageTimer;
    private float skillSize;

    private float timer;
    private SpriteRenderer sR;
  


    public void SetThousandCuts(int _skillDamage, float _percentExtraDamageOfSkill, float _damageTimer, float _skillSize) {
        skillDamage = _skillDamage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        damageTimer = _damageTimer;
        timer = damageTimer;
        skillSize = _skillSize;
        transform.localScale = new Vector3(skillSize, skillSize, 1);
        sR=GetComponent<SpriteRenderer>();
        PlayerManager.instance.player.gameObject.SetActive(false);

    }
    

    private void Update() {
        timer-=Time.deltaTime;
        if(timer<=0){
            timer=damageTimer;
            DoDamage();
            AudioManager.instance.PlaySFX(1);
        }
    }

    private void DoDamage(){
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position,sR.bounds.size.x/2);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if(enemiesToDamage[i].GetComponent<Enemy>()){
                EnemyStats enemyStats = enemiesToDamage[i].GetComponent<EnemyStats>();
                if(enemyStats!=null){
                    if (enemyStats != null)
                    {
                        PlayerManager.instance.player.charaterStats.DoPhysicalDamage(enemyStats, skillDamage, percentExtraDamageOfSkill);
                    }
        }
            }
        }
    }
    private void OnDestroy() {

        PlayerManager.instance.player.playerAnimationTrigger.OnAttackTrigger=null;
        PlayerManager.instance.player.gameObject.SetActive(true);
    }

}
