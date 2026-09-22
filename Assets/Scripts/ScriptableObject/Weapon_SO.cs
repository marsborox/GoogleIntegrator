using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_SO", menuName = "Scriptable Objects/Weapon_SO")]
public class Weapon_SO : ScriptableObject
{
    public string weaponName;
    public WeaponGroup weaponGroup;
    public WeaponType weaponType;
    public int amountOfDamageDice = 1;
    public DiceType damage;
    public DamageType damageType;
    public bool isVersatile;
    public DiceType versatile2H_damage;
    public List<WeaponProperty> weaponProperties = new List<WeaponProperty>();
    public bool isRange;
    public int rangeMin;
    public int rangeMax;
}