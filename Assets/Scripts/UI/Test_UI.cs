using UnityEngine;
using UnityEngine.UI;
public class Test_UI : UI
{
    [SerializeField] public Button travelButton;
    [SerializeField] public GameObject travelGUI;
    
    [SerializeField] public Button returnHome;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitiateButton(travelButton, OpenCloseUI,travelGUI); //Button, GameObject
        InitiateButton(returnHome, ReturnHome);//Button, Method
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ReturnHome()
    {
        
    }
}
