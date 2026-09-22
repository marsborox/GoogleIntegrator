using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using MessagePack;
public class Basic_UI : UI
{
    
    [SerializeField] private TMP_InputField _inputField;
    
    [Header("Google Docs General")]
    [SerializeField] private TestHero _testHero;
    [SerializeField] private Button _sendImgButton;
    [SerializeField] private Button _downloadImgButton;
    [SerializeField] private Button _sendTxtButton;
    [SerializeField] private Button _downloadTxtButton;
    [SerializeField] private Button _testGoogleDriveButton;
    [SerializeField] private Button _displayGFilesButton_GET_Request;
    [SerializeField] private Button _createGFolderButton;
    [SerializeField] private Button _createGSheetButton_POST_Request;
    [SerializeField] private Button _returnRootFolderButton;
    [Header("Google Sheets")]
    [SerializeField] private Button _testValuefromCellSheetButton;
    [SerializeField] private Button _testValuefromRangeSheetButton;
    [SerializeField] private Button _testNumberfromRowSheetButton;
    [SerializeField] private Button _testValuesFromMultipleCellsBatchButton;
    [SerializeField] private Button _writeValueToCell_PUT_Request;//PUT Request
    [SerializeField] private Button _writeValueToCell;
    [SerializeField] private Button _writeValueToRangeOfCells;
    [SerializeField] private Button _writeValuesInBatch;

    void Start()
    {

        SetValues();
        //AssignTextFIeldsOnValueChanged();
        AssignTextFIeldsOnEndEdit();
        AssigntTextFieldsOnSelect();
        AssigntTextFieldsOnDeSelect();
        SubscribeButtons();
    }

    void SubscribeButtons()
    {
        InitiateButton(_sendImgButton,SendData);
        InitiateButton(_sendImgButton,GoogleIntegratorController.instance.SendImage);
        InitiateButton(_downloadImgButton,GoogleIntegratorController.instance.DownloadImage);
        InitiateButton(_sendTxtButton,GoogleIntegratorController.instance.SendTextFile);
        InitiateButton(_downloadTxtButton,GoogleIntegratorController.instance.DownloadTextFile);

        InitiateButton(_testGoogleDriveButton, GoogleIntegratorController.instance.TestGoogleDrive);
        InitiateButton(_displayGFilesButton_GET_Request,GoogleIntegratorController.instance.DisplayGFiles);//test GET request
        InitiateButton(_createGFolderButton, GoogleIntegratorController.instance.CreateGFolder);
        InitiateButton(_createGSheetButton_POST_Request, GoogleIntegratorController.instance.CreateGSheet);// Test POST request
        InitiateButton(_returnRootFolderButton, GoogleIntegratorController.instance.TestReturnAllRootFolderFiles);


        InitiateButton(_testValuefromCellSheetButton, GoogleIntegratorController.instance.TestReadCellFromSheet);
        InitiateButton(_testValuefromRangeSheetButton,GoogleIntegratorController.instance.TestReadCellsInRangeFromSheet);
        InitiateButton(_testNumberfromRowSheetButton,GoogleIntegratorController.instance.TestReadNumberCellFromSheet);
        InitiateButton(_testValuesFromMultipleCellsBatchButton, GoogleIntegratorController.instance.TestReadMultipleCellsRange);

        InitiateButton(_writeValueToCell_PUT_Request,GoogleIntegratorController.instance.WriteToCellTestPUT);
        InitiateButton(_writeValueToCell,GoogleIntegratorController.instance.WriteToCell);
        InitiateButton(_writeValueToRangeOfCells,GoogleIntegratorController.instance.WriteToCellRange);
        InitiateButton(_writeValuesInBatch,GoogleIntegratorController.instance.WriteToCellBatch);
    }
    void AssignTextFIeldsOnValueChanged()
    {
        _inputField.onValueChanged.AddListener(delegate
        {
           SetHeroName(_inputField.text); 
        });
    }    
    void AssignTextFIeldsOnEndEdit()
    {
        _inputField.onEndEdit.AddListener(delegate
        {
           SetHeroName(_inputField.text); 
        });
    }
    void AssigntTextFieldsOnSelect()
    {
        //Debug.Log("field selected");
    }
    void AssigntTextFieldsOnDeSelect()
    {
        //Debug.Log("field deSelected");
    }
    void SetHeroName(string name)
    {
        //string name = "111";
        _testHero.SetName(name);
        //Debug.Log("hero name set to: "+name);
    }
    void SendData()
    {
        //Debug.Log("Sending data");
    }
    void SetValues()
    {
        _inputField.text = _testHero.ReturnName();
    }
}
