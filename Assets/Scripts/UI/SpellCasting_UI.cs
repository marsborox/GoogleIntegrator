using TMPro;
using UnityEngine;

public class SpellCasting_UI : MonoBehaviour
{
    private Hero _hero {get {return UI_Controller.instance.heroToDiplay;}}
    [SerializeField] private TextMeshProUGUI _spellCastingClass;
    [SerializeField] private TextMeshProUGUI _spellCastingAbility;
    [SerializeField] private TextMeshProUGUI _spellSaveDC;
    [SerializeField] private TextMeshProUGUI _spellAttackBonus;

    void OnEnable()
    {
        SubscribeToEvents();
    }
    void OnDisable()
    {
        UnSubscribeToEvents();
    }
    void SubscribeToEvents()
    {
        UI_Controller.OnRefreshHero_UI += DisplaySpellCastingInfo;
    }
    void UnSubscribeToEvents()
    {
        UI_Controller.OnRefreshHero_UI -= DisplaySpellCastingInfo;
    }

    void DisplaySpellCastingInfo()
    {

        //_spellCastingClass.text = _hero.characterSpellcasting.i
        _spellCastingAbility.text = _hero.characterSpellcasting.spellcastingAbility.abilityType.ToString();
        _spellSaveDC.text = _hero.characterSpellcasting.spellSaveDC.ToString();
        _spellAttackBonus.text = _hero.characterSpellcasting.spellAttackBonus.ToString();
    }
}
