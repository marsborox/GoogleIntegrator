using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Combat_UI : MonoBehaviour
{
    private Hero _hero {get {return UI_Controller.instance.heroToDiplay;}}
    [SerializeField] private TextMeshProUGUI _AC;
    [SerializeField] private TextMeshProUGUI _initiative;
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private TextMeshProUGUI _HP_max;
    [SerializeField] private TextMeshProUGUI _HP_current;
    [SerializeField] private TextMeshProUGUI _HP_temporary;
    [SerializeField] private TextMeshProUGUI _weaponProficiencyList;
    [SerializeField] private TextMeshProUGUI _hitDiceText;

    [SerializeField] private GameObject _attackFieldList;
    [SerializeField] private AttackField_UI _attackField;
    void OnEnable()
    {
        UI_Controller.OnRefreshHero_UI += DispplayAllCombatFields;

    }
    void OnDisable()
    {
        UI_Controller.OnRefreshHero_UI -= DispplayAllCombatFields;

    }
    void DispplayAllCombatFields()
    {
        DisplayCombatValues();
        DisplayAttacks();
        DisplayWeaponProficiencyList();
        DisplayHitDiceText();
    }
    void DisplayCombatValues()
    {
        CharacterCombat combat = _hero.combat;
        _AC.text = combat.armorClass.ToString();
        _initiative.text = combat.initiative.ToString();
        _speed.text = combat.speed.ToString()+" ft "+combat.speedSquare.ToString()+"sq";
        _HP_max.text = combat.hitPointsMax.ToString();
        _HP_current.text = combat.hitPointsCurrent.ToString();
        _HP_temporary.text = combat.hitPointsTemporary.ToString();
        
    }
    void DisplayAttacks()
    {
        //delete all children
        foreach(Transform child in _attackFieldList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Attack attack in _hero.combat.attackList)
        {
            AttackField_UI attackField = Instantiate(_attackField);
            attackField.SetAttackField(attack);
            attackField.transform.parent = _attackFieldList.transform;
        }
    }

void DisplayWeaponProficiencyList()
    {
        string weaponProficiencyList = "";
        string comma = ", ";
        if(!(_hero.weaponGroupProficiencyList.Count==0))
        {
            foreach(WeaponGroup weaponType in _hero.weaponGroupProficiencyList)
            {
                weaponProficiencyList += weaponType.ToString() + comma;
            }
        }
            foreach(WeaponType weaponType in _hero.weaponProficiencyList)
            {   
                
                weaponProficiencyList += weaponType.ToString() + comma;

            }
        _weaponProficiencyList.text = weaponProficiencyList;

    }
    private void DisplayHitDiceText()
    {
        //_hitDiceText.text = _hero.level.ToString() + " " + _hero.hit
        _hitDiceText.text =   _hero.level.ToString()+" "+ _hero.combat.hitDice.name;
    }
    
    /*void DisplayWeaponProficiencyList()
    {
        string weaponProficiencyList = "";
        string comma = ", ";
        if(!(_hero.weaponGroupProficiencyList.Count==0))
        {
            foreach(WeaponGroup weaponType in _hero.weaponGroupProficiencyList)
            {
                weaponProficiencyList += weaponType.ToString() + comma;
            }
        }
            foreach(WeaponType weaponType in _hero.weaponProficiencyList)
            {   
                if(IsInGroupList(weaponType))
                weaponProficiencyList += weaponType.ToString() + comma;

            }
        _weaponProficiencyList.text = weaponProficiencyList;

        bool IsInGroupList(WeaponType weaponType)
        {
            Debug.Log("heroweapon Group proficiency list count: "+_hero.weaponGroupProficiencyList.Count);
            bool returnBool=false;
            if(!(_hero.weaponGroupProficiencyList.Count==0))
            {
                foreach(WeaponGroup weaponGroup in _hero.weaponGroupProficiencyList)
                {//weapon SO is null
                    Weapon_SO weapon_SO = EquipmentCollection.instance.ReturnWeaponFromEnum(weaponType);
                    Debug.Log("is weaponSO null " + (weapon_SO= null));
                    if(weapon_SO.weaponGroup==weaponGroup)
                    {
                        returnBool = true;
                    }
                }
            }
            return returnBool;
        }
    }*/

}
