using Unity.VisualScripting;
using UnityEngine;

public class GoogleIntegratorController : Singleton<GoogleIntegratorController>
{
    public static new GoogleIntegratorController instance => Singleton<GoogleIntegratorController>.instance;

    private GoogleIntegrator _googleIntegrator;
    private GoogleSheetIntegrator _googleSheetIntegrator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        _googleIntegrator = GetComponent<GoogleIntegrator>();
        _googleSheetIntegrator = GetComponent<GoogleSheetIntegrator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    


    public void SendImage()
    {
        StartCoroutine(_googleIntegrator.SendImageRequest());
    }

    public void DownloadImage()
    {
        StartCoroutine(_googleIntegrator.DownloadImageRequest());
    }
    public void SendTextFile()
    {
        StartCoroutine(_googleIntegrator.SendTxtRequest());
    }
    public void DownloadTextFile()
    {
        StartCoroutine(_googleIntegrator.DownloadTxtRequest());
    }
    public void TestGoogleDrive()
    {
        StartCoroutine(_googleIntegrator.TestGoogleDriveRequest());
    }



    public void DisplayGFilesWithRefresh()
    {
        StartCoroutine(_googleIntegrator.DisplayGFilesRequestWithRefreshRequest());
    }
    public void DisplayGFiles()
    {//new
        _googleIntegrator.TestGET_Request_PrintFiles();
    }
    public void CreateGFolder()
    {
        StartCoroutine(_googleIntegrator.CreateGFolderRequest());
    }
    public void CreateGSheet()
    {//new
        _googleIntegrator.TestPOST_Request_CreateSpreadSheet();
    }
    public void TestReturnAllRootFolderFiles()
    {
        _googleIntegrator.ReturnAllFilesInRootFolder();
    }
    
    public void TestReadCellFromSheet()
    {
        _googleSheetIntegrator.TestReadCell();
    }
    public void TestReadCellsInRangeFromSheet()
    {
        _googleSheetIntegrator.TestReadMultipleCellsRange();
    }
    public void TestReadNumberCellFromSheet()
    {
        _googleSheetIntegrator.TestReadCellNumber();
    }
    public void TestReadMultipleCellsRange()
    {
        _googleSheetIntegrator.TestReadMultipleCellsBatch();
    }


    public void WriteToCellTestPUT()
    {
        _googleIntegrator.TestPUT_Request_UpdateSpreadSheetValue();
    }
    public void WriteToCell()
    {
        _googleSheetIntegrator.TestWriteValueToCell();
    }
    public void WriteToCellRange()
    {
        _googleSheetIntegrator.TestWriteValueToRange();
    }
    public void WriteToCellBatch()
    {
        _googleSheetIntegrator.TestWriteValueBatch();
    }
    
}
