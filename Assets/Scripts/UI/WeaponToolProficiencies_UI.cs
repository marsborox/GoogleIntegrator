using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponToolProficiencies_UI : MonoBehaviour
{
    private Hero _hero {get {return UI_Controller.instance.heroToDiplay;}}
    [SerializeField] private GameObject _simpleMeleeWeaponListUI;
    [SerializeField] private GameObject _simpleRangedWeaponListUI;
    [SerializeField] private GameObject _martialMeleeWeaponListUI;
    [SerializeField] private GameObject _martialRangedWeaponListUI;
    public BoolValueText_panelUI weaponBoolPanelPrefab;
    void Start()
    {
        //DisplayWeaponProficiencies();
    }
    void OnEnable()
    {
        //UI_Controller.OnRefreshHero_UI += DisplayWeaponProficiencies;
        DisplayWeaponProficiencies();
    }
    void OnDisable()
    {
        //UI_Controller.OnRefreshHero_UI -= DisplayWeaponProficiencies;
        CloseWeaponLists();
    }
    void DisplayWeaponProficiencies()
    {
        DisplayWeaponTypeList(EquipmentCollection.instance.simpleMeleeWeaponSO_List,_simpleMeleeWeaponListUI);
        DisplayWeaponTypeList(EquipmentCollection.instance.simpleRangedWeaponSO_List,_simpleRangedWeaponListUI);
        DisplayWeaponTypeList(EquipmentCollection.instance.martialMeleeWeaponSO_List,_martialMeleeWeaponListUI);
        DisplayWeaponTypeList(EquipmentCollection.instance.martialRangedWeaponSO_List,_martialRangedWeaponListUI);
    }
    void DisplayWeaponTypeList(List<Weapon_SO> weaponSoList, GameObject weaponList)
    {
        //Wea

        //Debug.Log("weaponList is: "+((weaponList==null)? "null" : "notNull"));
        //Debug.Log("weaponSoList is: "+((weaponSoList==null)? "null" : "notNull count "+weaponSoList.Count));
        
        foreach(Weapon_SO weapon_SO in weaponSoList)
        {
            BoolValueText_panelUI spawnedWeaponPanel = Instantiate(weaponBoolPanelPrefab);
            
            spawnedWeaponPanel.transform.parent = WeaponTypeistSwitch(weapon_SO).transform;
            spawnedWeaponPanel.transform.parent = weaponList.transform;
            bool isProficient = IsProficient(weapon_SO);
            spawnedWeaponPanel.SetValuesNameAndBool(isProficient,"",weapon_SO.weaponName);
        }
    }
    void CloseWeaponLists()
    {
        DestroyAllChildren(_simpleMeleeWeaponListUI);
        DestroyAllChildren(_simpleRangedWeaponListUI);
        DestroyAllChildren(_martialMeleeWeaponListUI);
        DestroyAllChildren(_martialRangedWeaponListUI);

        void DestroyAllChildren(GameObject parentObject)
        {
            foreach(Transform child in parentObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    GameObject WeaponTypeistSwitch(Weapon_SO weapon_SO)
    {
        GameObject returnListToSpawn = null;
        //Debug.Log("weaponGroup enum "+weapon_SO.weaponGroup.ToString());
        switch(weapon_SO.weaponGroup)
        {
            case WeaponGroup.SIMPLE_MELEE:
                {
                    returnListToSpawn = _simpleMeleeWeaponListUI;
                    break;
                }
            case WeaponGroup.SIMPLE_RANGED:
                {
                    returnListToSpawn = _simpleRangedWeaponListUI;
                    break;
                }
            case WeaponGroup.MARTIAL_MELEE:
                {
                    returnListToSpawn = _martialMeleeWeaponListUI;
                    break;
                }
            case WeaponGroup.MARTIAL_RANGE:
                {
                    returnListToSpawn = _martialRangedWeaponListUI;
                    break;
                }
            default:
                {
                    //Debug.Log("Returning nullList");
                    returnListToSpawn = null;
                    break;
                }
        }
        //Debug.Log("returnListToSpawn is: "+((returnListToSpawn==null)? "null" : "notNull"));
        return returnListToSpawn;
    }

    bool IsProficient(Weapon_SO weapon_SO)
    {
        bool isProficient = false;
        foreach(WeaponGroup weaponGroupProficiency in _hero.weaponGroupProficiencyList)
        {
            if(weaponGroupProficiency == weapon_SO.weaponGroup)
            {
                
                isProficient = true;
                break;
            }
        }
        if(!isProficient)
        {
            foreach(WeaponType weaponType in _hero.weaponProficiencyList)
            if(weaponType == weapon_SO.weaponType)
            {
                isProficient = true;
                break;
                
            }
            
        }
        return isProficient;
    }
    
}
