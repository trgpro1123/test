using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharaterStats
{
    private Player player;
    public System.Action onManaUIChanged;

    private PlayerFX playerFX;
    private float manaRegenerationTimer;
    public bool hadBuffSpeed=false;
    public bool hadDebuffSpeed=false;
    
    protected override void Start() {
        base.Start();
        player=GetComponent<Player>();
        playerFX=GetComponent<PlayerFX>();
        Invoke("UpdateBeforeStart",0.3f);
        
    }
    public override void UpdateBeforeStart()
    {
        base.UpdateBeforeStart();
        UI.instance.ingameUI.UpdateHealth();
        UI.instance.ingameUI.UpdateMana();
        currentMana=GetMaxMana();
        // Debug.Log("Update Before Start, current health: "+currentHealth);
        // Debug.Log("Update Before Start, current mana: "+currentMana);
        UI.instance.ingameUI.UpdateMana();
    }
    protected override void Update() {
        base.Update();
        ManaRegenerationPerSecond();

        // if(Input.GetKeyDown(KeyCode.Alpha5)){
        //     SetUpBleedEffect(10,0,5,1);
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha6)){
        //     ApplyStunEffect(2f);
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha7)){
        //     ApplyFastEffect(2f,10);
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha8)){
        //     ApplySlowEffect(2f,-5);
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha9)){
        //     IncreaseHealBy(100);
        // }
        if(Input.GetKeyDown(KeyCode.Alpha0)){

            // TakeDamage(1000);
            SetUpReverseControlsEffect(5f);

        }
    }
    public void ManaRegenerationPerSecond(){
        if(currentMana>=GetMaxMana()) return;
        if(manaRegenerationTimer<=0){
            IncreaseManaBy(EnergyRegenerationCalculation());
            manaRegenerationTimer=1f;
        }
        else{
            manaRegenerationTimer-=Time.deltaTime;
        }
    }
    public int EnergyRegenerationCalculation(){
        return Mathf.RoundToInt(magicRegeneration.GetValue()/10);
    }
    protected override int CheckTargetArmor(CharaterStats _targetStat, int _totalDamage)
    {
        if(skillManager.passThroughArmorSkill.passThroughArmorUnlocked){
            _totalDamage-=Mathf.RoundToInt(_targetStat.armor.GetValue()*0.8f);
        }
        else{

            _totalDamage-=_targetStat.armor.GetValue();
        }
        return _totalDamage;
    }
    public override void DoPhysicalDamage(CharaterStats _targetStat, int _damageOfSkill, float _percentExtraDamageOfSkill = 0)
    {
        if(TargetCanAvoidAttack(_targetStat)){
            return;
        }

        int currentTargetHealth=_targetStat.currentHealth;
        float percentDamageOfSkill=Mathf.Clamp01(_percentExtraDamageOfSkill);
        int totalDamage=_damageOfSkill;
        if(_percentExtraDamageOfSkill!=0){
            totalDamage+=Mathf.RoundToInt(GetStrength()*percentDamageOfSkill);
        }
        
        if(skillManager.suddenDeathSkill.suddenDeathUnlocked){
            if(currentTargetHealth<=currentTargetHealth*0.15f && Random.Range(0,100)<=20){
                _targetStat.TakeDamage(999999);
                return;
            }
        }
        
        if(skillManager.crushingImpactSkill.crushingImpactUnlocked&&player.currentWeapon==CurrentWeapon.Gauntlet){
            totalDamage+=Mathf.RoundToInt(GetStrength()*0.05f);
        }
        if(skillManager.perfectStateSkill.perfectStateUnlocked&&currentHealth>=GetMaxHealth()*0.9f){
            totalDamage+=Mathf.RoundToInt(GetStrength()*0.1f);
        }
        bool canApplyOverwhelmingPower= currentTargetHealth>=_targetStat.GetMaxHealth()*0.9f;
        if(skillManager.overwhelmingPowerSkill.overwhelmingPowerUnlocked&&canApplyOverwhelmingPower){
            totalDamage+=Mathf.RoundToInt(GetStrength()*0.2f);
        }
        if(skillManager.frenziedFurySkill.frenziedFuryUnlocked&&currentHealth<=GetMaxHealth()*0.3f){
            totalDamage+=Mathf.RoundToInt(GetStrength()*0.1f);
        }
        if(CanCrit()){
            totalDamage*=2;
        }
        totalDamage=CheckTargetArmor(_targetStat,totalDamage);
        if(player.currentWeapon==CurrentWeapon.Bow){
            if(skillManager.woundOfTheArrowSkill.woundOfTheArrowUnlocked&& Random.Range(0,100)<=20){
                _targetStat.SetUpBleedEffect(Mathf.RoundToInt(_targetStat.GetMaxHealth()*0.05f),0,15,15);
            }
        }
        IncreaseHealBy(Mathf.RoundToInt(totalDamage*((float)Lifesteal.GetValue()*(float)0.01)));
        _targetStat.TakeDamage(totalDamage);
    }
    public override void DoMagicalDamage(CharaterStats _targetStat, int _damageOfSkill, float _percentExtraDamageOfSkill = 0)
    {
        if(TargetCanAvoidAttack(_targetStat)){
            return;
        }

        int currentTargetHealth=_targetStat.currentHealth;
        float percentDamageOfSkill=Mathf.Clamp01(_percentExtraDamageOfSkill);
        int totalDamage=_damageOfSkill;
        if(_percentExtraDamageOfSkill!=0){
            totalDamage+=Mathf.RoundToInt(GetMagicPower()*percentDamageOfSkill);
        }
        
        if(skillManager.suddenDeathSkill.suddenDeathUnlocked){
            if(currentTargetHealth<=currentTargetHealth*0.15f && Random.Range(0,100)<=20){
                _targetStat.TakeDamage(999999);
                return;
            }
        }
        
        if(skillManager.perfectStateSkill.perfectStateUnlocked&&currentHealth>=GetMaxHealth()*0.9f){
            totalDamage+=Mathf.RoundToInt(GetMagicPower()*0.1f);
        }
        bool canApplyOverwhelmingPower= currentTargetHealth>=_targetStat.GetMaxHealth()*0.9f;
        if(skillManager.overwhelmingPowerSkill.overwhelmingPowerUnlocked&&canApplyOverwhelmingPower){
            totalDamage+=Mathf.RoundToInt(GetMagicPower()*0.2f);
        }
        if(skillManager.frenziedFurySkill.frenziedFuryUnlocked&&currentHealth<=GetMaxHealth()*0.3f){
            totalDamage+=Mathf.RoundToInt(GetMagicPower()*0.1f);
        }
        if(CanCrit()){
            totalDamage*=2;
        }
        totalDamage=CheckTargetMagicresistance(_targetStat,totalDamage);
        _targetStat.TakeDamage(totalDamage);
    }

    public override void TakeDamage(int _damge)
    {
        base.TakeDamage(_damge);
        if(isImmortal) return;
        AudioManager.instance.PlaySFX(20);
        StartCoroutine(InvisableByTime(2f));
        playerFX.TakeDamageFX(2f);
        GameManager.instance.PauseGameForSeconds(0.2f);

    }

    public override void IncreaseHealBy(int _heal)
    {
        base.IncreaseHealBy(_heal);
        if(currentHealth>GetMaxHealth()*0.3f){
            skillManager.frenziedFurySkill.DeleteStatus();
            skillManager.resoluteFortressSkill.DeleteStatus();
        }
        UI.instance.ingameUI.UpdateHealth();
    }

    public override void DecreaseHealthBy(int _damage){
        Debug.Log("Decrease Health before"+_damage);
        if(skillManager.challengingFateSkill.challengingFateUnlocked && wasImmortal==false 
            && currentHealth<=GetMaxHealth()*0.20f){
                StartCoroutine(InvisableByTime(10f));
                playerFX.TakeDamageFX(10f);
                UI.instance.ingameUI.CreateStatus(skillManager.challengingFateSkill.iconStatus,"Challenging Fate",skillManager.challengingFateSkill.statusKey,10f);
                wasImmortal=true;
        }
        
        if(skillManager.resoluteFortressSkill.resoluteFortressUnlocked&&currentHealth<=GetMaxHealth()*0.3f){
            _damage=Mathf.RoundToInt(_damage*0.85f);
        }
        currentHealth-=_damage;
        Debug.Log("Decrease Health after"+_damage);
        
        if(currentHealth<=GetMaxHealth()*0.3f){
            if(skillManager.frenziedFurySkill.frenziedFuryUnlocked){
                skillManager.frenziedFurySkill.ActiveStatus();
            }
            if(skillManager.resoluteFortressSkill.resoluteFortressUnlocked){
                skillManager.resoluteFortressSkill.ActiveStatus();
            }
        }
        
        Debug.Log("Take Damage "+_damage);
        


        UI.instance.ingameUI.UpdateHealth();
    }
    protected override void Die()
    {
        base.Die();
        player.Die();
        ResetImmortal();

        
    }

    public void IncreaseManaBy(int _mana){
        currentMana+=_mana;
        currentMana=Mathf.Clamp(currentMana,0,GetMaxMana());

        UI.instance.ingameUI.UpdateMana();
        onManaUIChanged?.Invoke();
    }

    public void DecreaseManathBy(int _mana){
        currentMana-=_mana;
        currentMana=Mathf.Clamp(currentMana,0,GetMaxMana());
        UI.instance.ingameUI.UpdateMana();
        onManaUIChanged?.Invoke();
    }


    public override void OnEvasion()
    {

    }
   
    
}
