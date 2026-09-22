using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Race", menuName = "Scriptable Objects/Race")]
public class Race_SO : ScriptableObject
{
    public string name;
    public CharacterSize characterSize;
    public int speed;
    public List<AbilityType> abilityProficiencies = new List <AbilityType>();
    [Header("AbilityScore Increase per level")]
    public List<AbilityType> plusOneAbilityList = new List <AbilityType>();
    public List<AbilityType> plusTwoAbilityList = new List <AbilityType>();
    
}
