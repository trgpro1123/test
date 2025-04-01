using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region Components
    public Animator animator {get;private set;}
    public Rigidbody2D rb{get;private set;}
    public SpriteRenderer sp{get;private set;}
    public CapsuleCollider2D cd{get;private set;}
    public CharaterStats charaterStats{get;private set;}
    public EntityFX entityFX{get;private set;}
    #endregion


    [Header("Collison Info")]
    public Transform attackCheck;
    public Transform attackHitBox;
    [HideInInspector]protected SpriteRenderer sRAttackHitBox;



    protected bool isKnockBack=false;


    protected int defaultMoveSpeed;
    protected bool facingRight =true;
    public int facingDir{get;private set;} =1;

    
    public System.Action onFlipped;



    [HideInInspector]protected GameObject testObjectTemp;

    protected virtual void Awake() {
        
    }
    protected virtual void Start()
    {
        animator=GetComponentInChildren<Animator>();
        rb=GetComponent<Rigidbody2D>();
        charaterStats=GetComponent<CharaterStats>();
        sp=GetComponentInChildren<SpriteRenderer>();
        cd=GetComponent<CapsuleCollider2D>();
        if(attackHitBox!=null)
            sRAttackHitBox=attackHitBox.GetComponent<SpriteRenderer>();
        entityFX=GetComponent<EntityFX>();
        
    }

    
    protected virtual void Update()
    {
        
    }

    public void DamageImpact(GameObject _target,float _power,float _duration)=>StartCoroutine(HitKnockBack(_target,_power,_duration));
    private IEnumerator HitKnockBack(GameObject _target,float _power,float _duration){
        isKnockBack=true;
        Vector2 direction=(transform.position-_target.transform.position).normalized;
        rb.AddForce(direction*_power,ForceMode2D.Impulse);
        yield return new WaitForSeconds(_duration);
        isKnockBack=false;
        // SetupZeroKnockBack();
    }
    protected virtual void SetupZeroKnockBack(){
        
    }
    

    #region Velocity
    public void ZeroVelocity(){
        if(isKnockBack) return;
        rb.velocity=new Vector2(0,0);
    }
    public void SetRigidbody(float _x,float _y){
        if(isKnockBack) return;
        rb.velocity=new Vector2(_x,_y);
        FlipController(_x);
    }
    #endregion


    
    #region Flip
    public virtual void Flip(){
        facingDir*=(-1);
        facingRight=!facingRight;
        sp.flipX=!sp.flipX;
    }
    public virtual void FlipController(float _x){
        if(_x>0&&!facingRight) Flip();
        else if(_x<0&&facingRight) Flip();
    }

    public void FlipPlayer(){
        Vector2 positionMose=Input.mousePosition;
        Vector3 positionWorld=Camera.main.WorldToScreenPoint(transform.position);
        if(positionMose.x<positionWorld.x){
            if(facingRight) Flip();
        }
        else{
            if(!facingRight) Flip();
        }

        

    }
    #endregion
    
    public void RoutateSpriteRendererY(GameObject _object){
        if(!facingRight){
            _object.GetComponent<SpriteRenderer>().flipY=true;
        }
    }


    public void FreezeTimerFor(float _duration){
        StartCoroutine(FreezeTimerCoroutine(_duration));
    }
    private IEnumerator FreezeTimerCoroutine(float _duration)
    {
        FreezeTimer(true);
        yield return new WaitForSeconds(_duration);
        FreezeTimer(false);
    }
    public virtual void FreezeTimer(bool _isForzer){

    }




    public void ColdFor(float _duration,float _coldPercent){
        int coldValue=Mathf.RoundToInt(_coldPercent*charaterStats.moveSpeed.GetValue());
        charaterStats.IncreaseStatBy(-coldValue,_duration,charaterStats.moveSpeed);
        StartCoroutine(ColdForCoroutine(_duration,_coldPercent));
    }
    private IEnumerator ColdForCoroutine(float _duration,float _coldPercent){
        Cold(true, _coldPercent);
        yield return new WaitForSeconds(_duration);
        Cold(false, _coldPercent);
    }
    public virtual void Cold(bool _isCold,float _coldPercent){
        
    }

    

    public virtual void Die(){

    }

}
