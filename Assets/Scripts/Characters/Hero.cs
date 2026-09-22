using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : Character
{
    [Header("Hero proprietary")]
    public Skills skills;
    public HeroClass_SO heroClass;
    public Race_SO heroRace;
    public bool isInspiration;
    public int level;
    
    public string backGround;
    public string alignment;
    public int experience;
    [Header("Proficiencies")]
    //if profiient, add prof bonus to attack roll
    public List<WeaponGroup> weaponGroupProficiencyList = new List<WeaponGroup>();
    public List<WeaponType> weaponProficiencyList = new List<WeaponType>();
    //if you lack proficiency with, you have disadvantage on
    //any ability check, saving throw, or attack roll that
    //involves Strength or Dexterity, and you can’t cast
    //spells.
    public List<ArmorGroup> armorGroupProficiencyList = new List<ArmorGroup>();
    public List<ArmorType>armorTypeProficiencyList = new List<ArmorType>();

   
    void Awake()
    {
        SetObjectProperties();
        SetRaceAndClass();
        //migrate
        combat.CalcSpeed();
        //passivePerception = 99;//jsut to test iv val is changed remove later
        //Debug.Log("passive Wisdom is "+passiveWisdom);
        abilities.CalcAbilityModifiers();
        //Debug.Log("passive Wisdom is "+passiveWisdom);
        CalcSavingThrows();
        //Debug.Log("passive Wisdom is "+passiveWisdom);
        CalcSkillThrows();
        //Debug.Log("passive Wisdom is "+passiveWisdom);
        Calc3PassiveStats();

        //SetWeaponProficiencies();
        //migrate
        combat.CalcArmorClass();
        combat.CalcCombatStats();
        combat.GenerateAttacks();
        CalcSpellcasting();
    }
    void Start()
    {
        UI_Controller.instance.RefReshHeroUIEvent();
        UI_Controller.instance.RefreshHeroProficienciesUIEvent();
    }

    public void SetObjectProperties()
    {
        abilities.AddAbilitiesToList();

        skills.AddSkillsToList();
    }
    public void CalcSavingThrows()
    {
        foreach(Ability ability in abilities.abilityList)
        ability.CalcSavingThrow(proficiencyBonus);
    }
    public void CalcSkillThrows()
    {
        foreach(Skill skill in skills.skillList)
        {
            skill.CalcSkill(abilities.abilityList,proficiencyBonus);
        }
    }
    public void Calc3PassiveStats()
    {
        CalcProficiencyBonus();
        CalcPassivePerception();
    }
    public void CalcSpellcasting()
    {
        characterSpellcasting.CalcSpellcastingAbilities(heroClass);
    }
    public void CalcPassivePerception()
    {
        passivePerception = 10 + abilities.wisdom.modifier;
        if(skills.perception.isProficient){passivePerception+=proficiencyBonus;}
    }
    public void CalcProficiencyBonus()
    {//do switch
        if(level <5)
        {
            proficiencyBonus = 2;
        }
        else if(level >4 && level <9)
        {
            proficiencyBonus = 3;
        }
        else if(level >8 && level <13)
        {
            proficiencyBonus = 4;
        }
        else if(level >12 && level <17)
        {
            proficiencyBonus = 5;
        }
        else if(level >16 && level <21)
        {
            proficiencyBonus = 6;
        }
    }
    private void SetWeaponProficiencies()
    {
        if(weaponGroupProficiencyList.Count == 0){return;}

        foreach(WeaponGroup weaponGroup in weaponGroupProficiencyList)
        {
            List<Weapon_SO> weapon_SOs = EquipmentCollection.instance.ReturnCorrectWeaponList(weaponGroup);
            foreach(Weapon_SO weapon_SO in weapon_SOs)
            {
                weaponProficiencyList.Add(weapon_SO.weaponType);
            }
        }
    }

    private void SetRaceAndClass()
    {
        //----------------------run method in RaceSO,
        //abiulity proficiencies
        //skill proficiencies
        skills.SetAbilityProficiencies(heroClass.skillProficiencyList);
        characterSize=heroRace.characterSize;
        abilities.AddOneToAbilityScore(heroRace.plusOneAbilityList);
        abilities.AddTwoToAbilityScore(heroRace.plusTwoAbilityList);
        abilities.SetAbilityProficiencies(heroRace.abilityProficiencies);
        combat.SetCombatStats(heroClass, heroRace);
        //migrate
        /*
        hitDice = DiceCollection.instance.ReturnDicePerType(heroClass.hitDice);
        speed = heroRace.speed;*/

    }

    public HeroSavedata SaveHero()
    {
        HeroSavedata saveData = new HeroSavedata();
        saveData.heroName = name;
        saveData.heroLevel = level;
        saveData.heroExperience=experience ;
        saveData.heroClass = heroClass.name;
        saveData.heroRace = heroRace.name;
        //save abilities
        abilities.SaveAbilities(ref saveData);
        skills.SaveSkills(ref saveData);
        saveData.combatSaveData = combat.SaveCombat();

        saveData.weaponGroupProficiencyList = weaponGroupProficiencyList;
        saveData.weaponProficiencyList = weaponProficiencyList;
        saveData.armorGroupProficiencyList = armorGroupProficiencyList;
        saveData.armorTypeProficiencyList = armorTypeProficiencyList;

        /*foreach(Weapon weapon in combat.weaponList)
        {
            HeroSavedata.
        }*/
        saveData.spellcastingSaveData=characterSpellcasting.SaveSpellcasting();

        return saveData;
    }
    public void LoadHero(HeroSavedata saveData)
    {
        name = saveData.heroName;
        level = saveData.heroLevel;
        experience = saveData.heroExperience;

        //set race from string find SO?
        heroRace = RaceAndClassCollection.instance.ReturnHeroRaceSO_FromName(saveData.heroRace);
        heroClass = RaceAndClassCollection.instance.ReturnHeroClassSO_FromName(saveData.heroClass);

        abilities.LoadAbilities(ref saveData);
        skills.LoadSkills(ref saveData);
        combat.LoadCombat(saveData.combatSaveData,this);

        weaponGroupProficiencyList = saveData.weaponGroupProficiencyList;
        weaponProficiencyList = saveData.weaponProficiencyList;
        armorGroupProficiencyList = saveData.armorGroupProficiencyList;
        armorTypeProficiencyList = saveData.armorTypeProficiencyList;
        characterSpellcasting.LoadSpellcasting(saveData.spellcastingSaveData);
    }
}