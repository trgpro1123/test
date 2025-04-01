using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharaterStats
{
    protected Enemy enemy;
    protected ItemDrop itemDrop;
    [Header("Level Detail")]
    [SerializeField] protected int level;

    [Range(0,1)] 
    [SerializeField] protected float percantagedModifiers;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        itemDrop=GetComponent<ItemDrop>();
        ApplyLevelModifiers();

    }

    protected void ApplyLevelModifiers()
    {
        Modify(health);
        Modify(strength);
        Modify(armor);
        Modify(agility);
        Modify(moveSpeed);
        Modify(Lifesteal);
        Modify(critChance);
        Modify(magicPower);
        Modify(magicRegeneration);
        Modify(magicResistance);
        Modify(luck);
        Modify(coolDown);
    }

    public override void TakeDamage(int _damge)
    {
        if(enemy!=null)
            IncreaseAttackDetectFor(0.5f);
        base.TakeDamage(_damge);

    }
    public void Modify(Stat _stat){
        for (int i = 0; i < level; i++)
        {
            float modifier=_stat.GetValue()*percantagedModifiers;
            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }
    public override void ApplySlowEffect(float _slowEffectDuration, int _slowEffectAmount)
    {
        entityFX.SlowEffectForTime(_slowEffectDuration);
        StartCoroutine(enemy.SlowDownCoroutine(_slowEffectDuration, _slowEffectAmount));
    }

    protected override void Die()
    {
        base.Die();
        if(skillManager.lifeReapSkill.lifeReapUnlocked)
        {;
            PlayerManager.instance.player.charaterStats.IncreaseHealBy(5);
        }
        StartCoroutine(itemDrop.GenerateDropItem());
        enemy.Die();
        
    }
    public void IncreaseAttackDetectFor(float _time)
    {
        StartCoroutine(IncreaseAttackDetectCoroutine(_time));
    }
    public IEnumerator IncreaseAttackDetectCoroutine(float _time)
    {
        enemy.distanceDetect=100;
        yield return new WaitForSeconds(_time);
        enemy.distanceDetect=enemy.defauletDistanceDetect;
        
    }
}
