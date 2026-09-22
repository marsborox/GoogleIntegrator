using UnityEngine;
[System.Serializable]
public class Armor
{
    public Armor_SO armorTemplate;
    public Armor_SO shieldTemplate;
    public Character character;
    public bool isProficient;
    public int armorClass=0;
    public string armorName{get{return armorTemplate.armorName;}}
    public ArmorGroup armorGroup{get{return armorTemplate.armorGroup;}}
    public ArmorType armorType{get{return armorTemplate.armorType;}}

    public int ReturnAC()
    {
        int ac=0;
        if(shieldTemplate!=null)
        {
            ac += shieldTemplate.armorClass;//might adjust for monk mabye other classes too
        }
        if(armorTemplate==null || armorTemplate.armorType == ArmorType.NO_ARMOR)
        {
            //Debug.Log("noArmor");
            ac += 10+character.abilities.dexterity.modifier;
        }
        else
        {
            ac += armorTemplate.armorClass;
            //Debug.Log("ac pre process armor "+ac);
            ProcessArmor(ref ac);
            //Debug.Log("ac post process armor "+ac);
        }
        return ac;
    }
    public void ProcessArmor(ref int ac)
    {
            //finish armor processing, perhaps change speed, 
            //need to check armor proficiency as well
        if(armorTemplate.isAddDexMod)
        {
            int charDexMod = character.abilities.dexterity.modifier;
            int dexMod = armorTemplate.dexModMax;

            if(character.abilities.dexterity.modifier>armorTemplate.dexModMax)
            {ac += armorTemplate.dexModMax;}
            else 
            {ac += character.abilities.dexterity.modifier;}
            
        }
        if(armorTemplate.isMinStr)
        {
            if(character.abilities.strength.score<armorTemplate.minStr)
            {//lower
                //if not dwarf//migrate
                character.combat.speed-=10;
 
            }
        }
        //if()
        CheckProficiency();
        //if(character.)//weapon proficiencies
    }
    void CheckProficiency()
    {
        bool isArmorProficient=false;
        bool isShieldProficient = false;
        //Debug.Log("checkignProficiency");
        foreach(ArmorGroup armorGroupProficiency in ((Hero)character).armorGroupProficiencyList)
        {
            if(armorTemplate.armorGroup == armorGroupProficiency)
            {
                isArmorProficient=true;//
                break;
            }

        }

        if(shieldTemplate!=null && shieldTemplate.armorGroup == ArmorGroup.SHIELD)
        {
            //do something if nto proficient w shield
            //isShieldProficient = true;
            isArmorProficient = false;
        }
        if(!isArmorProficient)
        {

            ArmorNotProficient();
        }
        void ArmorNotProficient()
        {
            //notp roficient w armor do somethign
            //disadvantage on
            //any ability check, saving throw, or attack roll that
            //involves Strength or Dexterity, and you can’t cast
            //spells. same for shield
            Debug.Log("armor not proficient");
        }
    }
}
