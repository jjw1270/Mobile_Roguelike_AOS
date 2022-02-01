using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Reflection;
using UnityEngine.UI;

public partial class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void ChangeButtonToGameLog()
    {
        UIManager.instance.InitButton();
        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "InsertLog", new List<string>() { "string logType", "string columnName", "string columnData", "string columnName2", "int columnData" }, InsertLog);
        UIManager.instance.SetFunctionButton(i++, "GetLog", new List<string>() { "string logType", "string limit" }, GetLog);

    }

    void InsertLog(InputField[] inputFields)
    {
        Param param = new Param();
        param.Add(inputFields[1].text, inputFields[2].text);
        param.Add(inputFields[3].text, System.Int32.Parse(inputFields[4].text));

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameLog.InsertLog(inputFields[0].text, param);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameLog.InsertLog(inputFields[0].text, param, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");


            });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameLog.InsertLog, inputFields[0].text, param, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            });
        }
    }

    void GetLog(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string logType = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameLog.GetLog(logType, Int32.Parse(inputFields[1].text));

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("최신 로그의 inDate : " + result.Rows()[0]["inDate"]["S"].ToString());

            if (result.IsSuccess())
            {
                if (result.HasFirstKey())
                {
                    var result2 = Backend.GameLog.GetLog(logType, 10, result.FirstKeystring());
                    Debug.Log($"firstKey를 이용한 ({backendType.ToString()}){methodName} : {result2}");
                }
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameLog.GetLog(logType, Int32.Parse(inputFields[1].text), result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("최신 로그의 inDate : " + result.Rows()[0]["inDate"]["S"].ToString());

                if (result.IsSuccess())
                {
                    if (result.HasFirstKey())
                    {
                        var result2 = Backend.GameLog.GetLog(logType, 10, result.FirstKeystring());
                        Debug.Log($"firstKey를 이용한({backendType.ToString()}){methodName} : {result2}");
                    }
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameLog.GetLog, logType, Int32.Parse(inputFields[1].text), result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("최신 로그의 inDate : " + result.Rows()[0]["inDate"]["S"].ToString());

                if (result.IsSuccess())
                {
                    if (result.HasFirstKey())
                    {
                        var result2 = Backend.GameLog.GetLog(logType, 10, result.FirstKeystring());
                        Debug.Log($"firstKey를 이용한 ({backendType.ToString()}){methodName} : {result2}");
                    }
                }
            });
        }
    }


}