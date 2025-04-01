using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatType{
    health,
    strength,
    armor,
    agility,
    Lifesteal,
    moveSpeed,
    critChance,
    magicPower,
    magicResistance,
    magicRegeneration,
    luck,
    coolDown
}
public class CharaterStats : MonoBehaviour
{
    public Stat health;
    public Stat strength;
    public Stat armor;
    public Stat agility;
    public Stat Lifesteal;
    public Stat moveSpeed;
    public Stat critChance;
    public Stat magicPower;
    public Stat magicResistance;
    public Stat magicRegeneration;
    public Stat luck;
    public Stat coolDown;


    public int currentHealth;
    public int currentMana;
    public bool isDead {get;private set;}
    public SkillManager skillManager;
    public Entity entity;
    public EntityFX entityFX;

    public System.Action onHealthChanged;
    

    protected HealthBar_UI healthBar_UI;
    public bool suddenDeathActived;
    public bool wasImmortal=false;



    
    public bool isImmortal;
    private Inventory inventory;


    public float bleedEffectDuration;
    private float bleedPerTime;
    private float bleedTimer;
    private int bleedDamage;
    private float reverseControlsEffectDuration;
    private float coldEffectDuration;
    public bool isCold=false;
    public bool isBleeding=false;
    public bool isReverseControls=false;
    public bool isForzerTime;


    




    protected virtual void Start(){
        skillManager=SkillManager.instance;
        entity=GetComponent<Entity>();
        entityFX=GetComponent<EntityFX>();
        inventory=Inventory.instance;
        healthBar_UI=GetComponentInChildren<HealthBar_UI>();
        isForzerTime=false;
        Invoke("UpdateBeforeStart",0.01f);
        
    }
    public virtual void UpdateBeforeStart(){
        currentHealth=GetMaxHealth();
        if(onHealthChanged!=null)
            onHealthChanged();
    }
    protected virtual void Update() {
        if(bleedEffectDuration>0){
            bleedEffectDuration-=Time.deltaTime;
        }
        if(reverseControlsEffectDuration>0){
            reverseControlsEffectDuration-=Time.deltaTime;
        }
        if(coldEffectDuration>0){
            coldEffectDuration-=Time.deltaTime;
        }

        ApplyBleedEffect();
        ApplyReverseControlsEffect();
        ApplyColdEffect();

        
    }
    
    


    #region Stat caculations
    protected bool TargetCanAvoidAttack(CharaterStats _targetStat){
        int totalEvation=_targetStat.agility.GetValue();
        if(Random.Range(0,100)<totalEvation){
            _targetStat.OnEvasion();
            return true;
        }
        return false;
    }
    protected virtual int CheckTargetArmor(CharaterStats _targetStat,int _totalDamage){
        _totalDamage-=_targetStat.armor.GetValue();
        _totalDamage=Mathf.Clamp(_totalDamage,0,int.MaxValue);
        return _totalDamage;
    }
    protected bool CanCrit(){
        int totalCritChance=Mathf.Clamp(critChance.GetValue()+luck.GetValue(),0,50);
        if(Random.Range(0,100)<totalCritChance) return true;
        return false;
    }

    protected int CheckTargetMagicresistance(CharaterStats _targetStat, int totalMagicDamage)
    {
        totalMagicDamage -= magicResistance.GetValue();
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }
    public int GetMaxHealth(){
        return health.GetValue();
    }
    public int GetStrength(){
        return strength.GetValue();
    }
    public int GetMagicPower(){
        return magicPower.GetValue();
    }
    public virtual int GetMaxMana(){
        return magicPower.GetValue()*3;
    }
    #endregion



    
    

    #region Magical Damage and Ailemnts

    public virtual void DoPhysicalDamage(CharaterStats _targetStat,int _damageOfSkill,float _percentExtraDamageOfSkill=0){
        if(TargetCanAvoidAttack(_targetStat)){
            Debug.Log("Target Avoided Attack");
            return;
        }
        float percentDamageOfSkill=Mathf.Clamp01(_percentExtraDamageOfSkill);
        int totalDamage=_damageOfSkill;
        if(_percentExtraDamageOfSkill!=0){
            totalDamage+=Mathf.RoundToInt(strength.GetValue()*percentDamageOfSkill);
        }
        if(CanCrit()){
            totalDamage*=2;
        }
        totalDamage=CheckTargetArmor(_targetStat,totalDamage);
        _targetStat.TakeDamage(totalDamage);
        
    }
    public virtual void DoMagicalDamage(CharaterStats _targetStat,int _damageOfSkill,float _percentExtraDamageOfSkill=0){
        if(TargetCanAvoidAttack(_targetStat)){
            Debug.Log("Target Avoided Attack");
            return;
        }
        float percentDamageOfSkill=Mathf.Clamp01(_percentExtraDamageOfSkill);
        int totalDamage=_damageOfSkill;
        if(_percentExtraDamageOfSkill!=0){
            totalDamage+=Mathf.RoundToInt(magicPower.GetValue()*percentDamageOfSkill);
        }
        if(CanCrit()){
            totalDamage*=2;
        }
        totalDamage=CheckTargetMagicresistance(_targetStat,totalDamage);
        _targetStat.TakeDamage(totalDamage);
    }
    #endregion

    public virtual void TakeDamage(int _damage){
        if(isImmortal) return;
        DecreaseHealthBy(_damage);
        if(currentHealth<=0 && !isDead) Die();
        return;

    } 
    public void IncreaseStatBy(int _amountModifier,float _duration,Stat _statModifier){
        StartCoroutine(StatModCoroutine(_amountModifier,_duration,_statModifier));
    }
    private  IEnumerator StatModCoroutine(int _amountModifier,float _duration,Stat _statModifier){
        _statModifier.AddModifier(_amountModifier);
        inventory.UpdateSlotUI();
        yield return new WaitForSeconds(_duration);
        _statModifier.RemoveModifier(_amountModifier);
        inventory.UpdateSlotUI();
    }
    public virtual void IncreaseHealBy(int _heal){
        currentHealth+=_heal;
        Debug.Log("Heal "+_heal);
        if(currentHealth>GetMaxHealth()){
            currentHealth=GetMaxHealth();
        }
        if(onHealthChanged!=null) onHealthChanged();
    }
    public virtual void DecreaseHealthBy(int _damage){
        Debug.Log("Take Damage "+_damage);
        currentHealth-=_damage;

        if(onHealthChanged!=null) onHealthChanged();
    }


    public virtual void OnEvasion(){

    }
    public void MakeInvinsable(bool _isInvisable)=> isImmortal=_isInvisable;
    public IEnumerator InvisableByTime(float _time){
        MakeInvinsable(true);
        yield return new WaitForSeconds(_time);
        MakeInvinsable(false);
    }
    public void ResetImmortal(){
        wasImmortal=false;
    }

    public void SetUpBleedEffect(int _damage,float _percentExtraDamage,float _bleedEffectDuration,float _bleedDuration){
        bleedDamage=_damage+Mathf.RoundToInt(strength.GetValue()*_percentExtraDamage);
        if(bleedEffectDuration>0){
            bleedEffectDuration+=_bleedEffectDuration;
        }else{

            bleedEffectDuration=_bleedEffectDuration;
        }
        if(!isBleeding){
            isBleeding=true;
            entityFX.BleedEffect(true);
            bleedPerTime=_bleedDuration;
            bleedTimer=_bleedDuration;
        }
    }
    public virtual void ApplyBleedEffect(){
        if(bleedEffectDuration>0){
            bleedTimer-=Time.deltaTime;
            if(bleedTimer<=0){
                TakeDamage(bleedDamage);
                bleedTimer=bleedPerTime;
            }

        }
        if(bleedEffectDuration<0&&isBleeding){
            bleedEffectDuration=0;
            isBleeding=false;
            entityFX.BleedEffect(false);
        }
    }
    public virtual void ApplyStunEffect(float _stunEffectDuration){

        entity.FreezeTimerFor(_stunEffectDuration);

    }
    public virtual void ApplyFastEffect(float _fastEffectDuration,int _fastEffectAmount){
        IncreaseStatBy(_fastEffectAmount,_fastEffectDuration,moveSpeed);
        entityFX.FastEffectForTime(1);
    }
    public virtual void ApplySlowEffect(float _slowEffectDuration,int _slowEffectAmount){
        IncreaseStatBy(_slowEffectAmount,_slowEffectDuration,moveSpeed);
        entityFX.SlowEffectForTime(1);
    }
    public virtual void SetUpReverseControlsEffect(float _reverseControlsEffectDuration){
        if(bleedEffectDuration>0){
            reverseControlsEffectDuration+=_reverseControlsEffectDuration;
        }else{

            reverseControlsEffectDuration=_reverseControlsEffectDuration;
        }
        entityFX.ReverseControlsEffect(true);
        isReverseControls=true;
    }
    public virtual void ApplyReverseControlsEffect(){
        if(reverseControlsEffectDuration<0&&isReverseControls){
            reverseControlsEffectDuration=0;
            entityFX.ReverseControlsEffect(false);
            isReverseControls=false;
        }
    }
    public virtual void SetUpColdEffect(float _coldEffectDuration){
        if(coldEffectDuration>0){
            coldEffectDuration+=_coldEffectDuration;
        }else{

            coldEffectDuration=_coldEffectDuration;
        }
        entityFX.ColdEffect(true);
        isCold=true;
    }
    public virtual void ApplyColdEffect(){
        if(coldEffectDuration<0&&isCold){
            coldEffectDuration=0;
            entityFX.ColdEffect(false);
            isCold=false;
        }
    }


    protected virtual void Die(){
        isDead=true;
    }
    public void ResetDie(){
        isDead=false;
    }
    public void KillYourSelf()=>Die();


    public Stat GetType(StatType _statType){
        if(_statType==StatType.health) return health;
        else if(_statType==StatType.strength) return strength;
        else if(_statType==StatType.armor) return armor;
        else if(_statType==StatType.agility) return agility;
        else if(_statType==StatType.Lifesteal) return Lifesteal;
        else if(_statType==StatType.moveSpeed) return moveSpeed;
        else if(_statType==StatType.critChance) return critChance;
        else if(_statType==StatType.magicPower) return magicPower;
        else if(_statType==StatType.magicResistance) return magicResistance;
        else if(_statType==StatType.magicRegeneration) return magicRegeneration;
        else if(_statType==StatType.luck) return luck;
        else if(_statType==StatType.coolDown) return coolDown;
        else return null;

    }
}
