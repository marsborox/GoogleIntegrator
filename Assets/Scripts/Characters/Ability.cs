using System.Runtime.CompilerServices;
using UnityEngine;

//public enum AbilityType{STR,DEX,CON,INT,WIS,CHA}
[System.Serializable]
public class Ability
{
    public AbilityType abilityType;
    public int score;
    public int modifier;
    public int savingThrow;
    public bool isProficient;
    
    public void CalcModifier()
    {
        //must test if works properly
        modifier = (score-10)/2;//check if rounds down automaticly
    }
    public void CalcSavingThrow(int proficiencyBonus)
    {
        
        savingThrow = modifier + (isProficient? proficiencyBonus:0);
    }
    public Ability(AbilityType inputAbilityType)
    {
        abilityType = inputAbilityType;
    }
    public AbilitySaveData SaveAbility()
    {
        AbilitySaveData saveData = new AbilitySaveData();
        saveData.abilityType = abilityType;
        saveData.abilityScore = score;
        saveData.isAbilityProficient = isProficient;
        return saveData;
    }
    public void LoadAbility(AbilitySaveData saveData)
    {
        abilityType = saveData.abilityType;
        score = saveData.abilityScore;
        isProficient = saveData.isAbilityProficient;
    }
}

