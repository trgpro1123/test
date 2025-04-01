// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{

    public Transform destination;
    public Transform positionToPoint;
    public System.Action<Enemy> onDeath;
    public float idleTime=1f;
    [Header("Attack Info")]
    public float timeChasing;
    public GameObject attackObject;
    public float distanceDetect=6;
    public float attackDistance;
    public float attackSize;
    public GameObject attackArea;
    public float attackCoolDown=0.4f;
    public float minAttackCoolDown;
    public float maxAttackCoolDown;

    [HideInInspector] public float lastTimeAttack;
    public NavMeshAgent navMeshAgent;
    public float angleToPlayer;
    
    public string lastAnimBoolName {get;private set;}


    public EnemyStateMachine stateMachine {get;private set;}
    protected Player player;
    public float playerDetectTimer;


    private int layerToIgnore1;
    private int layerToIgnore2;
    private int layerToIgnore3;
    private int layerToIgnore4;
    private int totalLayerToIgnore;
    private int totalLayerDetect;
    private int layerDetect1;

    public Enemy_AnimationFinishTrigger animationFinishTrigger;
    public bool isWaiting=false;
    public bool attacked=false;
    public float defauletDistanceDetect;




    protected override void Awake() {
        base.Awake();
        stateMachine=new EnemyStateMachine();
        
        // bỏ qua layer Enemy
        layerToIgnore1 = LayerMask.NameToLayer("Enemy");
        layerToIgnore2 = LayerMask.NameToLayer("Default");
        layerToIgnore3 = LayerMask.NameToLayer("See Through Object");
        layerToIgnore4 = LayerMask.NameToLayer("Player Foot");
        layerDetect1 = LayerMask.NameToLayer("Obstacle");
        totalLayerToIgnore = ~(1 << layerToIgnore1 | 1 << layerToIgnore2 | 1 << layerToIgnore3 | 1 << layerToIgnore4);
        totalLayerDetect = (1 << layerToIgnore3 | 1 << layerDetect1 );
        animationFinishTrigger=GetComponentInChildren<Enemy_AnimationFinishTrigger>();
    }
    protected override void Start() {
        base.Start();
        navMeshAgent=GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation=false;
        navMeshAgent.updateUpAxis=false;
        navMeshAgent.speed=charaterStats.moveSpeed.GetValue();
        defauletDistanceDetect=distanceDetect;

        player=PlayerManager.instance.player;
    }
    protected override void Update() {
        base.Update();
        stateMachine.enemyState.Update();
    }
    



    public bool IsPlayerDetected() {
        Vector2 directionPlayer=((Vector2)PlayerManager.instance.player.transform.position-(Vector2)attackCheck.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(attackCheck.position,directionPlayer,distanceDetect,totalLayerToIgnore);
        if(hit.collider != null){

            if(hit.collider.gameObject.CompareTag("Player")){
                // Debug.DrawLine(positionToPoint.position, (Vector2)positionToPoint.position + directionPlayer * distanceDetect, Color.red);
                playerDetectTimer=timeChasing;
                return true;
            }

            else{
                // Debug.DrawLine(positionToPoint.position, (Vector2)positionToPoint.position + directionPlayer * distanceDetect, Color.blue);
                return false;
            }
        }

        return false;
    }
    public void AnimatorTrigger() => stateMachine.enemyState.AnimationFininshTrigger();

    public override void FreezeTimer(bool _isForzer){
        if(_isForzer){
            animator.speed=0;
            navMeshAgent.velocity=Vector3.zero;
            navMeshAgent.enabled=false;
            charaterStats.isForzerTime=true;
        }else{
            animator.speed=1;
            navMeshAgent.enabled=true;
            charaterStats.isForzerTime=false;
        }
    }
    public override void Cold(bool _isCold, float _coldPercent)
    {
        if(_isCold){
            animator.speed=1-_coldPercent;
            navMeshAgent.speed=charaterStats.moveSpeed.GetValue()*(1-_coldPercent);
            charaterStats.isCold=true;
        }else{
            animator.speed=1;
            navMeshAgent.speed=charaterStats.moveSpeed.GetValue();
            charaterStats.isCold=false;
        }
    }
    public void SlowDown(int _amount){
        float percentSlowDown=(float)_amount/(float)charaterStats.moveSpeed.GetValue();
        navMeshAgent.speed=(float)charaterStats.moveSpeed.GetValue()*(float)(1-percentSlowDown);

    }
    public IEnumerator SlowDownCoroutine(float _time,int _amount){
        SlowDown(_amount);
        yield return new WaitForSecondsRealtime(_time);
        navMeshAgent.speed=charaterStats.moveSpeed.GetValue();
    }

    public virtual void ChangeBattleState(){
        
    }
    public void AssignLastAnimBoolName(string _lastAnimBoolName){
        lastAnimBoolName=_lastAnimBoolName;
    }

    public virtual void AnimationSpecialAttackTrigger(){

    }
    public void MoveToDestination(Vector2 _destination){
        if(navMeshAgent.enabled==true)
            navMeshAgent.SetDestination(_destination);
    }
    public void CreateDestination(Vector2 _destination){
        destination=Instantiate(positionToPoint,_destination,Quaternion.identity);
    }

    public void DestroyDestination(){
        if(destination!=null)
            Destroy(destination.gameObject);
    }

    public virtual void Attack1(){
        
    }
    public virtual void Attack2(){
        
    }
    public virtual void Attack3(){
        
    }
    public virtual void Attack4(){
        
    }
    public override void Die(){
        base.Die();
        navMeshAgent.enabled=false;
        navMeshAgent.updatePosition=false;
        entityFX.StopAllEffect();
        onDeath?.Invoke(this);
    }
    public float AngleEnemyToPlayer(){
        Vector2 directionToPlayer = PlayerManager.instance.player.transform.position - transform.position;
        return Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
    }
    public virtual void SetAngleToPlayer(){
        angleToPlayer=AngleEnemyToPlayer();
    }
    public virtual void SetAngleSpecial(){

    }
    public void EnemyCreateAreaAttack(GameObject _attackAnimation,StatType _statType,int _damage,float _distance,float _angle,float _percentExtraDamageOfSkill=0)
    {
        SpriteRenderer testSpriteRenderer = _attackAnimation.GetComponent<SpriteRenderer>();

        testObjectTemp = Instantiate(_attackAnimation, transform.position + Quaternion.Euler(0, 0, _angle) * new Vector3(_distance, 0, 0), Quaternion.Euler(0, 0, _angle));
        if (!facingRight)
        {
            RoutateSpriteRendererY(testObjectTemp);
        }
        EnemyDamageHitBox(_statType,_damage, _percentExtraDamageOfSkill, _angle, testSpriteRenderer,testObjectTemp);

    }


    public void EnemyDamageHitBox(StatType _statType,int _damage, float _percentExtraDamageOfSkill, float _angle, SpriteRenderer _testSpriteRenderer,GameObject _object)
    {
        attackHitBox.gameObject.SetActive(true);
        attackHitBox.rotation = Quaternion.Euler(0, 0, _angle);
        sRAttackHitBox.size = new Vector2(Vector2.Distance(transform.position, _object.transform.position) + _testSpriteRenderer.bounds.size.x / 2, _testSpriteRenderer.bounds.size.y);
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(sRAttackHitBox.bounds.center, sRAttackHitBox.size, _angle);
        
        foreach (var item in colliders)
        {
            
            PlayerStats playerStats = item.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                Debug.Log(playerStats.gameObject.name);
                if(_statType==StatType.strength){
                    charaterStats.DoPhysicalDamage(playerStats, _damage, _percentExtraDamageOfSkill);
                }
                if(_statType==StatType.magicPower){
                    charaterStats.DoMagicalDamage(playerStats, _damage, _percentExtraDamageOfSkill);
                }
                break;
            }
        }
        
    }
    public void OpenAttackArea(){
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        attackArea.SetActive(true);
        SetAngleToPlayer();
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
    public virtual void SetAttackArea(float _attackSize,float _attackDistance){
        attackSize=_attackSize;
        attackObject.transform.localScale=new Vector2(attackSize,attackSize);
        attackDistance=_attackDistance;
        
    }
    public void NavMeshAgentStopByTime(float _time){
        if(navMeshAgent.enabled==true)
            StartCoroutine(NavMeshAgentStopByTimeCoroutine(_time));
    }
    IEnumerator NavMeshAgentStopByTimeCoroutine(float _time){
        navMeshAgent.isStopped=true;
        yield return new WaitForSeconds(_time);
        if(navMeshAgent.enabled==true)
            navMeshAgent.isStopped=false;
    }
    public Vector2 RandomPosition(){
        
        while(true){
        float angle = Random.Range(0, Mathf.PI * 2);
        float distance = Random.Range(3, 6);

        float x = transform.position.x + Mathf.Cos(angle) * distance; 
        float y = transform.position.y + Mathf.Sin(angle) * distance;
        Vector2 vectorToMove=new Vector2(x,y);
            RaycastHit2D hit = 
            Physics2D.Raycast(attackCheck.position,vectorToMove - (Vector2)attackCheck.position,distance,totalLayerDetect);
            Collider2D obCollider = 
                Physics2D.OverlapCircle(vectorToMove, 0.6f, totalLayerDetect);
            if(hit==false&&obCollider==null){
                return vectorToMove;
            }

        }
    }
    
}
