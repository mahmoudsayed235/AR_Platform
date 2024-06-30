using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class configurationController : MonoBehaviour
{

    public string clientID = "";
    public GameObject splash;
    public GameObject onboarding;
    public GameObject getdata;
    public GameObject downloading;
    void Awake()
    {
        print(PlayerPrefs.GetString("THFirstTime"));
        if (PlayerPrefs.GetString("THFirstTime") != "yes")
        {
            PlayerPrefs.SetString("THExpirationDate", "-1");
        }
        PlayerPrefs.SetString("THClientID",clientID);   
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Start()
    {
        splash.SetActive(true);

        Invoke("showOnboarding", 3f);
        //setFirstTimeForTest();
    }
    void setFirstTimeForTest() {
        PlayerPrefs.SetString("THFirstTime", "no");
    }
    void showOnboarding() {
        if (PlayerPrefs.GetString("THFirstTime") != "yes")
        {
            PlayerPrefs.SetString("THFirstTime", "yes");
            onboarding.SetActive(true);
        }
        else
        {
            getdata.SetActive(true);
        }
        splash.SetActive(false);
    }
    public void showGetData() {
        getdata.SetActive(true);
        onboarding.SetActive(false);
    }
    public void showDownloading() {
        getdata.SetActive(false);
        downloading.SetActive(true);
    }
    public void exit() {
        Application.Quit();
    }
}
