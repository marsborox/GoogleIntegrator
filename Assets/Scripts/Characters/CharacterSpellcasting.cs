using Unity.VisualScripting;
using UnityEngine;

public class CharacterSpellcasting : MonoBehaviour
{
    public Character character;
    
    public Ability spellcastingAbility=null;
    public int spellSaveDC=0;
    public int spellAttackBonus=0;

    //
    //spellslots
    //spell list prepared
    //spell list known

    //spell attackRoll - if required by spell to make attack roll
    //should be spellCastingability modifier +d20 generallz attack roll add proficiency
    //spell save dc 8+ proficiency bonus + spellcasting mobility
    public void CalcSpellcastingAbilities(HeroClass_SO heroClassSO)
    {//hero
        
        if(heroClassSO.spellcastingAbility == AbilityType.NONE)
        {return;}
        spellcastingAbility = character.abilities.ReturnCorrectAbility(heroClassSO.spellcastingAbility);
        spellSaveDC = 8 + spellcastingAbility.modifier + character.proficiencyBonus;
        spellAttackBonus = spellcastingAbility.modifier + character.proficiencyBonus;
    }
    public SpellcastingSaveData SaveSpellcasting()
    {
        SpellcastingSaveData saveData = new SpellcastingSaveData();
        if(spellcastingAbility==null)
        {
            saveData.spellcastingAbility=AbilityType.NONE;
        }
        else
        {
            saveData.spellcastingAbility = spellcastingAbility.abilityType;
            saveData.spellSaveDC = spellSaveDC;
            saveData.spellAttackBonus = spellAttackBonus;
            //spells known, prepared, spellSlots
        }

        return saveData;
    }
    public void LoadSpellcasting(SpellcastingSaveData saveData)
    {
        if(saveData.spellcastingAbility==AbilityType.NONE)
        {return;}

        saveData.spellcastingAbility = spellcastingAbility.abilityType;
        saveData.spellSaveDC = spellSaveDC;
        saveData.spellAttackBonus = spellAttackBonus;
        //spells known, prepared, spellSlots
       
    }
}
