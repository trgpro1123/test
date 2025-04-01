

using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

[Serializable]
public class SkillInfo 
{
    public SkillData skillData;

    public Skill skill;
    public System.Action useSkill;
    public SkillInfo (SkillData _data,Skill _skill){
        skillData=_data;
        skill=_skill;
        AddSkill();
    }
    public void AddSkill(){
        ResetSkill();
        useSkill+=skill.ActiveSkill;
    }
    public void ActiveSkill(){
        if(useSkill==null) Debug.Log("Skill is null");
        useSkill?.Invoke();
    }
    public void ResetSkill(){
        useSkill=null;
    }

    public override bool Equals(object obj)
    {   // c1 nâng cao
        // return obj is SkillInfo info &&
        //        EqualityComparer<SkillData>.Default.Equals(skillData, info.skillData) &&
        //        EqualityComparer<Skill>.Default.Equals(skill, info.skill);
        // c2 cơ bản
        if (obj is SkillInfo other)
        {
            return skillData == other.skillData && skill == other.skill;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(skillData, skill);
    }
}
