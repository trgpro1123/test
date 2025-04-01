using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    private void Awake() {
        
        if(instance!=null&&this.gameObject!=null){
            Destroy(this.gameObject);
        }
        else{
            instance=this;
        }
        if(!gameObject.transform.parent){
            DontDestroyOnLoad(gameObject);
        }

        slashSkill=GetComponent<Slash_Skill>();
        swordAuraSkill=GetComponent<SwordAura_Skill>();
        heavyCutterSkill=GetComponent<HeavyCutter_Skill>();
        swordDashSkill=GetComponent<SwiftDash_Skill>();
        bladeThrowSkill=GetComponent<BladeThrow_Skill>();
        bladeCycloneSkill=GetComponent<BladeCyclone_Skill>();
        chaosSlashesSkill=GetComponent<ChaosSlashes_Skill>();
        dancingBladesSkill=GetComponent<DancingBlades_Skill>();
        blinkBladeSkill=GetComponent<BlinkBlade_Skill>();
        thousandCutsSkill=GetComponent<ThousandCuts_Skill>();

        powerBoostSkill=GetComponent<PowerBoost_Skill>();
        criticalRushSkill=GetComponent<CriticalRush_Skill>();
        overwhelmingPowerSkill=GetComponent<OverwhelmingPower_Skill>();
        frenziedFurySkill=GetComponent<FrenziedFury_Skill>();
        passThroughArmorSkill=GetComponent<PassThroughArmor_Skill>();
        suddenDeathSkill=GetComponent<SuddenDeath_Skill>();



        shotSkill=GetComponent<Shot_Skill>();
        manaInfusedShotSkill=GetComponent<ManaInfusedShot_Skill>();
        explosiveArrowSkill=GetComponent<ExplosiveArrow_Skill>();
        piercingShotSkill=GetComponent<PiercingShot_Skill>();
        cometDropSkill=GetComponent<CometDrop_Skill>();
        flurryOfArrowsSkill=GetComponent<FlurryOfArrows_Skill>();
        volleyOfArrowsSkill=GetComponent<VolleyOfArrows_Skill>();
        doppelgängerSkill=GetComponent<Doppelgänger_Skill>();
        starbreakerSkill=GetComponent<Starbreaker_Skill>();

        breakthroughShotSkill=GetComponent<BreakthroughShot_Skill>();
        perfectStateSkill=GetComponent<PerfectState_Skill>();
        quickdrawSkill=GetComponent<Quickdraw_Skill>();
        swiftReflexesSkill=GetComponent<SwiftReflexes_Skill>();
        windwalkerSkill=GetComponent<Windwalker_Skill>();
        woundOfTheArrowSkill=GetComponent<WoundOfTheArrow_Skill>();






        punchSkill=GetComponent<Punch_Skill>();
        empoweredFistSkill=GetComponent<EmpoweredFist_Skill>();
        obliteratingPunchSkill=GetComponent<ObliteratingPunch_Skill>();
        savageBarrageSkill=GetComponent<SavageBarrage_Skill>();
        energyPalmSkill=GetComponent<EnergyPalm_Skill>();
        forcewaveSkill=GetComponent<Forcewave_Skill>();
        rampageWarcrySkill=GetComponent<RampageWarcry_Skill>();
        earthquakeJumpSkill=GetComponent<EarthquakeJump_Skill>();
        seismicShockwaveSkill=GetComponent<SeismicShockwave_Skill>();

        challengingFateSkill=GetComponent<ChallengingFate_Skill>();
        crushingImpactSkill=GetComponent<CrushingImpact_Skill>();
        dizzyingStrikeSkill=GetComponent<DizzyingStrike_Skill>();
        ironcladBodySkill=GetComponent<IroncladBody_Skill>();
        resoluteFortressSkill=GetComponent<ResoluteFortress_Skill>();
        stalwartVitalitySkill=GetComponent<StalwartVitality_Skill>();



        bloodRitualSkill=GetComponent<BloodRitual_Skill>();
        chronoControlSkill=GetComponent<ChronoControl_Skill>();
        energyAbsorptioSkill=GetComponent<EnergyAbsorptio_Skill>();
        energyBombSkill=GetComponent<EnergyBomb_Skill>();
        ionCannonSkill=GetComponent<IonCannon_Skill>();
        lifeforceRestorationSkill=GetComponent<LifeforceRestoration_Skill>();
        manasGraceSkill=GetComponent<ManasGrace_Skill>();
        ultimateExplosionSkill=GetComponent<UltimateExplosion_Skill>();
        wintersDomainSkill=GetComponent<WintersDomain_Skill>();

        cooldownReductionSkill=GetComponent<CooldownReduction_Skill>();
        energyFlowSkill=GetComponent<EnergyFlow_Skill>();
        lifeReapSkill=GetComponent<LifeReap_Skill>();
        luckyyySkill=GetComponent<Luckyyy_Skill>();
        manaFreeSkill=GetComponent<ManaFree_Skill>();
        manaReservoirSkill=GetComponent<ManaReservoir_Skill>();
        manaResistanceSkill=GetComponent<ManaResistance_Skill>();
        masterfulTechniqueSkill=GetComponent<MasterfulTechnique_Skill>();
        
        
        listSkills=new SkillInfo[6];
        equimentSkillSlot=skillEquipmentSlot.GetComponentsInChildren<UI_EquipmentSkillTreeSlot>();
    }
    [Header("Sword")]
    public Slash_Skill slashSkill;
    public SwordAura_Skill swordAuraSkill;
    public HeavyCutter_Skill heavyCutterSkill;
    public SwiftDash_Skill swordDashSkill;
    public BladeThrow_Skill bladeThrowSkill;
    public BladeCyclone_Skill bladeCycloneSkill;
    public ChaosSlashes_Skill chaosSlashesSkill;
    public DancingBlades_Skill dancingBladesSkill;
    public BlinkBlade_Skill blinkBladeSkill;
    public ThousandCuts_Skill thousandCutsSkill;

    public PowerBoost_Skill powerBoostSkill;
    public CriticalRush_Skill criticalRushSkill;
    public OverwhelmingPower_Skill overwhelmingPowerSkill;
    public FrenziedFury_Skill frenziedFurySkill;
    public PassThroughArmor_Skill passThroughArmorSkill;
    public SuddenDeath_Skill suddenDeathSkill;


    [Header("Bow")]
    public Shot_Skill shotSkill;
    public ExplosiveArrow_Skill explosiveArrowSkill;
    public ManaInfusedShot_Skill manaInfusedShotSkill;
    public PiercingShot_Skill piercingShotSkill;
    public CometDrop_Skill cometDropSkill;
    public FlurryOfArrows_Skill flurryOfArrowsSkill;
    public VolleyOfArrows_Skill volleyOfArrowsSkill;
    public Doppelgänger_Skill doppelgängerSkill;
    public Starbreaker_Skill starbreakerSkill;

    public BreakthroughShot_Skill breakthroughShotSkill;
    public PerfectState_Skill perfectStateSkill;
    public Quickdraw_Skill quickdrawSkill;
    public SwiftReflexes_Skill swiftReflexesSkill;
    public Windwalker_Skill windwalkerSkill;
    public WoundOfTheArrow_Skill woundOfTheArrowSkill;






    [Header("Gauntlet")]
    public Punch_Skill punchSkill;
    public EmpoweredFist_Skill empoweredFistSkill;
    public ObliteratingPunch_Skill obliteratingPunchSkill;
    public SavageBarrage_Skill savageBarrageSkill;
    public EnergyPalm_Skill energyPalmSkill;
    public Forcewave_Skill forcewaveSkill;
    public RampageWarcry_Skill rampageWarcrySkill;
    public EarthquakeJump_Skill earthquakeJumpSkill;
    public SeismicShockwave_Skill seismicShockwaveSkill;

    public ChallengingFate_Skill challengingFateSkill;
    public CrushingImpact_Skill crushingImpactSkill;
    public DizzyingStrike_Skill dizzyingStrikeSkill;
    public IroncladBody_Skill ironcladBodySkill;
    public ResoluteFortress_Skill resoluteFortressSkill;
    public StalwartVitality_Skill stalwartVitalitySkill;




    [Header("Magic")]
    public BloodRitual_Skill bloodRitualSkill;
    public ChronoControl_Skill chronoControlSkill;
    public EnergyAbsorptio_Skill energyAbsorptioSkill;
    public EnergyBomb_Skill energyBombSkill;
    public IonCannon_Skill ionCannonSkill;
    public LifeforceRestoration_Skill lifeforceRestorationSkill;
    public ManasGrace_Skill manasGraceSkill;
    public UltimateExplosion_Skill ultimateExplosionSkill;
    public WintersDomain_Skill wintersDomainSkill;

    public CooldownReduction_Skill cooldownReductionSkill;
    public EnergyFlow_Skill energyFlowSkill;
    public LifeReap_Skill lifeReapSkill;
    public Luckyyy_Skill luckyyySkill;
    public ManaFree_Skill manaFreeSkill;
    public ManaReservoir_Skill manaReservoirSkill;
    public ManaResistance_Skill manaResistanceSkill;
    public MasterfulTechnique_Skill masterfulTechniqueSkill;

    





    public SkillInfo[] listSkills;

    [Header("Skill UI")]
    [SerializeField] private Transform skillEquipmentSlot;
    private UI_EquipmentSkillTreeSlot[] equimentSkillSlot;



    private void Start() {
        
        Invoke("UpdateSlotSkill",1f);

    }
    
    public void AddSkill(SkillInfo _skillInfo){
        if(_skillInfo == null || _skillInfo.skillData == null)
        {
            Debug.LogWarning("Trying to add null skill");
            return;
        }
        foreach (var item in listSkills)
        {
            if(item?.skillData == _skillInfo.skillData){
                    Debug.Log("Da co Skill khong the them");
                    return;
            }
            
        }
        
        if(CanAddSkill()==true){
            for(int i=0;i<listSkills.Length;i++){
                if(listSkills[i]?.skillData==null){
                    listSkills[i]=_skillInfo;
                    break;
                }
            }
            UpdateSlotSkill();
            Debug.Log("Add skill");
        }else {
            Debug.Log("Can't add skill");
        }
    }
    public void RemoveSkill(SkillInfo _skillInfo){

        for(int i=0;i<listSkills.Length;i++){
            if(listSkills[i]!=null&&listSkills[i].skillData!=null){
                if(listSkills[i].skillData==_skillInfo.skillData){
                    listSkills[i]=null;
                    break;
                }
            }
        }
        UpdateSlotSkill();
    }
    public bool CanAddSkill(){
        int count=0;
        foreach (var item in listSkills)
        {
            if(item?.skillData) count++;
        }
        if(count>=equimentSkillSlot.Length) return false;
        return true;
    }
     public void UpdateSlotSkill(){
        for (int i = 0; i < equimentSkillSlot.Length; i++)
        {
            equimentSkillSlot[i].ClearUpSlot();
        }
        for(int i=0;i<listSkills.Length;i++){
            if(listSkills[i]?.skillData)
                equimentSkillSlot[i].UpdateSlot(listSkills[i]);
        }
    }
    public SkillInfo[] GetArraySkillInfo(){
        return listSkills;
    }
    




}
