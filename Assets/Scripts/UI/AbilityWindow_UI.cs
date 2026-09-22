using TMPro;
using UnityEngine;

public class AbilityWindow_UI : UI
{
    private Hero _hero {get {return UI_Controller.instance.heroToDiplay;}}
    [SerializeField] private TextMeshProUGUI _strengthScore;
    [SerializeField] private TextMeshProUGUI _strengthModifier;
    [SerializeField] private TextMeshProUGUI _dexterityScore;
    [SerializeField] private TextMeshProUGUI _dexterityModifier;
    [SerializeField] private TextMeshProUGUI _constitutionScore;
    [SerializeField] private TextMeshProUGUI _constitutionModifier;
    [SerializeField] private TextMeshProUGUI _intelligenceScore;
    [SerializeField] private TextMeshProUGUI _intelligenceModifier;
    [SerializeField] private TextMeshProUGUI _wisdomScore;
    [SerializeField] private TextMeshProUGUI _wisdomModifier;
    [SerializeField] private TextMeshProUGUI _charismaScore;
    [SerializeField] private TextMeshProUGUI _charismaModifier;

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
        UI_Controller.OnRefreshHero_UI += DisplayAbilityInfo;
    }
    void UnSubscribeToEvents()
    {
        UI_Controller.OnRefreshHero_UI -= DisplayAbilityInfo;
    }
    void DisplayAbilityInfo()
    {
        Abilities abilities = _hero.abilities;
        _strengthScore.text = abilities.strength.score.ToString();
        _strengthModifier.text = abilities.strength.modifier.ToString();

        _dexterityScore.text = abilities.dexterity.score.ToString();
        _dexterityModifier.text = abilities.dexterity.modifier.ToString();

        _constitutionScore.text = abilities.constitution.score.ToString();
        _constitutionModifier.text = abilities.constitution.modifier.ToString();

        _intelligenceScore.text = abilities.intelligence.score.ToString();
        _intelligenceModifier.text = abilities.intelligence.modifier.ToString();

        _wisdomScore.text = abilities.wisdom.score.ToString();
        _wisdomModifier.text = abilities.wisdom.modifier.ToString();

        _charismaScore.text = abilities.charisma.score.ToString();
        _charismaModifier.text = abilities.charisma.modifier.ToString();
    }
}
