using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityGoogleDrive;
using Newtonsoft.Json;
public class GoogleIntegrator : Singleton<GoogleIntegrator>
{

    public static new GoogleIntegrator instance => Singleton<GoogleIntegrator>.instance;
    public GoogleSheetIntegrator googleSheetIntegrator;
    
    public Texture2D testImage;

    public RawImage finalDownloaded;
    private string _rootFolderID ="1gHxu4hGOch4KpwqYBqreXjG2ePzPtyHf";
    //private string _sheetIWorkOnID = "1SmJWxu3HvLDCB8zqdm6tpJlyfptM5u1MnotedPnOpp8";
    private string _filesInMyFolderJSON;
    public byte[] downloadedContent;

    [System.Serializable]
    public class GoogleDriveFile
    {
        public string name;
        public string id;
    }

    [System.Serializable]
    public class GoogleDriveFileList
    {
        public GoogleDriveFile[] files;
    }
    public class FolderRequestData
    {
        public string name;
        public string mimeType;
        public string [] parents;
    }
    public class FolderJSON
    {
        public string kind;
        public string id;
        public string name;
        public string mimeType;
    }
    public class SheetJSON
    {
        public string kind;
        public string id;
        public string name;
        public string mimeType;
    }

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        googleSheetIntegrator = GetComponent<GoogleSheetIntegrator>();
    }

/* ------------------------------------------------------- */
    public void ReturnAllFilesInRootFolder()
    {
        string url = "https://www.googleapis.com/drive/v3/files"+"?q='"+_rootFolderID+
        "'+in+parents+and+trashed=false"+"&fields=files(id,name,modifiedTime)";

        StartCoroutine(SendGET_Request(url,PrintFiles));

        void PrintFiles(UnityWebRequest request)
        {
            string json = request.downloadHandler.text;
            _filesInMyFolderJSON=json;
            Debug.Log(json);//will print json content into console
            //we will prob pass it to some public variable 
            // or process json here
        }
    }
    public void TestGET_Request_PrintFiles()
    {//print files
        //Debug.Log("testing files");
        string url = "https://www.googleapis.com/drive/v3/files";
        StartCoroutine(SendGET_Request(url,PrintFiles));

        void PrintFiles(UnityWebRequest request)
        {
            string json = request.downloadHandler.text;
            //Debug.Log("printing json");
            //Debug.Log(json);//will print json content into console

            //this will parse json
            GoogleDriveFileList fileList = JsonUtility.FromJson<GoogleDriveFileList>(json);
            if (fileList?.files != null)
            {
                foreach (GoogleDriveFile file in fileList.files)
                {//print each filename into console
                    Debug.Log(file.name);
                }
            }
            else
            {
                Debug.LogError("Failed to parse file list from Google Drive response.");
            }
        }
    }
    public void TestPOST_Request_CreateSpreadSheet()
    {
        Debug.Log("testing POST request");
        string url = "https://www.googleapis.com/drive/v3/files";
        //create JSON
        string sheetName = "secondSpreadsheet";
        //create request w MIME type / Data we will send
        //application/vnd.google-apps.folder  in this case - MIME type
        FolderRequestData folderRequestData = new FolderRequestData
        {
            name = sheetName,
            mimeType = "application/vnd.google-apps.spreadsheet",
            parents = new string[]{_rootFolderID}//set Parent
        };
        // Convert object to JSON
        string jsonBody = JsonUtility.ToJson(folderRequestData);
        //Debug.Log("sending JSON: "+jsonBody);
        StartCoroutine(SendPOST_Request(url, jsonBody,DisplayReturnInfo));

        void DisplayReturnInfo(UnityWebRequest request)
        {
            //tempshutdown
            string json = request.downloadHandler.text;
            //Debug.Log(json);
            SheetJSON createdSheet = JsonUtility.FromJson<SheetJSON>(json);
            Debug.Log("create folder name: "+createdSheet.name+" id: "+createdSheet.id+" of type: "+createdSheet.mimeType);
        }
    }
    public void TestPUT_Request_UpdateSpreadSheetValue()
    {
        string spreadsheetId = "1SmJWxu3HvLDCB8zqdm6tpJlyfptM5u1MnotedPnOpp8";//spreadSheetID
        string range = "Sheet1!F15";

        //string url = $"https://sheets.googleapis.com/v4/spreadsheets/{spreadsheetId}/values/{range}?valueInputOption=RAW";

        string url = "https://sheets.googleapis.com/v4/spreadsheets/"+spreadsheetId+"/values/"+range+"?valueInputOption=RAW";
        string jsonBody =
        @"{
            ""range"": ""Sheet1!F15"",
            ""majorDimension"": ""ROWS"",
            ""values"": 
            [[""test123""]]
        }";
        //string jsonBody =;
        Debug.Log(jsonBody);

        StartCoroutine(SendPUT_Request(url, jsonBody, PrintResult));

        void PrintResult(UnityWebRequest request)
        {
            string json = request.downloadHandler.text;

            Debug.Log(json);
        }
    }
    public IEnumerator SendGET_Request(string url, Action<UnityWebRequest> method)
    {
         yield return SendWebRequest(
            () => UnityWebRequest.Get(url),
            request =>
            {
                method(request);
            },
            error => Debug.LogError(error)
        );
    }
    public IEnumerator SendPUT_Request(string url, string jsonBody,Action<UnityWebRequest> method)
    {
         yield return SendWebRequest(
            () => UnityWebRequest.Put(url,jsonBody),
            request =>
            {
                method(request);
            },
            error => Debug.LogError(error)
        );
    }
    public IEnumerator SendPOST_Request(string url, string jsonBody, Action<UnityWebRequest>method)
    {//POST request

        //yield return SendGoogleDrivePOSTRequest_wJSON(
        /*Debug.LogError("SendingJSON");
        Debug.Log("url: "+url);
        Debug.Log("json: jsonBody: "+jsonBody);*/
        yield return SendWebRequest(    
            () => UnityWebRequest.Post(url,jsonBody,"application/json"),//not sure if this is correct
            request =>
            {
                method(request);
            },
            error => Debug.LogError(error)
        );
    }
    private IEnumerator SendWebRequest(Func<UnityWebRequest> requestFactory, Action<UnityWebRequest> onSuccess, Action<string> onError)
    {
        bool alreadyRetried = false;

        while (true)
        {
            UnityWebRequest request = requestFactory();
            request.SetRequestHeader("Authorization", "Bearer " + AuthController.AccessToken);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request);
                request.Dispose();
                yield break;
            }

            if (!alreadyRetried && request.responseCode == 401)
            {
                alreadyRetried = true;
                bool refreshDone = false;
                bool refreshSuccess = false;

                void HandleAccessTokenRefreshed(bool success)
                {
                    refreshSuccess = success;
                    refreshDone = true;
                    AuthController.OnAccessTokenRefreshed -= HandleAccessTokenRefreshed;
                }

                AuthController.OnAccessTokenRefreshed += HandleAccessTokenRefreshed;
                AuthController.RefreshAccessToken();

                while (!refreshDone)
                {
                    yield return null;
                }

                request.Dispose();

                if (refreshSuccess)
                {
                    continue;
                }

                onError?.Invoke("Authorization failed.");
                yield break;
            }

            string error = request.error;
            if (string.IsNullOrEmpty(error))
            {
                error = request.downloadHandler?.text;
            }

            request.Dispose();
            onError?.Invoke(string.IsNullOrEmpty(error) ? "Google Drive request failed." : error);
            yield break;
        }
    }

    #region Simple methods using Package only
    /*public void TestGoogleDrive()
    {
        StartCoroutine(TestGoogleDriveRequest());
    }
    public void DisplayGFilesWithRefresh()
    {
        StartCoroutine(DisplayGFilesRequestWithRefreshRequest());
    }
    
    public void SendImage()
    {
        StartCoroutine(SendImageRequest());
    }

    public void DownloadImage()
    {
        StartCoroutine(DownloadImageRequest());
    }
    public void SendTextFile()
    {
        StartCoroutine(SendTxtRequest());
    }
    public void DownloadTextFile()
    {
        StartCoroutine(DownloadTxtRequest());
    }*/
    public IEnumerator SendImageRequest()
    {
        var content = testImage.EncodeToPNG();
        var file = new UnityGoogleDrive.Data.File() {Name = "TestImageUpload", Content = content};
        var request = GoogleDriveFiles.Create(file);
        request.Fields = new List <string> {"id"};
        yield return request.Send();
        print(request.IsError);
        print(request.ResponseData.Content);
        print(request.ResponseData.Id);
    }
    public IEnumerator DownloadImageRequest()
    {
        var request = GoogleDriveFiles.Download("1NpGVQ2cla6rjf5GH2SbeMy0LexhmWdYg");//string is the fileID
        yield return request.Send();
        print(request.IsError);
        print(request.ResponseData.Content);
        downloadedContent = request.ResponseData.Content;
        Texture2D tex = new Texture2D(1024,1024);//prob resolution of img goes here
        tex.LoadImage(downloadedContent);
        tex.Apply();
        finalDownloaded.texture = tex;
    }
    public IEnumerator SendTxtRequest()
    {
        var content = Encoding.ASCII.GetBytes("THIS IS MY TEXT");//text that will be in file
        var file = new UnityGoogleDrive.Data.File() {Name = "Test Text Upload.txt", Content = content};//format of file has to be here
        var request = GoogleDriveFiles.Create(file);
        request.Fields = new List <string> {"id"};
        yield return request.Send();
        print(request.IsError);
        print(request.ResponseData.Content);
        print(request.ResponseData.Id);
    }
    public IEnumerator DownloadTxtRequest()
    {
        var request = GoogleDriveFiles.Download("1sEjGTX4jiDOxZOgfw2rd2nW84F5svEbu");//string is the fileID
        yield return request.Send();
        print(request.IsError);
        print(request.ResponseData.Content);
        downloadedContent = request.ResponseData.Content;

        string txt = Encoding.ASCII.GetString(downloadedContent);//get text in file to string
        string textSaver = Application.dataPath + "/Test.txt";//save file
        //if in unity editor file is saved in assets, if its compiled, then ../whereverExeIsSaved/nameOfApp_Data/*here*
        //we can use Application.persistentPath if we want to specify where - must check

        if(File.Exists(textSaver))
        {//if file exists on drive delete it
            File.Delete(textSaver);
        }
        //save text to file
        File.AppendAllText(textSaver, txt + "\n");
    }
    #endregion
    #region google GET / POST request no authBroken
    public IEnumerator TestGoogleDriveRequest()
    {//send and receive some API request to googledocs
        string url = "https://www.googleapis.com/drive/v3/files";

        UnityWebRequest request = UnityWebRequest.Get(url);//this creates request

        request.SetRequestHeader("Authorization", "Bearer " + AuthController.AccessToken);//adds OAuth token token is string

        yield return request.SendWebRequest(); //sends request
        //prob here some methods to do
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            //Debug.LogError(request.downloadHandler.text);//response
        }
        else
        {
            Debug.Log("WebRequest success");
            //Debug.Log(request.downloadHandler.text);
        }
    }
    public IEnumerator DisplayGFilesRequest()
    {
        string url = "https://www.googleapis.com/drive/v3/files";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", "Bearer " + AuthController.AccessToken);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            Debug.LogError(request.downloadHandler.text);
        }
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log(json);
            // Convert JSON into C# object
            GoogleDriveFileList fileList = JsonUtility.FromJson<GoogleDriveFileList>(json);
            // Print each file name separately
            foreach (GoogleDriveFile file in fileList.files)
            {
                Debug.Log(file.name);
            }
        }
    }
    #endregion

    #region nonUniversal send request methods
    public IEnumerator DisplayGFilesRequestWithRefreshRequest()
    {//Get request
        string url = "https://www.googleapis.com/drive/v3/files";

        yield return SendWebRequest(
            () => UnityWebRequest.Get(url),
            request =>
            {
                string json = request.downloadHandler.text;
                //Debug.Log("printing json");
                //Debug.Log(json);//will print json content into console

                //this will parse json
                GoogleDriveFileList fileList = JsonUtility.FromJson<GoogleDriveFileList>(json);
                if (fileList?.files != null)
                {
                    foreach (GoogleDriveFile file in fileList.files)
                    {//print each filename into console
                        Debug.Log(file.name);
                    }
                }
                else
                {
                    Debug.LogError("Failed to parse file list from Google Drive response.");
                }
            },
            error => Debug.LogError(error)
        );
    }
    public IEnumerator CreateGSheetRequest()
    {//POST request
        string sheetName = "AAA sheetByApp";
        string url = "https://www.googleapis.com/drive/v3/files";

        //create request w MIME type / Data we will send
        //application/vnd.google-apps.folder  in this case - MIME type
        FolderRequestData folderRequestData = new FolderRequestData
        {
            name = sheetName,
            mimeType = "application/vnd.google-apps.spreadsheet",
            parents = new string[]{_rootFolderID}//set Parent
        };
        // Convert object to JSON
        string jsonBody = JsonUtility.ToJson(folderRequestData);
        //yield return SendGoogleDrivePOSTRequest_wJSON(
        yield return SendWebRequest(    
            () => UnityWebRequest.Post(url,jsonBody,"application/json"),//not sure if this is correct
            request =>
            {
                string json = request.downloadHandler.text;
                //Debug.Log(json);
                SheetJSON createdSheet = JsonUtility.FromJson<SheetJSON>(json);
                Debug.Log("create folder name: "+createdSheet.name+" id: "+createdSheet.id+" of type: "+createdSheet.mimeType);    
            },
            /*jsonBody,*/
            error => Debug.LogError(error)
        );
    }
    
    public IEnumerator CreateGFolderRequest()
    {//POST request
        string folderName = "AaSsDdFf";

        string url = "https://www.googleapis.com/drive/v3/files";

        //create request w MIME type / Data we will send
        //application/vnd.google-apps.folder  in this case - MIME type
        FolderRequestData folderRequestData = new FolderRequestData
        {
            name = folderName,
            mimeType = "application/vnd.google-apps.folder"
        };
        // Convert object to JSON
        string jsonBody = JsonUtility.ToJson(folderRequestData);
        //yield return SendGoogleDrivePOSTRequest_wJSON(
        yield return SendWebRequest(    
            () => UnityWebRequest.Post(url,jsonBody,"application/json"),//not sure if this is correct
            request =>
            {
                string json = request.downloadHandler.text;
                //Debug.Log(json);
                FolderJSON createdFolder = JsonUtility.FromJson<FolderJSON>(json);
                Debug.Log("create folder name: "+createdFolder.name+" id: "+createdFolder.id+" of type: "+createdFolder.mimeType);
                //_rootFolderID = createdFolder.id; //---------------temporarily disabled    
            },
            /*jsonBody,*/
            error => Debug.LogError(error)
        );
    }
    #endregion
}

    /*private IEnumerator SendGoogleDrivePOSTRequest_wJSON(Func<UnityWebRequest> requestFactory, Action<UnityWebRequest> onSuccess, 
    string jsonBody,Action<string> onError)
    {
        bool alreadyRetried = false;

        while (true)
        {
            //set request
            UnityWebRequest request = requestFactory();


            //not sure if i need this
            //byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            //request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            //request.downloadHandler = new DownloadHandlerBuffer();

            //Headers
            //request.SetRequestHeader("Content-Type", "application/json");//this header is created elsewhere so prob not needed
            request.SetRequestHeader("Authorization", "Bearer " + AuthController.AccessToken);
            
            yield return request.SendWebRequest();

            //act upon sucess or unsucess w attempt to reauth
            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request);
                request.Dispose();
                yield break;
            }

            if (!alreadyRetried && request.responseCode == 401)
            {
                alreadyRetried = true;
                bool refreshDone = false;
                bool refreshSuccess = false;

                void HandleAccessTokenRefreshed(bool success)
                {
                    refreshSuccess = success;
                    refreshDone = true;
                    AuthController.OnAccessTokenRefreshed -= HandleAccessTokenRefreshed;
                }

                AuthController.OnAccessTokenRefreshed += HandleAccessTokenRefreshed;
                AuthController.RefreshAccessToken();

                while (!refreshDone)
                {
                    yield return null;
                }

                request.Dispose();

                if (refreshSuccess)
                {
                    continue;
                }

                onError?.Invoke("Authorization failed.");
                yield break;
            }

            string error = request.error;
            if (string.IsNullOrEmpty(error))
            {
                error = request.downloadHandler?.text;
            }

            request.Dispose();
            onError?.Invoke(string.IsNullOrEmpty(error) ? "Google Drive request failed." : error);
            yield break;
        }
    }*/
