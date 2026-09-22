using System.Data.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor_SO", menuName = "Scriptable Objects/Armor_SO")]
public class Armor_SO : ScriptableObject
{
    public string armorName;
    public ArmorGroup armorGroup;
    public ArmorType armorType;

    public int armorClass;
    public bool isAddDexMod;
    [Tooltip("9999 means no limit")]
    public int dexModMax = 9999; // 9999 no limit
    public bool isMinStr;
    public int minStr;
    public bool isStealthDisadvantage;
}
