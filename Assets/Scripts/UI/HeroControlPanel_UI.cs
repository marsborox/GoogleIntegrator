using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroControlPanel_UI : UI
{
    [SerializeField] private Button _equipmentProficienciesUI_Button;
    [SerializeField] private GameObject _equipmentProficienciesUI;

    [SerializeField] private Button _saveHero_Button;
    [SerializeField] private Button _loadHero_Button;
    void Start()
    {
        InitiateButtons();
    }
    void InitiateButtons()
    {
        //InitiateButton(_equipmentProficienciesUI_Button,OpenCloseUI,_equipmentProficienciesUI);
        InitiateButton(_equipmentProficienciesUI_Button,OpenWepaonProficiencies,_equipmentProficienciesUI_Button);
        InitiateButton(_saveHero_Button,SaveLoadSystem.instance.SavewHero);
        InitiateButton(_loadHero_Button,SaveLoadSystem.instance.LoadHero);
    }

    void OpenWepaonProficiencies(Button button)
    {
        OpenCloseUI(button,_equipmentProficienciesUI);
        UI_Controller.instance.RefreshHeroProficienciesUIEvent();
    }
    
}
