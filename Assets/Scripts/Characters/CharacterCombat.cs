using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public Abilities abilities;
    public List<Weapon> weaponList = new List<Weapon>();
    public Armor armor;
    public List<Attack>attackList = new List<Attack>();
    
    public int passivePerception;//(perception)//more bigger calculations //10+pereception skill(WisMod)
    public int armorClass;
    public int initiative;
    //  dex mod + d20

    public int speed;
    public int speedSquare;
    public DiceCollection.Dice hitDice;
    public int hitPointsMax;
    public int hitPointsCurrent;
    public int hitPointsTemporary;


    
    public void CalcArmorClass()
    {//will be hero only prob
        //armor.ProcessArmor(ref armorClass);
        armorClass = armor.ReturnAC();
    }
    public void CalcCombatStats()
    {
        initiative = abilities.dexterity.modifier/* +bonus*/;
        //if items change speed add from items for char
        CalcSpeed();
        
    }
    public void CalcSpeed()
    {
        speedSquare = speed/5;
    }
    public void GenerateAttacks()
    {//must change for hero and genral monster
        //from weapons
        if (weaponList.Count == 0)
        return;
        foreach(Weapon weapon in weaponList)
        {
            //Debug.Log("creating attack");
            Attack attack = weapon.ReturnAttack();
            //Debug.Log("Adding attack to list");
            attackList.Add(attack);
        }
    }
    public void SetCombatStats(HeroClass_SO heroClass, Race_SO heroRace)
    {
        hitDice = DiceCollection.instance.ReturnDicePerType(heroClass.hitDice);
        speed = heroRace.speed;
    }

    public CombatSaveData SaveCombat()
    {
        CombatSaveData saveData = new CombatSaveData();
        foreach(Weapon weapon in weaponList)
        {
            WeaponSaveData weaponSaveData = weapon.SaveWeapon();
            saveData.weaponList.Add(weaponSaveData);
        }

        ArmorSaveData armorSaveData = new ArmorSaveData();
        armorSaveData.armor_SO = armor.armorName;
        saveData.armor = armorSaveData;
        saveData.passivePerception = passivePerception;
        saveData.initiative = initiative;
        saveData.speed = speed;
        saveData.hpMax = hitPointsMax;
        saveData.hpCurrent = hitPointsCurrent;
        saveData.hpTemp = hitPointsTemporary;

        return saveData;

        //attackList??? mabyeNot
        //passivePerception
        //initiative
        //speed
        //hp max
        //hp current
        //hp temp
    }
    public void LoadCombat(CombatSaveData saveData, Character hero)
    {
        //load weapons to List
        foreach(WeaponSaveData weaponSaveData in saveData.weaponList)
        {
            Weapon weapon= new Weapon();
            weapon.weaponTemplate = EquipmentCollection.instance.ReturnWeaponFromString(weaponSaveData.weapon_SO);
            weapon.character = hero;
            weaponList.Add(weapon);
        }
        Armor createArmor = new Armor();
        createArmor.armorTemplate = EquipmentCollection.instance.ReturnArmorFromString(saveData.armor.armor_SO);
        armor = createArmor;
        //generate attacks
        passivePerception = saveData.passivePerception;
        //calc all armorClass
        initiative = saveData.initiative;
        speed = saveData.speed;
        //calc speed Square
        hitPointsMax = saveData.hpMax;
        hitPointsCurrent = saveData.hpCurrent;
        hitPointsTemporary = saveData.hpTemp;
    }
}
