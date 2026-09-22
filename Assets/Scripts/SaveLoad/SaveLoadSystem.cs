using System.Collections.Generic;
using System.IO;
using MessagePack;
using UnityEngine;

public class SaveLoadSystem : Singleton<SaveLoadSystem>
{
    public static new SaveLoadSystem instance => Singleton<SaveLoadSystem>.instance;
    public string fileName = "/gameSave.sav";
    public string heroSaveName = "/hero.sav";
    public string defaultSaveDirectory;
    //public string savePath;

    private void Awake()
    {
        base.Awake();
        defaultSaveDirectory = Application.persistentDataPath;// sets to some default location
    }
    public void SavewHero()
    {
        HeroSavedata heroSavedata = HeroManager.instance.hero.SaveHero();
        string savePath = defaultSaveDirectory+heroSaveName;
        byte[] bytes = MessagePackSerializer.Serialize(heroSavedata);
        File.WriteAllBytes(defaultSaveDirectory+heroSaveName,bytes);
        Debug.Log("save at " + savePath);

    }
    public void LoadHero()
    {
        string savePath = defaultSaveDirectory + heroSaveName;
        if (File.Exists(savePath))
        {
            byte[] bytes = File.ReadAllBytes(savePath);
            HeroSavedata heroSavedata  = MessagePackSerializer.Deserialize<HeroSavedata>(bytes);
            
            HeroManager.instance.hero.LoadHero(heroSavedata);
        }
    }
}
[MessagePackObject]
public class SaveData
{
    [Key(0)] public HeroSavedata heroSavedata;
    /*[Key(0)] public Race_SO race;
    [Key(1)] public HeroClass_SO heroClass;
    [Key(2)] public List<Ability> heroList;*/

}
[MessagePackObject]
public class HeroSavedata
{
    [Key(0)] public string heroName;
    [Key(1)] public int heroLevel;
    [Key(2)] public int heroExperience;

    //[Key(3)] public HeroClass_SO heroClass;
    [Key(3)] public string heroClass;
    //[Key(4)] public Race_SO heroRace;
    [Key(5)] public string heroRace;
    //[Key(12)]public List<Ability> abilityList = new List<Ability>();
    [Key(11)] public List<AbilitySaveData> abilityScores;
    [Key(12)] public List<SkillSaveData> skills;
    //[Key(5)]public List<Weapon> weaponList = new List<Weapon>();
     
    //[Key(6)]public Armor armor;
    [Key(7)]public List<WeaponGroup> weaponGroupProficiencyList = new List<WeaponGroup>();
    [Key(8)]public List<WeaponType> weaponProficiencyList = new List<WeaponType>();
    [Key(9)]public List<ArmorGroup> armorGroupProficiencyList = new List<ArmorGroup>();
    [Key(10)]public List<ArmorType>armorTypeProficiencyList = new List<ArmorType>();
    //[Key(11)]public List<Skill> heroSkillList;
    [Key(13)] public CombatSaveData combatSaveData;
    [Key(14)] public SpellcastingSaveData spellcastingSaveData;
}
[MessagePackObject]
public class AbilitySaveData
{
    [Key(0)] public AbilityType abilityType;
    [Key(1)] public int abilityScore;
    [Key(2)] public bool isAbilityProficient;

}
[MessagePackObject]
public class SkillSaveData
{
    [Key(0)] public AbilityType skillAbilityType;
    [Key(1)] public SkillType skillType;
    [Key(2)] public bool isProficient;
    [Key(3)] public int skillValue;
}
[MessagePackObject]
public class CombatSaveData
{
    [Key(0)] public List<WeaponSaveData> weaponList= new List<WeaponSaveData>();
    [Key(1)] public ArmorSaveData armor;
    [Key(2)] public int passivePerception;
    [Key(3)] public int initiative;
    [Key(4)] public int speed;
    [Key(5)] public int hpMax;
    [Key(6)] public int hpCurrent;
    [Key(7)] public int hpTemp;
    
}
[MessagePackObject]
public class WeaponSaveData
{
    [Key(0)] public string weapon_SO;
    //weapon template is only thing we need for now
    
}
[MessagePackObject]
public class ArmorSaveData
{
    [Key(0)] public string armor_SO;
}
[MessagePackObject]
public class SpellcastingSaveData
{
    [Key(0)] public AbilityType spellcastingAbility;
    [Key(1)] public int spellSaveDC;
    [Key(2)] public int spellAttackBonus;

    //spells known, prepared, spellSlots
}

