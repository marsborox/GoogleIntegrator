using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroClass", menuName = "Scriptable Objects/HeroClass")]
public class HeroClass_SO : ScriptableObject
{
    public string className;
    public DiceType hitDice;
    public int skillProficiencyCount;

    public bool isSpellcastingClass;
    public AbilityType spellcastingAbility = AbilityType.NONE;

    public List<SkillType> skillProficiencyList = new List<SkillType>();
    
}
