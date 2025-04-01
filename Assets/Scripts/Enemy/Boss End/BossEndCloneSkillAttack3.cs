using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEndCloneSkillAttack3 : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;
    [SerializeField] private Transform attackHitBox;
    [SerializeField] private GameObject attackObject;
    private Enemy_BossEnd enemy;
    private GameObject testObjectTemp;
    private bool facingRight =true;
    private float angleToPlayer;
    private int damage;
    private float attackSize;
    private float attackDistance;
    private Player player;

    public void SetUpSkill(Enemy_BossEnd _enemy,int _damage,float _attackSize,float _attackDistance){
        enemy=_enemy;
        damage=_damage;
        attackSize=_attackSize;
        attackDistance=_attackDistance;
        player=PlayerManager.instance.player;
        FaceToPlayer();

    }


    public void OpenAttackArea(){
        attackArea.SetActive(true);
        angleToPlayer=AngleEnemyToPlayer();
        SpriteRenderer attackObjectSpriteRenderer = attackObject.GetComponent<SpriteRenderer>();
        attackArea.transform.rotation = Quaternion.Euler(0, 0, angleToPlayer);
        attackArea.GetComponent<SpriteRenderer>().size = new Vector2(Vector2.Distance(transform.position, transform.position + Quaternion.Euler(0, 0, angleToPlayer) * new Vector3(attackDistance, 0, 0)) + attackObjectSpriteRenderer.bounds.size.x / 2, attackObjectSpriteRenderer.bounds.size.y);
    }

    public void CloseAttackArea(){
        if(attackArea!=null)
            attackArea.SetActive(false);
    }
    public void CloseAttackHitBox(){
        if(attackHitBox!=null)
            attackHitBox.gameObject.SetActive(false);
    }
    public virtual void Flip(){
        facingRight=!facingRight;
        transform.Rotate(0,180,0);

    }
    public void FaceToPlayer(){
        
        if(player.transform.position.x<transform.position.x){
            if(facingRight) Flip();
        }
        else{
            if(!facingRight) Flip();
        }
    }
    public float AngleEnemyToPlayer(){
        Vector2 directionToPlayer = PlayerManager.instance.player.transform.position - transform.position;
        return Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
    }
    public void EnemyCreateAreaAttack(GameObject _attackAnimation,StatType _statType,int _damage,float _distance,float _angle,float _percentExtraDamageOfSkill=0)
    {
        // float angle = AnglePlayerToMouse();
        SpriteRenderer testSpriteRenderer = _attackAnimation.GetComponent<SpriteRenderer>();

        testObjectTemp = Instantiate(_attackAnimation, transform.position + Quaternion.Euler(0, 0, _angle) * new Vector3(_distance, 0, 0), Quaternion.Euler(0, 0, _angle));
        EnemyDamageHitBox(_statType,_damage, _percentExtraDamageOfSkill, _angle, testSpriteRenderer,testObjectTemp);

    }


    public void EnemyDamageHitBox(StatType _statType,int _damage, float _percentExtraDamageOfSkill, float _angle, SpriteRenderer _testSpriteRenderer,GameObject _object)
    {
        SpriteRenderer sRAttackHitBox = attackHitBox.GetComponent<SpriteRenderer>();
        attackHitBox.gameObject.SetActive(true);
        attackHitBox.rotation = Quaternion.Euler(0, 0, _angle);
        sRAttackHitBox.size = new Vector2(Vector2.Distance(transform.position, _object.transform.position) + _testSpriteRenderer.bounds.size.x / 2, _testSpriteRenderer.bounds.size.y);
        // Debug.Log("enemyDamageHitBox");
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(sRAttackHitBox.bounds.center, sRAttackHitBox.size, _angle);
        
        foreach (var item in colliders)
        {
            
            PlayerStats playerStats = item.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                // EnemyStats enemyStats = item.GetComponent<EnemyStats>();
                Debug.Log(playerStats.gameObject.name);
                enemy.charaterStats.DoPhysicalDamage(playerStats, _damage, _percentExtraDamageOfSkill);
                break;
            }
        }
        
    }
    public void Attack()
    {
        // attackSize=attackSize1;
        // attackDistance=attackDistance1;
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        EnemyCreateAreaAttack(attackObject,StatType.strength,damage,attackDistance,angleToPlayer,0);
    }
}
