using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public enum ApiFunctionCAllName
{
    SignUp,
    SignIn,
    GetSupjects,
    GetSupjectData,
	GetSimulationData,
	ChangePassword,
	whatsnew,
	simulationReport,
	getGrades,
	sendCodeForgetPassword,
	resetPassword
}

public enum ApiType
{
    Get,
    Post
}
public static class Server
{
    public static string domain = "https://tachyhealth-ar-api.azurewebsites.net/api/";
}
public class ApiController : MonoBehaviour {
    


    public delegate void ApiResponse(string response);
    public static event ApiResponse OnSignUpResponse;
    public static event ApiResponse OnSignInResponse;
    public static event ApiResponse OnGetSupjectsResponse;
	public static event ApiResponse OnGetSupjectDataResponse;
	public static event ApiResponse OnGetSimulationDataResponse;
	public static event ApiResponse OnChangePasswordResponse;
	public static event ApiResponse OnChangewhatsNew;
	public static event ApiResponse OnChangeSimulationReport;
	public static event ApiResponse onChangeGrade;
	public static event ApiResponse OnChangeSendCode;
	public static event ApiResponse onChangeResetPAssword;

    private string response="";


    private void Start()
    {

    }
    /*
    public void CALLAPI(string url, ApiType type, ApiFunctionCAllName funcName, Hashtable header = null, Hashtable body = null)
    {
        StartCoroutine(CallAPI(url, type, funcName, header, body));
    }

    IEnumerator CallAPI(string url, ApiType type, ApiFunctionCAllName funcName, Hashtable header , Hashtable body)
    {
        UnityWebRequest www = new UnityWebRequest();

        if (type == ApiType.Post)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            if (body != null)
            {
                foreach (DictionaryEntry item in body)
                {                   
                    form.Add(new MultipartFormDataSection((string)item.Key, (string)item.Value));
                }
            }
            www = UnityWebRequest.Post(Server.domain+url, form);
        }

        else if (type == ApiType.Get)
            www = UnityWebRequest.Get(url);

        if (header != null)
        {
            foreach (DictionaryEntry item in header)
            {
                www.SetRequestHeader((string)item.Key, (string)item.Value);
            }
        }

        yield return www.SendWebRequest();

        if (www.isNetworkError)
            Debug.Log(www.error);

        else
        {
            response = www.downloadHandler.text;
            //Debug.Log( www.GetResponseHeader("").GetType.ToString());
            switch (funcName)
            {
                case ApiFunctionCAllName.SignUp:
                    OnSignUpResponse(response);
                    break;
                case ApiFunctionCAllName.SignIn:
                    OnSignInResponse(response);
                    break;
                case ApiFunctionCAllName.GetSupjects:
                    OnGetSupjectsResponse(response);
                    break;
                case ApiFunctionCAllName.GetSupjectData:
                    OnGetSupjectDataResponse(response);
                    break;
			case ApiFunctionCAllName.GetSimulationData:
				OnGetSimulationDataResponse(response);
				break;
			case ApiFunctionCAllName.ChangePassword:
				OnChangePasswordResponse(response);
				break;
			case ApiFunctionCAllName.whatsnew:
				OnChangewhatsNew(response);
				break;
			case ApiFunctionCAllName.simulationReport:
				OnChangeSimulationReport(response);
				break;
			case ApiFunctionCAllName.getGrades:
				onChangeGrade(response);
				break;
			case ApiFunctionCAllName.sendCodeForgetPassword:
				OnChangeSendCode(response);
				break;
			case ApiFunctionCAllName.resetPassword:
				onChangeResetPAssword(response);
				break;
				default:
                    break;
            }
         }
		www.Dispose ();
    }*/

    //for test
   

}
