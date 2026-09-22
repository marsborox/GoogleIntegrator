using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterSpellcasting characterSpellcasting;
    //MainInfo
    public Abilities abilities;
    public CharacterCombat combat;
    [Header("Basic Info")]
    public string characterName;
    public CharacterSize characterSize;
    public int proficiencyBonus; //based on level // might not be in monster hero only
    public int passivePerception;//(perception)//more bigger calculations //10+pereception skill(WisMod)
    

    public void Awake()
    {   //all is testing 
        abilities.AddAbilitiesToList();
        abilities.CalcAbilityModifiers();
    }

}
