using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
/*[System.Serializable]
public class Response
{
    public bool status;
    public string expirationDate;
    public List<Data> data;
}
[System.Serializable]
public class Data
{
    public string name;
    public string type;
    public string url;
    //  public string targetName;
    //public string timeStamp;
}*/
public class downloadController : MonoBehaviour
{

    //[SerializeField]
   // ApiController controllersAccess;
    public GameObject noInternetCanvas;
    public Text downloadPercentage;
    public Text downloadingText;
    public Image downloadBar;
    string response = "";
    UnityWebRequest www;
    bool isDownloading = false;
    UnityWebRequest wwwFileSize, wwwFile;
    float value = 0.0f;
    Response res;
    float test;
    string downloadProgress = "0.0";
    bool checkedInternet = false;
    // Start is called before the first frame update
    void Start()
    {
        print("download page");
        www = new UnityWebRequest();
        //downloadPercentage.text = "0%";
        downloadBar.fillAmount = 0f;
        downloading();
    }
    public void checkConnectivity()
    {

        noInternetCanvas.SetActive(false);
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            noInternetCanvas.SetActive(true);
        }
        else
        {
            Debug.Log("call downloading service after check internet");
           
            downloading();
        }
    }
  
    public void downloading() {

        noInternetCanvas.SetActive(false);
        string response = PlayerPrefs.GetString("THResponse" + PlayerPrefs.GetString("THClientID"));
        StartCoroutine(Downloading(response));
    }
    IEnumerator Downloading(string response)
    {
        print("start downloading fun");
        res = JsonUtility.FromJson<Response>(response);
        for (int i = 0; i < res.data.Count; i++)
        {
            value = ((float)i / (float)res.data.Count) * 100.0f;
            //if file not found ..download it
            if (!System.IO.File.Exists(Application.persistentDataPath + "/" + res.data[i].name))
            {
                PlayerPrefs.SetString(res.data[i].name + "TimeStamp",res.data[i].timeStamp);

                print(res.data[i].name+ " is not found");
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {

                    checkedInternet = true;
                    Debug.Log("Error. Check internet connection!");
                    noInternetCanvas.SetActive(true);
                    yield return null ;
                }
                //checkConnectivity();

                downloadingText.text= "Downloading";
            isDownloading = true;
            wwwFile = UnityWebRequest.Get(res.data[i].url);
            yield return wwwFile.SendWebRequest();
            wwwFileSize = UnityWebRequest.Head(res.data[i].url);
            yield return wwwFileSize.SendWebRequest();
            print("downloaded size : " + wwwFile.downloadedBytes.ToString());
            print("file size : " + wwwFileSize.GetResponseHeader("Content-Length"));
            if (wwwFile.downloadedBytes.ToString() == wwwFileSize.GetResponseHeader("Content-Length"))
                {
                    print("file is downloaded and stored");
                    File.WriteAllBytes(Application.persistentDataPath + "/" + res.data[i].name, wwwFile.downloadHandler.data);
                }
            else
                {
                    i--;
                }
                }
            else
                {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {

                    checkedInternet = true;
                    Debug.Log("Error. Check internet connection!");
                    noInternetCanvas.SetActive(true);
                    yield return null;
                }
                //the file is updated
                print("stored timestamp : " + PlayerPrefs.GetString(res.data[i].name + "TimeStamp"));
                print("server timestamp : " + res.data[i].timeStamp);
                if (PlayerPrefs.GetString(res.data[i].name + "TimeStamp")!= res.data[i].timeStamp) {
                    downloadingText.text = "Downloading";
                    wwwFileSize = UnityWebRequest.Head(res.data[i].url);
                    yield return wwwFileSize.SendWebRequest();
                    isDownloading = true;
                    wwwFile = UnityWebRequest.Get(res.data[i].url);
                    yield return wwwFile.SendWebRequest();

                    if (wwwFile.downloadedBytes.ToString() == wwwFileSize.GetResponseHeader("Content-Length"))
                    {
                        File.WriteAllBytes(Application.persistentDataPath + "/" + res.data[i].name, wwwFile.downloadHandler.data);
                        PlayerPrefs.SetString(res.data[i].name + "TimeStamp", res.data[i].timeStamp);
                    }
                    else
                    {
                        i--;
                    }
                    wwwFile.Dispose();
                    wwwFile = null;
                    wwwFileSize.Dispose();
                    wwwFileSize = null;
                    Resources.UnloadUnusedAssets();
                    Caching.ClearCache();

                }
                print(res.data[i].name + " is found");
                continue;
            }
            print(Application.persistentDataPath);
            wwwFile.Dispose();
            wwwFile = null;
            wwwFileSize.Dispose();
            wwwFileSize = null;
            Resources.UnloadUnusedAssets();
            Caching.ClearCache();

            
        }

        SceneManager.LoadScene("AR Main Scene");
    }
    // Update is called once per frame
    void Update()
    {
        if (isDownloading&& wwwFile!=null)
        {
            test = ((float)(res.data.Count)*1.0f);

            downloadProgress = (value + (((wwwFile.downloadProgress) / test) * 100.0f)).ToString()+".000";

            downloadProgress = downloadProgress.Substring(0,downloadProgress.IndexOf("."));
            downloadPercentage.text = downloadProgress + "%";
            downloadBar.fillAmount =((value/100.0f)+( wwwFile.downloadProgress / test));
          
            if (checkedInternet)
            {
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    checkedInternet = false;
                    noInternetCanvas.SetActive(false);
                }
            }
        }
    }

}
