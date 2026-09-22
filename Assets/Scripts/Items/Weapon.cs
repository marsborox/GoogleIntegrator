using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public Weapon_SO weaponTemplate;
    public Character character;
    public string weaponName{get{return weaponTemplate.weaponName;}}
    public WeaponGroup weaponGroup{get{return weaponTemplate.weaponGroup;}}
    public WeaponType weaponType{get{return weaponTemplate.weaponType;}}
    //public is fixed dmg or dice
    //damage dice number
    public DiceType damageDice{get{return weaponTemplate.damage;}}
    public DamageType damageType{get{return weaponTemplate.damageType;}}
    public bool isVersatile{get{return weaponTemplate.isVersatile;}}
    public DiceType versatile2H_damage{get{return weaponTemplate.versatile2H_damage;}}
    public List<WeaponProperty> weaponProperties {get{return weaponTemplate.weaponProperties;}}
    public bool isRange{get{return weaponTemplate.isRange;}}
    public int rangeMin{get{return weaponTemplate.rangeMin;}}
    public int rangeMax{get{return weaponTemplate.rangeMax;}}
    
    
    
    //ToHit modifier we add,modifier [ STR melee, DEX ranged,] finese and thrown specific+ add proficiency if possible
    //DmgDice
    //DmgBonus - we add modifier of same stat we used in to hit

    

    /*public int toHitBonus;//
    public int damageBonus;*/

    public Attack ReturnAttack()
    {
        Attack attack = new Attack();
        attack.name = weaponName;
        Ability ability = ReturnCorrectModifier();

        //Debug.Log(nameof(ability.abilityType).ToString());
        //Debug.Log("returning attack w ability "+ability.abilityType.ToString());

        attack.toHitBonus = CalcHitBonus(ability);
        attack.damageDice = damageDice;
        attack.damageBonus = CalcDamageBonus(ability);
        attack.damageType = damageType;
        return attack;
    }

    Ability ReturnCorrectModifier()
    {
    Abilities abilities = character.abilities;
        Ability ability = null;
        if(isRange)
        {
            ability = abilities.dexterity;
        }
        else//is not ranged
        {            
            foreach(WeaponProperty weaponProperty in weaponProperties)
            {
                if(weaponProperty == WeaponProperty.FINESE)
                {
                    if(abilities.dexterity.modifier>abilities.strength.modifier)
                    {
                        //use strength mod
                        ability = abilities.dexterity;
                    }
                    else
                    {
                        ability = abilities.strength;
                    }
                }
                else 
                {
                    ability = abilities.strength;
                }
            }
        }
        //Debug.Log("is Ability Null? " +ability==null? "null" : "not null");
        //Debug.Log("is Ability Null?" + ability + " " + nameof(ability.abilityType));
        return ability;
    }
    int CalcHitBonus(Ability ability)
    {
        int hitBonus;
        hitBonus = ability.modifier;
        bool isGroupProficient=false;
        //************************** extract method is proficient
        foreach(WeaponGroup weaponTypeProficiency in ((Hero)character).weaponGroupProficiencyList)
        {
            if(weaponTypeProficiency == weaponGroup)
            {
                hitBonus+=((Hero)character).proficiencyBonus;
                isGroupProficient = true;
                break;
            }
        }
        if(!isGroupProficient)
        {
            foreach(WeaponType weaponTypeProficiency in ((Hero)character).weaponProficiencyList)
            {
                if(weaponTypeProficiency == weaponType)
                {
                    hitBonus+=((Hero)character).proficiencyBonus;
                    break;
                }
            }
        }
        //******************
        return hitBonus;
    }

    int CalcDamageBonus(Ability ability)
    {
        return ability.modifier;
    }
    public WeaponSaveData SaveWeapon()
    {
        WeaponSaveData saveData = new WeaponSaveData();
        saveData.weapon_SO = weaponName;
        return saveData;
    }
    public void LoadWeapon(WeaponSaveData saveData)
    {
        var eqCollection = EquipmentCollection.instance;
        foreach(Weapon_SO weapon_SO in eqCollection.weaponList)
        {
            if(saveData.weapon_SO == weapon_SO.name)
            {
                weaponTemplate = weapon_SO;
                break;
            }
        }
    }
}