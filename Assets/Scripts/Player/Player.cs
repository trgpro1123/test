using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CurrentWeapon{
    None,
    Sword,
    Gauntlet,
    Bow
}
public class Player : Entity
{
    [Header("Attack Info")]
    public Vector2[] attackMovement; 
 
    [Header("Rolling Info")]
    public float rollCooldown=0.2f;
    private bool canRoll=true;
    public Vector2 rollDir{get;private set;}
    public bool isRolling=false;


    [Header("Current Weapon")]

    public CurrentWeapon currentWeapon;


    
    

    
    public bool isBusy;
    public bool canUseSkill;
    [HideInInspector]public Vector3 mousePosition;
    private int originalEnemyLayerMask;
    private int enemyLayer;
    private int playerLayer;
    public bool canUseGate;

    public SkillManager skillManager;
    public PlayerAnimationTrigger playerAnimationTrigger;

    public PlayerStateMachine stateMachine {get;private set;}
    public PlayerIdleState idleState {get;private set;}
    public PlayerMoveState moveState {get;private set;}
    public PlayerPrimarySwordAttackState primarySwordAttackState {get;private set;}

    public PlayerSwordSlashSkillAttackState heavyCutterAttackState {get;private set;}
    public PlayerSwordSlashSkillAttackState bladeThrowState {get;private set;}
    public PlayerSwordSlashSkillAttackState bladeCycloneState {get;private set;}
    public PlayerSwordDashSkillState swordDashSkillState {get;private set;}
    public PlayerChaosSlashSkillAttackState chaosSlashesState {get;private set;}
    public PlayerThousandCutsState thousandCutsState {get;private set;}
    public PlayerBlinkBladeState blinkBladeState {get;private set;}





    public PlayerArcheryState archeryState {get;private set;}
    public PlayerFlurryOfArrowsSkillState flurryOfArrowsSkillState {get;private set;}






    public PlayerGauntletPrimaryAttackState gauntletPrimaryAttackState{get;private set;}
    public PlayerGauntletSkillAttackState gauntletSkillAttackState {get;private set;}
    public PlayerSavageBarrageSkillState savageBarrageSkillState {get;private set;}
    public PlayerEnergyPalmSkillState energyPalmSkillState {get;private set;}
    public PlayerEarthquakeJumpSkillState earthquakeJumpSkillState {get;private set;}
    public PlayerSeismicShockwaveSkillState seismicShockwaveSkillState {get;private set;}




    public PlayerPreUseSkillState preUseSkillState {get;private set;}
    public PlayerUseSkillState useSkillState {get;private set;}
    public PlayerUltimateExplosionSkillState ultimateExplosionSkillState {get;private set;}




    public PlayerBowPrimaryAttackState bowPrimaryAttackState {get;private set;}
    public PlayerRollingState rollingState {get;private set;}
    public PlayerDeathState deathState {get;private set;}


    public IInteractable Interactable { get; set; }








    protected override void Awake()
    {

        base.Awake();
        Interactable=null;
        playerAnimationTrigger=GetComponentInChildren<PlayerAnimationTrigger>();
        stateMachine=new PlayerStateMachine();
        skillManager=SkillManager.instance;
        idleState=new PlayerIdleState(this,stateMachine,"Idle");
        moveState=new PlayerMoveState(this,stateMachine,"Move");

        primarySwordAttackState=new PlayerPrimarySwordAttackState(this,stateMachine,"SwordAttack");
        heavyCutterAttackState=new PlayerSwordSlashSkillAttackState(this,stateMachine,"SwordSlashSkillAttack");
        bladeThrowState=new PlayerSwordSlashSkillAttackState(this,stateMachine,"SwordSlashSkillAttack");
        bladeCycloneState=new PlayerSwordSlashSkillAttackState(this,stateMachine,"SwordSlashSkillAttack");
        swordDashSkillState=new PlayerSwordDashSkillState(this,stateMachine,"SwordDashSkill");
        chaosSlashesState=new PlayerChaosSlashSkillAttackState(this,stateMachine,"SwordChaosSlashSkillAttack");
        thousandCutsState=new PlayerThousandCutsState(this,stateMachine,"ThousandCutsSkillAttack");
        blinkBladeState=new PlayerBlinkBladeState(this,stateMachine,"BlinkBladeSkillAttack");


        archeryState=new PlayerArcheryState(this,stateMachine,"Archery");
        flurryOfArrowsSkillState=new PlayerFlurryOfArrowsSkillState(this,stateMachine,"FlurryOfArrowsSkillAttack");


        gauntletSkillAttackState=new PlayerGauntletSkillAttackState(this,stateMachine,"GauntletSkillAttack");
        gauntletPrimaryAttackState=new PlayerGauntletPrimaryAttackState(this,stateMachine,"GauntletAttack");
        savageBarrageSkillState=new PlayerSavageBarrageSkillState(this,stateMachine,"SavageBarrageSkillAttack");
        energyPalmSkillState=new PlayerEnergyPalmSkillState(this,stateMachine,"EnergyPalmSkillAttack");
        earthquakeJumpSkillState=new PlayerEarthquakeJumpSkillState(this,stateMachine,"EarthquakeJumpSkillAttack");
        seismicShockwaveSkillState=new PlayerSeismicShockwaveSkillState(this,stateMachine,"EarthquakeJumpSkillAttack");




        preUseSkillState=new PlayerPreUseSkillState(this,stateMachine,"PreUseSkill");
        useSkillState=new PlayerUseSkillState(this,stateMachine,"UseSkill");
        ultimateExplosionSkillState=new PlayerUltimateExplosionSkillState(this,stateMachine,"UltimateExplosionSkill");





        bowPrimaryAttackState=new PlayerBowPrimaryAttackState(this,stateMachine,"BowAttack");
        deathState=new PlayerDeathState(this,stateMachine,"Death");
        rollingState=new PlayerRollingState(this,stateMachine,"Rolling");

        attackHitBox.gameObject.SetActive(false);
        isRolling=false;
        


    }

    protected override void Start()
    {
        base.Start();
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerLayer = LayerMask.NameToLayer("Player Foot");
        originalEnemyLayerMask = Physics2D.GetLayerCollisionMask(enemyLayer);
        stateMachine.Initialize(idleState);
    }
    private void OnEnable() {
        Start();
        stateMachine.ChangeState(idleState);
    }

    protected override void Update()
    {
        base.Update();


        stateMachine.playerState.Update();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(charaterStats.isForzerTime||isBusy||!canRoll) return;
        if(Input.GetKeyDown(KeyCode.F)&&UI.instance.ingameUI.IsDialogueOpen()==false){
            Interactable?.Interact();
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            Inventory.instance.SwapWeapons();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            float xInput=Input.GetAxisRaw("Horizontal");
            float yInput=Input.GetAxisRaw("Vertical");

            rollDir=new Vector2(xInput,yInput).normalized;
            
            if(xInput==0&&yInput==0)
                rollDir=new Vector2(facingDir,0).normalized;
            stateMachine.ChangeState(rollingState);
            StartCoroutine(CanRollCoroutine(rollCooldown));
            
        }
        
    }
    public IEnumerator CanRollCoroutine(float _time){
        canRoll=false;
        yield return new WaitForSeconds(_time);
        canRoll=true;
    }

    public IEnumerator Busy(float _time){
        isBusy=true;
        yield return new WaitForSeconds(_time);
        isBusy=false;

    }
    public void SetIsRolling(bool _isRolling){
        isRolling=_isRolling;
    }

    public void AnimatorTrigger() => stateMachine.playerState.AnimationFininshTrigger();
    public void AttackOpenTrigger(){
       
        
    }
    

    public void AttackEndTrigger(){
        attackHitBox.gameObject.SetActive(false);
    }
    public override void Die()
    {
        Debug.Log("Player Die");
        isBusy=true;
        stateMachine.ChangeState(deathState);
        UI.instance.SwitchOnEndScreen();
    }
    
    public bool IsFacingRight(){
        return facingRight;
    }
    public float AnglePlayerToMouse(){
        Vector2 directionToMouse = mousePosition - transform.position;
        return Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
    }
    public void PlayerCreateAreaAttack(GameObject _attackAnimation,StatType _statType,int _damage,float _percentExtraDamageOfSkill,float _distance,float _angle)
    {
        SpriteRenderer testSpriteRenderer = _attackAnimation.GetComponent<SpriteRenderer>();

        testObjectTemp = Instantiate(_attackAnimation, transform.position + Quaternion.Euler(0, 0, _angle) * new Vector3(_distance, 0, 0), Quaternion.Euler(0, 0, _angle));
        if (!facingRight)
        {
            RoutateSpriteRendererY(testObjectTemp);
        }
        PlayerDamageHitBox(_statType,_damage, _percentExtraDamageOfSkill, _angle, testSpriteRenderer,testObjectTemp);

    }


    public void PlayerDamageHitBox(StatType _statType,int _damage, float _percentExtraDamageOfSkill, float _angle, SpriteRenderer _testSpriteRenderer,GameObject _object)
    {
        Vector3 center = sRAttackHitBox.bounds.center;
    Vector3 size = sRAttackHitBox.size;
    
    // Vẽ hình chữ nhật debug
    StartCoroutine(DrawDebugBox(center, size, _angle, 2f)); // Hiển thị trong 2 giây
        
        attackHitBox.gameObject.SetActive(true);
        attackHitBox.rotation = Quaternion.Euler(0, 0, _angle);
        sRAttackHitBox.size = new Vector2(Vector2.Distance(transform.position, _object.transform.position) + _testSpriteRenderer.bounds.size.x / 2, _testSpriteRenderer.bounds.size.y);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(sRAttackHitBox.bounds.center, sRAttackHitBox.size, _angle);
        foreach (var item in colliders)
        {
            EnemyStats enemyStats = item.gameObject.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {

                if(_statType==StatType.strength){
                    charaterStats.DoPhysicalDamage(enemyStats, _damage, _percentExtraDamageOfSkill);
                }
                else if(_statType==StatType.magicPower){
                    charaterStats.DoMagicalDamage(enemyStats, _damage, _percentExtraDamageOfSkill);
                }
            }
        }
        attackHitBox.gameObject.SetActive(false);
    }


    private IEnumerator DrawDebugBox(Vector3 center, Vector2 size, float angle, float duration)
{
    float timer = 0;
    while (timer < duration)
    {
        // Vẽ khung hình chữ nhật quay theo góc angle
        Vector3 halfSize = new Vector3(size.x/2, size.y/2, 0);
        
        // Tính toán 4 góc của hình chữ nhật
        Vector3 topLeft = RotatePointAroundPivot(center + new Vector3(-halfSize.x, halfSize.y, 0), center, angle);
        Vector3 topRight = RotatePointAroundPivot(center + new Vector3(halfSize.x, halfSize.y, 0), center, angle);
        Vector3 bottomRight = RotatePointAroundPivot(center + new Vector3(halfSize.x, -halfSize.y, 0), center, angle);
        Vector3 bottomLeft = RotatePointAroundPivot(center + new Vector3(-halfSize.x, -halfSize.y, 0), center, angle);
        
        // Vẽ 4 cạnh của hình chữ nhật
        Debug.DrawLine(topLeft, topRight, Color.red);
        Debug.DrawLine(topRight, bottomRight, Color.red);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red);
        Debug.DrawLine(bottomLeft, topLeft, Color.red);
        
        timer += Time.deltaTime;
        yield return null;
    }
}

private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angle)
{
    Vector3 dir = point - pivot;
    dir = Quaternion.Euler(0, 0, angle) * dir;
    return pivot + dir;
}






















    public override void FreezeTimer(bool _isForzer){
        if(_isForzer){
            animator.speed=0;
            charaterStats.isForzerTime=true;
        }else{
            animator.speed=1;
            charaterStats.isForzerTime=false;
        }
    }
    public override void Cold(bool _isCold, float _coldPercent)
    {
        if(_isCold){
            animator.speed=1-_coldPercent;
            charaterStats.isCold=true;
        }else{
            animator.speed=1;
            charaterStats.isCold=false;
        }
    }

    
    public void WeaponChange(WeaponType _weaponType){
        ResetWeaponActive();
        if(_weaponType==WeaponType.Bow){
            currentWeapon=CurrentWeapon.Bow;
        }
        else if(_weaponType==WeaponType.Gauntlet){
            currentWeapon=CurrentWeapon.Gauntlet;
        }
        else if(_weaponType==WeaponType.Sword){
            currentWeapon=CurrentWeapon.Sword;
        }
        
    }
    public void ResetWeaponActive(){
        currentWeapon=CurrentWeapon.None;
    }
    public void DisableEnemyPlayerCollision()
    {
        Physics2D.SetLayerCollisionMask(enemyLayer, originalEnemyLayerMask & ~(1 << playerLayer));
    }
    public void EnableEnemyPlayerCollision()
    {
        Physics2D.SetLayerCollisionMask(enemyLayer, originalEnemyLayerMask | (1 << playerLayer));
    }
    



    
}
