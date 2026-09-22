using UnityEngine;
using TMPro;

public class Hero_MainInfo_UI : UI
{
    //Hero hero = UI_Controller.instance.heroToDiplay;
    private Hero _hero {get {return UI_Controller.instance.heroToDiplay;}}
    [SerializeField] private TextMeshProUGUI _characterName;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _class;
    [SerializeField] private TextMeshProUGUI _race;
    [SerializeField] private TextMeshProUGUI _backGround;
    [SerializeField] private TextMeshProUGUI _alignment;
    [SerializeField] private TextMeshProUGUI _experience;


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
        UI_Controller.OnRefreshHero_UI += DisplayMainInfo;
    }
    void UnSubscribeToEvents()
    {
        UI_Controller.OnRefreshHero_UI -= DisplayMainInfo;
    }
    void DisplayMainInfo()
    {
        _characterName.text = _hero.characterName;
        _level.text = _hero.level.ToString();
        _class.text = _hero.heroClass.className;
        _race.text = _hero.heroRace.name;
        _backGround.text = _hero.backGround;
        _alignment.text = _hero.alignment;
        _experience.text = _hero.experience.ToString();//add / maxXPtoNextLevel

    }
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
}
