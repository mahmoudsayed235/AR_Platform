using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;
using System;
public class getDataController : MonoBehaviour
{
    [SerializeField]
    configurationController configurationcontroller;
    public GameObject noInternetCanvas;
    public GameObject accountFrozen;
    string response = "";
    UnityWebRequest www;
    Response res;
    bool checkedInternet = false;
    public string contentApiLink = "https://tachyhealth-ar.azurewebsites.net/api/content/";
    // Start is called before the first frame update
    void Start()
    {
        print("getData page");
        checkConnectivity();

    }
    public void checkConnectivity()
    {
        //for testing
       // PlayerPrefs.SetString("THExpirationDate", "ASD");

        noInternetCanvas.SetActive(false);
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            //first time to run the app so you need to call service
            if (PlayerPrefs.GetString("THExpirationDate")=="-1")
            {
                checkedInternet = true;
                noInternetCanvas.SetActive(true);
            }
            else
            {
                parseResponse(PlayerPrefs.GetString("THResponse" + PlayerPrefs.GetString("THClientID")));

            }
        }
        else
        {
            CALLAPI(contentApiLink + PlayerPrefs.GetString("THClientID"));
            Debug.Log("call service");
            Debug.Log(contentApiLink + PlayerPrefs.GetString("THClientID"));

        }
    }
    public void CALLAPI(string url)
    {
        StartCoroutine(CallAPI(url));
    }
    IEnumerator CallAPI(string url)
    {
        www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
            Debug.Log(www.error);

        else
        {
            print(www.downloadHandler.text);
            response = www.downloadHandler.text;
            parseResponse(response);
        }
        www.Dispose();
    }
    void parseResponse(string response)
    {
        print(response);
        //for testing dar el oyoun
       // response= "{ \"status\": true, \"expirationDate\": \"2019 / 12 / 12 00:00:00\", \"os\": \"Android\", \"data\": [ { \"name\": \"eyecomponenetsarsceneandroid\", \"triggerName\": \"trigger1\", \"url\": \"https://tachyhealth-ar-api.azurewebsites.net/AR-Content71/interactivesceneeyeenglishandroid\", \"timeStamp\": \"201906040000000000\", \"type\": \"scene\" }, { \"name\": \"glaucomaarsceneandroid\", \"triggerName\": \"trigger2\", \"url\": \"https://tachyhealth-ar-api.azurewebsites.net/AR-Content71/eyearandroid\", \"timeStamp\": \"201906030000000000\", \"type\": \"scene\" }, { \"name\": \"interactiveeyesceneandroid\", \"triggerName\": \"trigger3\", \"url\": \"https://tachyhealth-ar-api.azurewebsites.net/AR-Content71/lasik360.mp4\", \"timeStamp\": \"201906020000000000\", \"type\": \"scene\" }, { \"name\": \"interactiveorofacialsceneandroid\", \"triggerName\": \"trigger4\", \"url\": \"https://tachyhealth-ar-api.azurewebsites.net/AR-Content71/interactivesceneeyeenglishandroid\", \"timeStamp\": \"201906040000000000\", \"type\": \"scene\" }, { \"name\": \"motilityarsceneandroid\", \"triggerName\": \"trigger5\", \"url\": \"https://tachyhealth-ar-api.azurewebsites.net/AR-Content71/eyearandroid\", \"timeStamp\": \"201906030000000000\", \"type\": \"scene\" }, { \"name\": \"orofacialarsceneandroid\", \"triggerName\": \"trigger6\", \"url\": \"https://tachyhealth-ar-api.azurewebsites.net/AR-Content71/lasik360.mp4\", \"timeStamp\": \"201906020000000000\", \"type\": \"scene\" } ] }";
        res = JsonUtility.FromJson<Response>(response);
        print("parsed : "+ response);
        if (res.status)
        {
            PlayerPrefs.SetString("THResponse" + PlayerPrefs.GetString("THClientID"), response);

            //BEFORE download we should check if the expiration date is expired or not
           
            //if expired accountfrozen
            print("expired : " + isExpired(res.expirationDate));
            if (isExpired(res.expirationDate)) {
                accountFrozen.SetActive(true);
                return;
            }
            else
            {
                configurationcontroller.showDownloading();
            }
        }
        else
        {
            accountFrozen.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (checkedInternet)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                checkedInternet = false;
                noInternetCanvas.SetActive(false);
            }
        }
    }
    bool isExpired(string expirationDate)
    {
        var over = DateTime.Parse(expirationDate);
        var dateAndTimeVar = System.DateTime.Now;
        print("over: " + over);
        print("dateAndTimeVar: " + dateAndTimeVar);
        print(DateTime.Parse(dateAndTimeVar.ToString()) >= DateTime.Parse(over.ToString()));
        if (DateTime.Parse(dateAndTimeVar.ToString()) >= DateTime.Parse(over.ToString()))
        {
            return true;
        }else
        {
            return false;
        }
    }
}
