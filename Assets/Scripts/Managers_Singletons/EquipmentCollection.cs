using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class EquipmentCollection : Singleton<EquipmentCollection>
{
    public static new EquipmentCollection instance => Singleton<EquipmentCollection>.instance;
    //public List<Armor>
    public List<Weapon_SO> simpleMeleeWeaponSO_List = new List<Weapon_SO>();
    public List<Weapon_SO> simpleRangedWeaponSO_List = new List<Weapon_SO>();
    public List<Weapon_SO> martialMeleeWeaponSO_List = new List<Weapon_SO>();
    public List<Weapon_SO> martialRangedWeaponSO_List = new List<Weapon_SO>();

    public List<Weapon_SO> weaponList = new List<Weapon_SO>();

    public List<Armor_SO> lightArmor_SOs = new List<Armor_SO>();
    public List<Armor_SO> mediumArmor_SOs = new List<Armor_SO>();
    public List<Armor_SO> heavyArmor_SOs = new List<Armor_SO>();
    public List<Armor_SO> shieldArmor_SOs = new List<Armor_SO>();
    public List<Armor_SO> armorList = new List<Armor_SO>();
    void Awake()
    {
        base.Awake();
        
    }
    public List<Weapon_SO> ReturnCorrectWeaponList(WeaponGroup weaponType)
    {
        switch(weaponType)
        {
            case WeaponGroup.SIMPLE_MELEE:
            {
                return simpleMeleeWeaponSO_List;
            }
            case WeaponGroup.SIMPLE_RANGED:
            {
                return simpleRangedWeaponSO_List;
            }
            case WeaponGroup.MARTIAL_MELEE:
            {
                return martialMeleeWeaponSO_List;
            }

            case WeaponGroup.MARTIAL_RANGE:
            {
                return martialRangedWeaponSO_List;
            }
            default:
            {
                return null;
            }
        }
    }
    public void ReturnWeaponGroupFromType(WeaponType weaponType)
    {

    }

    public Weapon_SO ReturnWeaponFromEnum(WeaponType weaponType)
    {
        //Debug.Log("returning weaponSO from enum");
        Weapon_SO returnWeaponSO = null;
        foreach(Weapon_SO weaponSO in weaponList)
        {
            if(weaponType == weaponSO.weaponType)
            {
                returnWeaponSO= weaponSO;
            }
        }
        return returnWeaponSO;
    }
    public Weapon_SO ReturnWeaponFromString(string weaponName)
    {
        //Debug.Log("returning weaponSO from string");
        Weapon_SO returnWeaponSO = null;
        foreach(Weapon_SO weaponSO in weaponList)
        {
            if(weaponName == weaponSO.weaponName)
            {
                returnWeaponSO= weaponSO;
            }
        }
        return returnWeaponSO;
    }
    public Armor_SO ReturnArmorFromEnum(ArmorType armorType)
    {
        //Debug.Log("returning armorSO from enum");
        Armor_SO returnArmorSO = null;
        foreach(Armor_SO armorSO in armorList)
        {
            if(armorType == armorSO.armorType)
            {
                returnArmorSO= armorSO;
            }
        }
        return returnArmorSO;
    }
    public Armor_SO ReturnArmorFromString(string armorName)
    {
        //Debug.Log("returning armorSO from string");
        Armor_SO returnArmorSO = null;
        foreach(Armor_SO armorSO in armorList)
        {
            if(armorName == armorSO.armorName)
            {
                returnArmorSO= armorSO;
            }
        }
        return returnArmorSO;
    }
       
      
}
