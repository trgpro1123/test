using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossCave : Enemy
{

    [Header("Skill Attack 1")]
    public Transform transformListProjecttile;
    public float skill1DamagePerCent;
    public int numberProjecttiles=50;
    public float lifeTimeProjecttile;
    public float speedProjectile;
    [Range(0,359)]  public float angleSpread=353;
    public float startingDistance=0.2f;
    public int chanceSkill1;
    public int defaultChanceSkill1;
    public float cooldownSkill1;
    public float lastTimeUseSkill1;
    private List<GameObject> listProjecttiles=new List<GameObject>();

    [Header("Skill Attack 2")]
    public GameObject skillObject2;
    public Transform posstionSkillAttack2;
    public float skilAttack2Size;
    public float skill2DamagePerCent;
    public int chanceSkill2;
    public int defaultChanceSkill2;
    public float cooldownSkill2;
    public float lastTimeUseSkill2;
    [Header("Skill Buff")]
    [Range(0,1)]  public float percentHealthToUseSkill;
    public bool isBuff;
    


    
    public BossCaveIdleState idleState { get; private set; }
    public BossCaveMoveState moveState { get; private set; }
    public BossCaveDeathState deathState { get; private set; }
    public BossCaveChasingState chasingState { get; private set; }
    public BossCaveBattleState battleState { get; private set; }
    public BossCaveAttackState attackState { get; private set; }
    public BossCaveAttackState1 attackState1 { get; private set; }
    public BossCavePrepareAttackState2 prepareAttackState2 { get; private set; }
    public BossCaveAttackState2 attackState2 { get; private set; }
    public BossCaveAttackState3 attackState3 { get; private set; }
  

    protected override void Awake()
    {
        base.Awake();
        idleState = new BossCaveIdleState(this, stateMachine, "Idle", this);
        moveState = new BossCaveMoveState(this, stateMachine, "Move", this);
        deathState = new BossCaveDeathState(this, stateMachine, "Death", this);
        chasingState = new BossCaveChasingState(this, stateMachine, "Move", this);
        battleState = new BossCaveBattleState(this, stateMachine, "Idle", this);
        attackState = new BossCaveAttackState(this, stateMachine, "Attack", this);
        attackState1 = new BossCaveAttackState1(this, stateMachine, "Attack 1", this);
        prepareAttackState2 = new BossCavePrepareAttackState2(this, stateMachine, "Prepare Attack 2", this);
        attackState2 = new BossCaveAttackState2(this, stateMachine, "Attack 2", this);
        attackState3 = new BossCaveAttackState3(this, stateMachine, "Attack 3", this);

    }
    protected override void Start() {
        base.Start();
        GetListProjecttile();
        stateMachine.Initialize(idleState);
        
        
        
    }
    protected override void Update()
    {
        base.Update();
        
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }
    public void GetListProjecttile(){
        foreach(Transform child in transformListProjecttile){
            listProjecttiles.Add(child.gameObject);
        }
    }

    public bool CanUseBossCaveSkill1(){
        if(Random.Range(0,100)<=chanceSkill1){
            chanceSkill1=defaultChanceSkill1;
            return true;
        }
        chanceSkill1+=5;
        return false;
    }
    public bool CanUseBossCaveSkill2(){
        if(Random.Range(0,100)<=chanceSkill2){
            chanceSkill2=defaultChanceSkill1;
            return true;
        }
        chanceSkill2+=5;
        return false;
    }

    

    public override void Attack1()
    {
        // attackSize=attackSize1;
        // attackDistance=attackDistance1;
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        EnemyCreateAreaAttack(attackObject,StatType.strength,charaterStats.strength.GetValue(),attackDistance,angleToPlayer,0);
    }
    public override void Attack2()
    {
        CreateSkillAttack2();
        
    }
    public override void Attack3()
    {
        
        int damage=Mathf.RoundToInt(charaterStats.magicPower.GetValue()*skill2DamagePerCent);
        GameObject newSkill2=Instantiate(skillObject2,posstionSkillAttack2.transform.position,Quaternion.Euler(0, 0, angleToPlayer),transform);
        newSkill2.GetComponent<BossCaveSkillAttack3>().SetProjectile(damage,skilAttack2Size,this);
    }
    public override void SetAngleSpecial()
    {
        Vector2 directionToPlayer = PlayerManager.instance.player.transform.position - posstionSkillAttack2.transform.position;
        angleToPlayer= Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
    }
    public override void Attack4()
    {
        BuffSkill();
    }


    private void CreateSkillAttack2()
    {
        float startAngle,currentAngle,angleStep,endAngle;
        int damage=Mathf.RoundToInt(charaterStats.strength.GetValue()*skill1DamagePerCent);
        TargetConeOfInfluence(out startAngle,out endAngle,out currentAngle,out angleStep);
            for (int i = 0; i < numberProjecttiles; i++)
            {
                Vector2 pos= FindProjectileSpawnPoint(currentAngle);
                // GameObject newArrow = Instantiate(skillObject2[i], pos, Quaternion.identity);
                listProjecttiles[i].SetActive(true);
                listProjecttiles[i].transform.position=pos;
                listProjecttiles[i].transform.parent = null;
                listProjecttiles[i].GetComponent<BossCaveProjectle>().SetProjectile(damage,speedProjectile,lifeTimeProjecttile,charaterStats);
                listProjecttiles[i].transform.rotation = Quaternion.Euler(0, 0, currentAngle-90);
                currentAngle+=angleStep;
            }
            currentAngle=startAngle; 
    }

    private void TargetConeOfInfluence(out float startAngle,out float endAngle,out float currentAngle,out float angleStep)
    {
        // Vector3 mousePosition=Input.mousePosition;
        // mousePosition=Camera.main.ScreenToWorldPoint(mousePosition);
        // Vector2 targetDirection = mousePosition - player.attackCheck.transform.position;
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


    private Vector2 FindProjectileSpawnPoint(float currentAngle){
        float x=transform.position.x+startingDistance*Mathf.Cos(currentAngle*Mathf.Deg2Rad);
        float y=transform.position.y+startingDistance*Mathf.Sin(currentAngle*Mathf.Deg2Rad);
        
        Vector2 pos=new Vector2(x,y);
        return pos;
    }
    public void BuffSkill(){
        maxAttackCoolDown/=2;
        cooldownSkill1/=2;
        cooldownSkill2/=2;
        int arrmorBuff=Mathf.RoundToInt(charaterStats.armor.GetValue()*0.2f);
        int strengthBuff=Mathf.RoundToInt(charaterStats.strength.GetValue()*0.2f);
        int magicPowerBuff=Mathf.RoundToInt(charaterStats.magicPower.GetValue()*0.2f);
        charaterStats.armor.AddModifier(arrmorBuff);
        charaterStats.strength.AddModifier(strengthBuff);
        charaterStats.magicPower.AddModifier(magicPowerBuff);
        
    }
}
