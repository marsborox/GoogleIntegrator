using TMPro;
using UnityEngine;

public class Saves_Skills_UI : UI
{
    private Hero _hero {get {return UI_Controller.instance.heroToDiplay;}}
    [Header("AbilitySaves")]
    [SerializeField] private TextMeshProUGUI _strength;
    [SerializeField] private TextMeshProUGUI _dexterity;
    [SerializeField] private TextMeshProUGUI _constitution;
    [SerializeField] private TextMeshProUGUI _intelligence;
    [SerializeField] private TextMeshProUGUI _wisdom;
    [SerializeField] private TextMeshProUGUI _charisma;
    [SerializeField] private BoolValueText_panelUI _panel_strength;
    [SerializeField] private BoolValueText_panelUI _panel_dexterity;
    [SerializeField] private BoolValueText_panelUI _panel_constitution;
    [SerializeField] private BoolValueText_panelUI _panel_intelligence;
    [SerializeField] private BoolValueText_panelUI _panel_wisdom;
    [SerializeField] private BoolValueText_panelUI _panel_charisma;
    [Header ("Skills")]
    [SerializeField] private TextMeshProUGUI _acrobatics;
    [SerializeField] private TextMeshProUGUI _animalHandling;
    [SerializeField] private TextMeshProUGUI _arcana;
    [SerializeField] private TextMeshProUGUI _athletics;
    [SerializeField] private TextMeshProUGUI _deception;
    [SerializeField] private TextMeshProUGUI _history;
    [SerializeField] private TextMeshProUGUI _insight;
    [SerializeField] private TextMeshProUGUI _intimidation;
    [SerializeField] private TextMeshProUGUI _investigation;
    [SerializeField] private TextMeshProUGUI _medicine;
    [SerializeField] private TextMeshProUGUI _nature;
    [SerializeField] private TextMeshProUGUI _perception;
    [SerializeField] private TextMeshProUGUI _performance;
    [SerializeField] private TextMeshProUGUI _persuation;
    [SerializeField] private TextMeshProUGUI _religion;
    [SerializeField] private TextMeshProUGUI _seightOfHand;
    [SerializeField] private TextMeshProUGUI _stealth;
    [SerializeField] private TextMeshProUGUI _survival;



    [SerializeField] private BoolValueText_panelUI _panel_acrobatics;
    [SerializeField] private BoolValueText_panelUI _panel_animalHandling;
    [SerializeField] private BoolValueText_panelUI _panel_arcana;
    [SerializeField] private BoolValueText_panelUI _panel_athletics;
    [SerializeField] private BoolValueText_panelUI _panel_deception;
    [SerializeField] private BoolValueText_panelUI _panel_history;
    [SerializeField] private BoolValueText_panelUI _panel_insight;
    [SerializeField] private BoolValueText_panelUI _panel_intimidation;
    [SerializeField] private BoolValueText_panelUI _panel_investigation;
    [SerializeField] private BoolValueText_panelUI _panel_medicine;

    [SerializeField] private BoolValueText_panelUI _panel_nature;
    [SerializeField] private BoolValueText_panelUI _panel_perception;
    [SerializeField] private BoolValueText_panelUI _panel_performance;
    [SerializeField] private BoolValueText_panelUI _panel_persuation;
    [SerializeField] private BoolValueText_panelUI _panel_religion;
    [SerializeField] private BoolValueText_panelUI _panel_seightOfHand;
    [SerializeField] private BoolValueText_panelUI _panel_stealth;
    [SerializeField] private BoolValueText_panelUI _panel_survival;


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
        //UI_Controller.OnRefreshHero_UI += DisplaySaves;
        UI_Controller.OnRefreshHero_UI += DisplayAllSavePanels;
        //UI_Controller.OnRefreshHero_UI += DisplaySkillValues;
        UI_Controller.OnRefreshHero_UI += DiplayAllSkillPanels;
    }
    void UnSubscribeToEvents()
    {
        //UI_Controller.OnRefreshHero_UI -= DisplaySaves;
        UI_Controller.OnRefreshHero_UI -= DisplayAllSavePanels;
        //UI_Controller.OnRefreshHero_UI -= DisplaySkillValues;
        UI_Controller.OnRefreshHero_UI -= DiplayAllSkillPanels;
    }


    void DisplaySaves()
    {   
        Abilities abilities= _hero.abilities;
        _strength.text = abilities.strength.savingThrow.ToString();
        _dexterity.text = abilities.dexterity.savingThrow.ToString();
        _constitution.text = abilities.constitution.savingThrow.ToString();
        _intelligence.text = abilities.intelligence.savingThrow.ToString();
        _wisdom.text = abilities.wisdom.savingThrow.ToString();
        _charisma.text = abilities.charisma.savingThrow.ToString();
    }
    void DisplaySkillValues()
    {
        Skills skills = _hero.skills;
        _acrobatics.text = skills.acrobatics.skillValue.ToString();
        _animalHandling.text = skills.animalHandling.skillValue.ToString();
        _arcana.text = skills.arcana.skillValue.ToString();
        _athletics.text = skills.athletics.skillValue.ToString();
        _deception.text = skills.deception.skillValue.ToString();
        _history.text = skills.history.skillValue.ToString();
        _insight.text = skills.insight.skillValue.ToString();
        _intimidation.text = skills.intimidation.skillValue.ToString();
        _investigation.text = skills.investigation.skillValue.ToString();
        _medicine.text = skills.medicine.skillValue.ToString();
        _nature.text = skills.nature.skillValue.ToString();
        _perception.text = skills.perception.skillValue.ToString();
        _performance.text = skills.performance.skillValue.ToString();
        _persuation.text = skills.persuation.skillValue.ToString();
        _religion.text = skills.religion.skillValue.ToString();
        _seightOfHand.text = skills.sleightOfHand.skillValue.ToString();
        _stealth.text = skills.sleightOfHand.skillValue.ToString();
        _survival.text = skills.sleightOfHand.skillValue.ToString();
    }

    void DiplayAllSkillPanels()
    {
        Skills skills = _hero.skills;
        DisplaySkillPanel(_panel_acrobatics,skills.acrobatics);
        DisplaySkillPanel(_panel_animalHandling,skills.animalHandling);
        DisplaySkillPanel(_panel_arcana,skills.arcana);
        DisplaySkillPanel(_panel_athletics,skills.athletics);
        DisplaySkillPanel(_panel_deception,skills.deception);
        DisplaySkillPanel(_panel_history,skills.history);
        DisplaySkillPanel(_panel_insight,skills.insight);
        DisplaySkillPanel(_panel_intimidation,skills.intimidation);
        DisplaySkillPanel(_panel_investigation,skills.investigation);
        DisplaySkillPanel(_panel_medicine,skills.medicine);

        DisplaySkillPanel(_panel_nature,skills.nature);
        DisplaySkillPanel(_panel_perception,skills.perception);
        DisplaySkillPanel(_panel_performance,skills.performance);
        DisplaySkillPanel(_panel_persuation,skills.persuation);
        DisplaySkillPanel(_panel_religion,skills.nature);
        DisplaySkillPanel(_panel_seightOfHand,skills.sleightOfHand);
        DisplaySkillPanel(_panel_stealth,skills.stealth);
        DisplaySkillPanel(_panel_survival,skills.survival);
    }
    private void DisplaySkillPanel(BoolValueText_panelUI panel, Skill skill)
    {
        panel.SetValues(skill.isProficient,skill.skillValue.ToString());
    }
    void DisplayAllSavePanels()
    {
        Abilities abilities= _hero.abilities;
        DisplaySavePanel(_panel_strength,abilities.strength);
        DisplaySavePanel(_panel_dexterity,abilities.dexterity);
        DisplaySavePanel(_panel_constitution,abilities.constitution);
        DisplaySavePanel(_panel_intelligence,abilities.intelligence);
        DisplaySavePanel(_panel_wisdom,abilities.wisdom);
        DisplaySavePanel(_panel_charisma,abilities.charisma);
    }
    private void DisplaySavePanel(BoolValueText_panelUI panel, Ability ability)
    {
        panel.SetValues(ability.isProficient,ability.savingThrow.ToString());
    }
}
