using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossEnd : Enemy
{

    [Header("Nomar Attack")]
    public float attackDistance1;
    public float attackSize1;

    [Header("Attack 1")]
    public GameObject skillObject1;
    public float speed;
    public float lifeTime;
    public int numberProjecttiles;
    [Range(0,359)]  public float angleSpread;
    public float startingDistance=0.2f;
    public int chanceSkill1;
    public int defaultChanceSkill1;
    public float cooldownSkill1;
    public float lastTimeUseSkill1;

    [Header("Attack 2")]
    public GameObject skillObject2;
    public GameObject objectToSpawn2;
    public float radiusSkill2 = 3f;
    public float skill2DamagePerCent;
    public int maxCount;
    public float timeToNextCreateClone;
    public int chanceSkill2;
    public int defaultChanceSkill2;
    public float cooldownSkill2;
    public float lastTimeUseSkill2;

    [Header("Attack 3")]
    public GameObject skillObject3;
    public GameObject cloneBoss;
    public GameObject magicSummonObject;
    public float radiusSkill3;
    public int maxClone;
    public int chanceSkill3;
    public int defaultChanceSkill3;
    public float cooldownSkill3;
    public float lastTimeUseSkill3;
    public List<GameObject> listCloneBoss;
    public bool canUseSkill3=true;

    


    
    public BossEndIdleState idleState { get; private set; }
    public BossEndMoveState moveState { get; private set; }
    public BossEndDeathState deathState { get; private set; }
    public BossEndChasingState chasingState { get; private set; }
    public BossEndBattleState battleState { get; private set; }
    public BossEndAttackState attackState { get; private set; }
    public BossEndAttackState1 attackState1 { get; private set; }
    public BossEndAttackState2 attackState2 { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        idleState = new BossEndIdleState(this, stateMachine, "Idle", this);
        moveState = new BossEndMoveState(this, stateMachine, "Move", this);
        deathState = new BossEndDeathState(this, stateMachine, "Death", this);
        chasingState = new BossEndChasingState(this, stateMachine, "Move", this);
        battleState = new BossEndBattleState(this, stateMachine, "Idle", this);
        attackState = new BossEndAttackState(this, stateMachine, "Attack", this);
        attackState1 = new BossEndAttackState1(this, stateMachine, "Attack 1", this);
        attackState2 = new BossEndAttackState2(this, stateMachine, "Attack 2", this);
    }
    protected override void Start() {
        base.Start();
        isWaiting=true;
        stateMachine.Initialize(idleState);
        
        
        
    }
    protected override void Update()
    {
        base.Update();
        
    }

    public override void Die()
    {
        base.Die();
        if(listCloneBoss != null && listCloneBoss.Count > 0){
        List<GameObject> clonesToDestroy = new List<GameObject>(listCloneBoss);
        foreach(var clone in clonesToDestroy)
        {
            if(clone != null)
            {
                EnemyStats stats = clone.GetComponent<EnemyStats>();
                if(stats != null)
                {
                    stats.TakeDamage(99999);
                }
                else
                {
                    Destroy(clone);
                }
            }
        }
        listCloneBoss.Clear();
        }
        if(GameManager.instance != null)
        {
            GameManager.instance.StartDialogueEnd();
        }
        
        if(stateMachine != null)
        {
            stateMachine.ChangeState(deathState);
        }
    }

    public bool CanUseBossEndSkill1(){
        if(Random.Range(0,100)<=chanceSkill1){
            chanceSkill1=defaultChanceSkill1;
            return true;
        }
        chanceSkill1+=5;
        return false;
    }
    public bool CanUseBossEndSkill2(){
        if(Random.Range(0,100)<=chanceSkill2){
            chanceSkill2=defaultChanceSkill2;
            return true;
        }
        chanceSkill2+=5;
        return false;
    }
    public bool CanUseBossEndSkill3(){
        Debug.Log("Can use skill 3 "+canUseSkill3);
        if(canUseSkill3==false) return false;
        if(Random.Range(0,100)<=chanceSkill3){
            chanceSkill3=defaultChanceSkill3;
            return true;
        }
        chanceSkill3+=5;
        return false;
    }
    

    public override void Attack1()
    {
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        EnemyCreateAreaAttack(attackObject,StatType.strength,charaterStats.strength.GetValue(),attackDistance,angleToPlayer,0);
    }
    public override void Attack2()
    {
        CreateVolleyOfArrows();
    }
    public override void Attack3()
    {
        int damage=Mathf.RoundToInt(charaterStats.strength.GetValue()*skill2DamagePerCent);
        GameObject newSkillObject2 = Instantiate(skillObject2, transform.position, Quaternion.identity);
        newSkillObject2.GetComponent<BossEndSkillAttack2>().SetUpSkill(objectToSpawn2,damage,attackSize,attackDistance,maxCount,radiusSkill2,timeToNextCreateClone,this);
        this.gameObject.SetActive(false);
    }
    public override void Attack4()
    {
        GameObject newSkillObject3 = Instantiate(skillObject3, transform.position, Quaternion.identity);
        newSkillObject3.GetComponent<BossEndSkillAttack3>().SetUpSkill(cloneBoss,maxClone,radiusSkill3,magicSummonObject,this);

    }
    private void CreateVolleyOfArrows()
    {
        float startAngle,currentAngle,angleStep,endAngle;
        TargetConeOfInfluence(out startAngle,out endAngle,out currentAngle,out angleStep);
            for (int j = 0; j < numberProjecttiles; j++)
            {
                Vector2 pos= FindBulletSpawnPoint(currentAngle);
                GameObject newBossEndSkill1 = Instantiate(skillObject1, pos, Quaternion.identity);
                newBossEndSkill1.GetComponent<BossEndSkillAttack1>().SetUpSkill(charaterStats.strength.GetValue(),speed,lifeTime,charaterStats);
                newBossEndSkill1.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
                currentAngle+=angleStep;
            }
            currentAngle=startAngle; 
            Debug.Log(numberProjecttiles+" arrows created");
    }

    private void TargetConeOfInfluence(out float startAngle,out float endAngle,out float currentAngle,out float angleStep)
    {
        Vector2 targetDirection = player.attackCheck.transform.position-transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float haftAngleSpread = 0f;
        angleStep = 0f;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (numberProjecttiles - 1);
            haftAngleSpread = angleSpread / 2;
            startAngle = targetAngle - haftAngleSpread;
            endAngle = targetAngle + haftAngleSpread;
            currentAngle = startAngle;

        }
    }


    private Vector2 FindBulletSpawnPoint(float currentAngle){
        float x=transform.position.x+startingDistance*Mathf.Cos(currentAngle*Mathf.Deg2Rad);
        float y=transform.position.y+startingDistance*Mathf.Sin(currentAngle*Mathf.Deg2Rad);
        
        Vector2 pos=new Vector2(x,y);
        return pos;
    }


}
