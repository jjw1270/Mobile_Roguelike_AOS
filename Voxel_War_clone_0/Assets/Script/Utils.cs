using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Reflection;
using System;
using UnityEngine.UI;
public partial class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeButtonToUtils()
    {
        UIManager.instance.InitButton();

        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "GetServerTime", new List<string>() { }, GetServerTime);
        UIManager.instance.SetFunctionButton(i++, "GetServerStatus", new List<string>() { }, GetServerStatus);
        UIManager.instance.SetFunctionButton(i++, "GetLatestVersion", new List<string>() { }, GetLatestVersion);
        UIManager.instance.SetFunctionButton(i++, "GetGoogleHash", new List<string>() { }, GetGoogleHash);

    }

    void GetServerTime(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Utils.GetServerTime();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("현재 시간(미국) : " + result.GetReturnValuetoJSON()["utcTime"].ToString());


        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Utils.GetServerTime(result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("현재 시간(미국) : " + result.GetReturnValuetoJSON()["utcTime"].ToString());

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Utils.GetServerTime, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("현재 시간(미국) : " + result.GetReturnValuetoJSON()["utcTime"].ToString());
            });
        }
    }

    void GetServerStatus(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Utils.GetServerStatus();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("프로젝트 상태 : " + result.GetReturnValuetoJSON()["serverStatus"].ToString());


        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Utils.GetServerStatus(result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("프로젝트 상태 : " + result.GetReturnValuetoJSON()["serverStatus"].ToString());

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Utils.GetServerStatus, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("프로젝트 상태 : " + result.GetReturnValuetoJSON()["serverStatus"].ToString());
            });
        }
    }

    void GetLatestVersion(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Utils.GetLatestVersion();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log(string.Format("프로젝트 버전:{0} / 업데이트 여부 : {1} \n", result.GetReturnValuetoJSON()["version"].ToString(), result.GetReturnValuetoJSON()["type"].ToString()));


        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Utils.GetLatestVersion(result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log(string.Format("프로젝트 버전:{0} / 업데이트 여부 : {1} \n", result.GetReturnValuetoJSON()["version"].ToString(), result.GetReturnValuetoJSON()["type"].ToString()));

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Utils.GetLatestVersion, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log(string.Format("프로젝트 버전:{0} / 업데이트 여부 : {1} \n", result.GetReturnValuetoJSON()["version"].ToString(), result.GetReturnValuetoJSON()["type"].ToString()));
            });
        }
    }

    void GetGoogleHash(InputField[] inputFields)
    {
        string googlehash = Backend.Utils.GetGoogleHash();

        Debug.Log("구글 해시 키 : " + googlehash);
    }

}
