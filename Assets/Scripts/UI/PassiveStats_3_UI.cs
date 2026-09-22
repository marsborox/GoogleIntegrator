using TMPro;
using UnityEngine;

public class PassiveStats_3_UI : MonoBehaviour
{
    private Hero _hero {get {return UI_Controller.instance.heroToDiplay;}}
    [SerializeField] private TextMeshProUGUI _passiveWisdom;
    [SerializeField] private TextMeshProUGUI _inspiration;
    [SerializeField] private TextMeshProUGUI _proficiencyBnus;

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
        UI_Controller.OnRefreshHero_UI += DisplayStats;

    }
    void UnSubscribeToEvents()
    {
        UI_Controller.OnRefreshHero_UI -= DisplayStats;

    }
    private void DisplayStats()
    {
        _passiveWisdom.text = _hero.passivePerception.ToString();
        _inspiration.text = _hero.isInspiration? "X":" ";
        _proficiencyBnus.text = _hero.proficiencyBonus.ToString();
    }
}
