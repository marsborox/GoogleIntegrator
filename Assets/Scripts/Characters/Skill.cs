using System.Collections.Generic;
using System.IO;
using MessagePack;
using UnityEngine;
[System.Serializable]
//[MessagePackObject]
public class Skill
{
    public AbilityType skillAbilityType;
    public SkillType skillType;
    public bool isProficient;
    public int skillValue;


    public Skill(AbilityType inputAbilityType, SkillType inputSkillType)
    {
        skillAbilityType = inputAbilityType;
        skillType = inputSkillType;
        isProficient = false;
    }
    public void CalcSkill(List<Ability> abilities,int heroProficiencyBonus)
    {
        int abilityModifier=0;
        foreach (Ability ability in abilities)
        {
            if(skillAbilityType==ability.abilityType)
            {
                //Debug.Log("Ability name type and modifier: "+ability.abilityType.ToString()+" "+ability.modifier);
                abilityModifier = ability.modifier;
                break;
            }
        }
        int proficiencyBonus = isProficient? heroProficiencyBonus:0;

        skillValue = abilityModifier+proficiencyBonus;
    }
    public SkillSaveData SaveSkill()
    {
        SkillSaveData saveData = new SkillSaveData();

        saveData.skillAbilityType = skillAbilityType;
        saveData.skillType = skillType;
        saveData.isProficient = isProficient;
        saveData.skillValue = skillValue;
        return saveData;
    }
    public void LoadSkill(SkillSaveData saveData)
    {
        skillAbilityType = saveData.skillAbilityType ;
        skillType = saveData.skillType;
        isProficient = saveData.isProficient;
        skillValue = saveData.skillValue;
    }
}
