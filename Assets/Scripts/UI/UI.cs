using UnityEngine;
using UnityEngine.UI;
using System;

public class UI : MonoBehaviour
{
    /*[SerializeField] public Button travelButton;
    [SerializeField] public GameObject travelGUI;
    
    [SerializeField] public Button returnHome;*/
    
    void Start()
    {
        //InitiateButton(travelButton, OpenCloseUI,travelGUI); //Button, GameObject
        //InitiateButton(returnHome, ReturnHome);//Button, Method
    }
    
    void InitiateButton(/*bool boolUI,*/Button button ,GameObject gUIPanel,bool tempBoolean)
    {
        button.onClick.AddListener(delegate
        {
            
            OpenCloseUI(/*boolUI,*/ button, gUIPanel);
            //boolUI = tempBoolean;
            gUIPanel.SetActive(tempBoolean);
        });
        //boolUI = false;
        gUIPanel.SetActive(false);
    }

    public void InitiateButton(Button button, Action method)
    {//Might be issue here
        button.onClick.AddListener(delegate
        {
            method();
        });
        //boolUI = false;
    }
    
    public void InitiateButton<T>(Button button, Action<T> method,T value)
{
    button.onClick.AddListener(delegate
    {
        method(value);
    });
    //boolUI = false;
}
public void InitiateButton<T>(Button button, Action <Button,T> method,T value)
{
    //InitiateButton(EquipmentProficienciesUI_Button,OpenCloseUI,EquipmentProficienciesUI);
    button.onClick.AddListener(delegate
    {
        method(button, value);
    });
    //boolUI = false;
}
    /*public void InitiateButton<T>(Button button, Action<T> method,T value)
{
    button.onClick.AddListener(delegate
    {
        method(value);
    });
    //boolUI = false;
}*/
/*
public void InitiateButton (Button button, Action<Button,UI> method, UI ui)
{
    button.onClick.AddListener(delegate
    {
        method(button, ui);
    });
    //boolUI = false;
}*/
public void InitiateButtonFunc<T>(Button button, Func<T> method)
{//will remove this later
    button.onClick.AddListener(delegate
    {
        method();
    });
    //boolUI = false;
}
public void RemoveListeners(Button button)
{ 
    button.onClick.RemoveAllListeners();
}
    
    
    //this should go to UI class
    //open/close UI or ganeObject
    public void OpenCloseUI( Button button, GameObject gUIPanel)
    {//turn off/on uiMenuPanel
        if (!gUIPanel.activeSelf)
        {
            //tempBoolean = true;
            //button.GetComponent<Image>().color = pressedColor;
            gUIPanel.SetActive(true);
        }
        else
        {
            //tempBoolean = false;
            //button.GetComponent<Image>().color = unpressedColor;
            gUIPanel.SetActive(false);
        }
    }

}
