using TMPro;
using UnityEngine;

public class AttackField_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _attackName;
    [SerializeField] private TextMeshProUGUI _toHitBonus;
    [SerializeField] private TextMeshProUGUI _attackDamage;
    [SerializeField] private TextMeshProUGUI _attackType;

    public void SetAttackField(Attack attack)
    {
        _attackName.text = attack.name;
        _toHitBonus.text = "+ " + attack.toHitBonus.ToString();

        //dice missing
        
        //DiceCollection.Dice dice = DiceCollection.instance.ReturnDIcePerType(attack.damageDice);
        string diceName = DiceCollection.instance.ReturnDiceNamePerType(attack.damageDice);

        _attackDamage.text = diceName+ " + " + attack.damageBonus/* + " " + attack.damageType*//*nameof(attack.damageType)*/;
        _attackType.text = attack.damageType.ToString();

        //damastring attackDamage = attack.damageDice.
    }
}
