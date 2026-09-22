using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{

    public Skill acrobatics = new Skill(AbilityType.DEX, SkillType.ACROBATICS);
    public Skill animalHandling = new Skill(AbilityType.WIS,SkillType.ANIMAL_HANDLING);
    public Skill arcana = new Skill(AbilityType.INT,SkillType.ARCANA);
    public Skill athletics = new Skill(AbilityType.STR,SkillType.ATHLETICS);
    public Skill deception = new Skill(AbilityType.CHA,SkillType.DECEPTION);
    public Skill history = new Skill(AbilityType.INT,SkillType.HISTORY);
    public Skill insight = new Skill(AbilityType.WIS,SkillType.INSIGHT);
    public Skill intimidation = new Skill(AbilityType.CHA,SkillType.INTIMIDATION);
    public Skill investigation = new Skill(AbilityType.INT,SkillType.INVESTIGATION);
    public Skill medicine = new Skill(AbilityType.WIS,SkillType.MEDICINE);
    public Skill nature = new Skill(AbilityType.INT,SkillType.NATURE);
    public Skill perception = new Skill(AbilityType.WIS,SkillType.PERCEPTION);
    public Skill performance = new Skill(AbilityType.CHA,SkillType.PERFORMANCE);
    public Skill persuation = new Skill(AbilityType.CHA,SkillType.PERSUASION);
    public Skill religion = new Skill(AbilityType.INT,SkillType.RELIGION);
    public Skill sleightOfHand = new Skill(AbilityType.DEX,SkillType.SLEIGHT_OF_HAND);
    public Skill stealth = new Skill(AbilityType.DEX,SkillType.STEALTH);
    public Skill survival = new Skill(AbilityType.WIS,SkillType.SURVIVAL);

    public List<Skill> skillList = new List<Skill>();

    public void AddSkillsToList()
    {
        skillList.Add(acrobatics);
        skillList.Add(animalHandling);
        skillList.Add(arcana);
        skillList.Add(athletics);
        skillList.Add(deception);
        skillList.Add(history);
        skillList.Add(insight);
        skillList.Add(intimidation);
        skillList.Add(investigation);
        skillList.Add(medicine);
        skillList.Add(nature);
        skillList.Add(perception);
        skillList.Add(performance);
        skillList.Add(persuation);
        skillList.Add(religion);
        skillList.Add(sleightOfHand);
        skillList.Add(stealth);
        skillList.Add(survival);
    }
    public void SetAbilityProficiencies(List<SkillType> skillTypeList)
    {
        foreach(SkillType skillType in skillTypeList)
        {
            foreach(Skill skill in skillList)
            {
                if(skillType == skill.skillType)
                {
                    skill.isProficient=true;
                    break;
                }
            }
        }
    }
    public void SaveSkills(ref HeroSavedata hero)
    {
        foreach(Skill skill in skillList)
        {
            SkillSaveData skillSave = skill.SaveSkill();
            hero.skills.Add(skillSave);
        }
    }
    public void LoadSkills(ref HeroSavedata hero)
    {
        foreach(SkillSaveData saveData in hero.skills)
        {
            foreach(Skill ability in skillList)
            {
                if(saveData.skillType == ability.skillType)
                {
                    ability.LoadSkill(saveData);
                    break;
                }
            }
        }
    }

}
