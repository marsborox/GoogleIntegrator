using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
public class GoogleSheetIntegrator : MonoBehaviour/*Singleton<GoogleSheetIntegrator>*/
{
    private string _sheetIWorkOnID = "1SmJWxu3HvLDCB8zqdm6tpJlyfptM5u1MnotedPnOpp8";//spreadSheetID
    GoogleIntegrator googleIntegrator;

    [System.Serializable]
    public class GoogleSheetContentJSON
    {
        public string range;
        public string majorDimension;
        public string[][] values;

    }
    /*public class GoogleSheetBatchWriteJSON
    {
        public string[][] values;
    }*/
    class GoogleSheetBatchWriteJSON
    {
        //public string valueInputOption;
        public List<ValueRangePair> data;
    }
    private class ValueRangePair
    {
        public string range;
        public string[][] values;
        
        //values[0] = new string[1];//temp shut
        public ValueRangePair(string inputRange,string inputValue)
        {
            range = inputRange;
            values = new string [1][];
            values[0] = new string[1];
            values[0][0] = inputValue;
        }
    }
    private List<ValueRangePair>_valueRangePairList = new List<ValueRangePair>();
    void Start()
    {
        googleIntegrator = GetComponent<GoogleIntegrator>();
    }

    public void TestReadCell()
    {//any walue as string
        string spreadsheetId = _sheetIWorkOnID;

        // Example:
        // Sheet1!A1
        string range = "Sheet1!A2";

        string url = "https://sheets.googleapis.com/v4/spreadsheets/"+spreadsheetId+"/values/"+range;

        StartCoroutine(googleIntegrator.SendGET_Request(url, PrintResult));

        void PrintResult(UnityWebRequest request)
        {   //we ask for one val only
            string json = request.downloadHandler.text;
            // raw json available here

            Debug.Log(json);
            GoogleSheetContentJSON returnJSON = JsonConvert.DeserializeObject<GoogleSheetContentJSON>(json);
            Debug.Log(returnJSON.range);
            Debug.Log(returnJSON.majorDimension);
            // array [i,j]
            /*for(int i = 0 ; i < returnJSON.values.Length;i++)
            {
                for(int j = 0; j < returnJSON.values[i].Length;j++)
                {
                    Debug.Log(returnJSON.values[i][j]);
                }
            }*/
            Debug.Log(returnJSON.values[0][0]);
        }
    }
    
    public void TestReadMultipleCellsRange()
    {
        string spreadsheetId = _sheetIWorkOnID;

        // Example:
        // Sheet1!A1
        string range = "Sheet1!A1:D20";

        string url = "https://sheets.googleapis.com/v4/spreadsheets/"+spreadsheetId+"/values/"+range;

        StartCoroutine(googleIntegrator.SendGET_Request(url, PrintResult));

        void PrintResult(UnityWebRequest request)
        {
            string json = request.downloadHandler.text;

            Debug.Log(json);
      
            GoogleSheetContentJSON returnJSON = JsonConvert.DeserializeObject<GoogleSheetContentJSON>(json);

            Debug.Log(returnJSON.range);
            Debug.Log(returnJSON.majorDimension);
            // array [i,j]
            // this will write every thing in array separately as 
            for(int i = 0 ; i < returnJSON.values.Length;i++)
            {
                for(int j = 0; j < returnJSON.values[i].Length;j++)
                {
                    Debug.Log(returnJSON.values[i][j]);
                }
            }
        }
    }
    public void TestReadCellNumber()
    {
        string spreadsheetId = _sheetIWorkOnID;

        // Example:
        // Sheet1!A1
        string range = "Sheet1!J10";

        string url = "https://sheets.googleapis.com/v4/spreadsheets/"+spreadsheetId+"/values/"+range;

        StartCoroutine(googleIntegrator.SendGET_Request(url, PrintResult));

        void PrintResult(UnityWebRequest request)
        {   //we ask for one val only
            string json = request.downloadHandler.text;
            // raw json available here

            Debug.Log(json);
            GoogleSheetContentJSON returnJSON = JsonConvert.DeserializeObject<GoogleSheetContentJSON>(json);
            Debug.Log(returnJSON.range);
            Debug.Log(returnJSON.majorDimension);
            // array [i,j]
            /*for(int i = 0 ; i < returnJSON.values.Length;i++)
            {
                for(int j = 0; j < returnJSON.values[i].Length;j++)
                {
                    Debug.Log(returnJSON.values[i][j]);
                }
            }*/
            //int returnNumber = returnJSON.values[0][0].TryParse(int);
            Debug.Log(returnJSON.values[0][0]);
            int number = int.Parse(returnJSON.values[0][0]);
            Debug.Log(number);
        }
    }
    public void TestReadMultipleCellsBatch()
    {
        string spreadsheetId = _sheetIWorkOnID;

        //we want cells A1 A2 C4 the string for each cell must be ranges:<CellCoordinate>
        string cell1 = "Sheet1!A1";
        string cell2 = "Sheet1!A2";
        string cell3 = "Sheet1!C4";


        // resulting url https://sheets.googleapis.com/v4/spreadsheets/SPREADSHEET_ID/values:batchGet?ranges=Sheet1!A1&ranges=Sheet1!A2&ranges=Sheet1!C4

        string url = "https://sheets.googleapis.com/v4/spreadsheets/"+spreadsheetId+"/values:batchGet?"+"ranges="+cell1+"&ranges="+cell2+"&ranges="+cell3;

        StartCoroutine(googleIntegrator.SendGET_Request(url, PrintResult));

        void PrintResult(UnityWebRequest request)
        {
            string json = request.downloadHandler.text;

            Debug.Log(json);
      
            //GoogleSheetContentJSON returnJSON = JsonConvert.DeserializeObject<GoogleSheetContentJSON>(json);

            //analysejson
        }
    }

    public void TestWriteValueToCell()
    {
        string spreadsheetId = "1SmJWxu3HvLDCB8zqdm6tpJlyfptM5u1MnotedPnOpp8";//spreadSheetID
        string range = "Sheet1!F15";//where to write
        string value = "testButBetter";//what to write
        //string url = $"https://sheets.googleapis.com/v4/spreadsheets/{spreadsheetId}/values/{range}?valueInputOption=RAW";

        string url = "https://sheets.googleapis.com/v4/spreadsheets/"+spreadsheetId+"/values/"+range+"?valueInputOption=RAW";
        
        string jsonBody;

        GoogleSheetContentJSON jsonContent = new GoogleSheetContentJSON();
        jsonContent.range = range;
        jsonContent.majorDimension = "ROWS";
        //will create the array 
        jsonContent.values = new string[1][];
        jsonContent.values[0] = new string [1];
        jsonContent.values[0][0] = value;
        
        jsonBody = JsonConvert.SerializeObject(jsonContent);
        StartCoroutine(googleIntegrator.SendPUT_Request(url, jsonBody, PrintResult));

        void PrintResult(UnityWebRequest request)
        {
            string json = request.downloadHandler.text;

            Debug.Log(json);
        }
    }

    public void TestWriteValueToRange()
    {
        string spreadsheetId = "1SmJWxu3HvLDCB8zqdm6tpJlyfptM5u1MnotedPnOpp8";//spreadSheetID
        string range = "Sheet1!H1:J2";//where to write
        
        //string value = "testButBetter";//what to write
        
        string url = "https://sheets.googleapis.com/v4/spreadsheets/"+spreadsheetId+"/values/"+range+"?valueInputOption=RAW";
        
        string jsonBody;

        GoogleSheetContentJSON jsonContent = new GoogleSheetContentJSON();
        jsonContent.range = range;
        jsonContent.majorDimension = "ROWS";
        //will create the array 
        jsonContent.values = new string[2][];
                
        jsonContent.values[0]=new []{"qqq","aaa","www"};
        jsonContent.values[1]=new []{"sss","ddd","eee"};

        jsonBody = JsonConvert.SerializeObject(jsonContent);
        //Debug.Log(jsonBody);
        StartCoroutine(googleIntegrator.SendPUT_Request(url, jsonBody, PrintResult));

        void PrintResult(UnityWebRequest request)
        {
            string json = request.downloadHandler.text;

            Debug.Log(json);
        }
    }
    public void TestWriteValueBatch()
    {

        string url ="https://sheets.googleapis.com/v4/spreadsheets/"+ _sheetIWorkOnID+ "/values:batchUpdate?valueInputOption=RAW";
        string jsonBody;
        GoogleSheetBatchWriteJSON jsonContent = new GoogleSheetBatchWriteJSON();
        jsonContent.data= CreateTest_ValuePairList();
        
        
        /*jsonContent.data = new string[testValueRangePairList.Count][];
        
        for(int i = 0; i<testValueRangePairList.Count;i++)
        {
            jsonContent.values[i]= new string [2];
            jsonContent.values[i][0]=testValueRangePairList[i].range;
            //jsonContent.values[i][1]=testValueRangePairList[i].values;  //temp shut    
            // ----------------------- ALL WORKS EXCEPT WE MSUT FIX JSON ASSEMBLY  
        }*/
        jsonBody = JsonConvert.SerializeObject(jsonContent);
        Debug.Log("sending JSON");
        Debug.Log(jsonBody);
        StartCoroutine(googleIntegrator.SendPOST_Request(url, jsonBody, PrintResult));

        void PrintResult(UnityWebRequest request)
        {
            string json = request.downloadHandler.text;

            Debug.Log(json);
        }

        static List<ValueRangePair> CreateTest_ValuePairList()
        {
            List<ValueRangePair> returnList = new List<ValueRangePair>();
            ValueRangePair pair1 = new ValueRangePair("Sheet1!H4","aaa");
            ValueRangePair pair2 = new ValueRangePair("Sheet1!I5","sss");
            ValueRangePair pair3 = new ValueRangePair("Sheet1!J6","ddd");
            returnList.Add(pair1);
            returnList.Add(pair2);
            returnList.Add(pair3);
            /*returnList.Add(new ValueRangePair("aaa","Sheet1!H4"));
            returnList.Add(new ValueRangePair("sss","Sheet1!I5"));
            returnList.Add(new ValueRangePair("aaa","Sheet1!H4"));*/
            return returnList;
        }   
    }
}