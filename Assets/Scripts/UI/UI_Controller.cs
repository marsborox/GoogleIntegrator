using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : Singleton<UI_Controller>
{
    public static new UI_Controller instance => Singleton<UI_Controller>.instance;
    public delegate void UI_Event();
    public static UI_Event OnRefreshHero_UI;
    public static UI_Event OnRefreshEquipmentProficiencies_UI;

    
    public Hero heroToDiplay;
    public delegate void Notify(string message);



    public void RefReshHeroUIEvent()
    {
        //Debug.Log("Event refresh HeroUI");
        OnRefreshHero_UI?.Invoke();
    }
    public void RefreshHeroProficienciesUIEvent()
    {
        //Debug.Log("Event OnRefreshEquipmentProficiencies_UI");
        OnRefreshEquipmentProficiencies_UI?.Invoke();
    }
    
}
