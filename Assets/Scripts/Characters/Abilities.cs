using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public Ability strength = new Ability(AbilityType.STR);
    public Ability dexterity = new Ability(AbilityType.DEX);
    public Ability constitution = new Ability(AbilityType.CON);
    public Ability intelligence = new Ability(AbilityType.INT);
    public Ability wisdom = new Ability(AbilityType.WIS);
    public Ability charisma = new Ability(AbilityType.CHA);
    public List<Ability> abilityList = new List<Ability>();

    public void AddAbilitiesToList()
    {
        abilityList.Add(strength);
        abilityList.Add(dexterity);
        abilityList.Add(constitution);
        abilityList.Add(intelligence);
        abilityList.Add(wisdom);
        abilityList.Add(charisma);
    }
    public void AddOneToAbilityScore(List<AbilityType> abilityTypeList)
    {//list on SO w proficiencies
        foreach(AbilityType abilityType in abilityTypeList)
        {
            foreach(Ability ability in abilityList)
            {
                if(abilityType == ability.abilityType)
                {
                    ability.score ++;
                }
            }
        }
    }
    public void AddTwoToAbilityScore(List<AbilityType> abilityTypeList)
    {//list on SO w proficiencies
        foreach(AbilityType abilityType in abilityTypeList)
        {
            foreach(Ability ability in abilityList)
            {
                if(abilityType == ability.abilityType)
                {
                    ability.score +=2;
                }
            }
        }
    }
    public void SetAbilityProficiencies(List<AbilityType> abilityTypeList)
    {
        foreach (AbilityType abilityType in abilityTypeList)
        {
            foreach(Ability ability in abilityList)
            {
                if(abilityType == ability.abilityType)
                {
                    ability.isProficient = true;
                }
            }
        }
    }
    public void CalcAbilityModifiers()
    {
        foreach(Ability ability in abilityList)
        {
            ability.CalcModifier();
        }
    }
    public Ability ReturnCorrectAbility(AbilityType abilityType)
    {
        Ability returnAbility = null;

        foreach(Ability ability in abilityList)
        {
            if(abilityType == ability.abilityType)
            returnAbility = ability;

        }
        return returnAbility;
    }
    public void SaveAbilities(ref HeroSavedata hero)
    {//move to ability

        foreach(Ability ability in abilityList)
        {
            AbilitySaveData abilitySave = ability.SaveAbility();
            hero.abilityScores.Add(abilitySave);
        }

    }
    public void LoadAbilities(ref HeroSavedata hero)
    {
        foreach(AbilitySaveData saveData in hero.abilityScores)
        {
            foreach(Ability ability in abilityList)
            {
                if(saveData.abilityType == ability.abilityType)
                {
                    ability.LoadAbility(saveData);
                    break;
                }
            }
        }
        //mabye calc modifiers
    }
}
