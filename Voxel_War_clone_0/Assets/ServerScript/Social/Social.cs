using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using BackEnd;
using System.Reflection;
using System;

public partial class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void ChangeButtonToSocial()
    {
        UIManager.instance.InitButton();
        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "GetUserInfoByNickName", new List<string>() { "string nickname" }, GetUserInfoByNickName);
        UIManager.instance.SetFunctionButton(i++, "GetUserInfoByInDate", new List<string>() { "string inDate" }, GetUserInfoByInDate);
        UIManager.instance.SetFunctionButton(i++, "GetRandomUserInfo", new List<string>() { "int limit" }, GetRandomUserInfo);
        UIManager.instance.SetFunctionButton(i++, "GetRandomUserInfo(table)", new List<string>() { "string tableName", "string column", "int value", "int gap", "int limit" }, GetRandomUserInfobyData);
    }

    void GetUserInfoByNickName(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string nickname = inputFields[0].text;
        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.GetUserInfoByNickName(nickname);

            if (result.IsSuccess())
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string gamerIndate = result.GetReturnValuetoJSON()["row"]["inDate"].ToString();
                Debug.Log("해당 유저의 inDate : " + gamerIndate);
            }
            else
            {
                Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.GetUserInfoByNickName(nickname, result =>
            {

                if (result.IsSuccess())
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                    string gamerIndate = result.GetReturnValuetoJSON()["row"]["inDate"].ToString();
                    Debug.Log("해당 유저의 inDate : " + gamerIndate);
                }
                else
                {
                    Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.GetUserInfoByNickName, nickname, result =>
            {

                if (result.IsSuccess())
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                    string gamerIndate = result.GetReturnValuetoJSON()["row"]["inDate"].ToString();
                    Debug.Log("해당 유저의 inDate : " + gamerIndate);
                }
                else
                {
                    Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
                }
            });
        }
    }


    void GetUserInfoByInDate(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;
        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.GetUserInfoByInDate(inDate);

            if (result.IsSuccess())
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string gamerNickname = result.GetReturnValuetoJSON()["row"]["nickname"].ToString();
                Debug.Log("해당 유저의 nickname : " + gamerNickname);
            }
            else
            {
                Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.GetUserInfoByInDate(inDate, result =>
            {

                if (result.IsSuccess())
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                    string gamerNickname = result.GetReturnValuetoJSON()["row"]["nickname"].ToString();
                    Debug.Log("해당 유저의 nickname : " + gamerNickname);
                }
                else
                {
                    Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.GetUserInfoByInDate, inDate, result =>
            {

                if (result.IsSuccess())
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                    string gamerNickname = result.GetReturnValuetoJSON()["row"]["nickname"].ToString();
                    Debug.Log("해당 유저의 nickname : " + gamerNickname);
                }
                else
                {
                    Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
                }
            });
        }
    }


    void GetRandomUserInfo(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        int limit = Int32.Parse(inputFields[0].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.GetRandomUserInfo(limit);

            if (result.IsSuccess())
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string users = string.Empty;
                foreach (LitJson.JsonData json in result.Rows())
                {
                    users += "유저 : " + json["nickname"].ToString() + "\n";
                }
                Debug.Log("랜덤 유저 nickname : " + users);
            }
            else
            {
                Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.GetRandomUserInfo(limit, result =>
            {

                if (result.IsSuccess())
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                    string users = string.Empty;
                    foreach (LitJson.JsonData json in result.Rows())
                    {
                        users += "유저 : " + json["nickname"].ToString() + "\n";
                    }
                    Debug.Log("랜덤 유저 nickname : " + users);
                }
                else
                {
                    Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.GetRandomUserInfo, limit, result =>
            {

                if (result.IsSuccess())
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                    string users = string.Empty;
                    foreach (LitJson.JsonData json in result.Rows())
                    {
                        users += "유저 : " + json["nickname"].ToString() + "\n";
                    }
                    Debug.Log("랜덤 유저 nickname : " + users);
                }
                else
                {
                    Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
                }
            });
        }
    }

    void GetRandomUserInfobyData(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string tableName = inputFields[0].text;
        string column = inputFields[1].text;
        int value = Int32.Parse(inputFields[2].text);
        int gap = Int32.Parse(inputFields[3].text);
        int limit = Int32.Parse(inputFields[4].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.GetRandomUserInfo(tableName, column, value, gap, limit);

            if (result.IsSuccess())
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string users = string.Empty;
                foreach (LitJson.JsonData json in result.Rows())
                {
                    users += string.Format("유저 : {0} / {1} : {2} \n", json["nickname"].ToString(), column, json["table"][column].ToString());
                }
                Debug.Log("랜덤 유저 nickname : " + users);
            }
            else
            {
                Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.GetRandomUserInfo(tableName, column, value, gap, limit, result =>
              {

                  if (result.IsSuccess())
                  {
                      Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                      string users = string.Empty;
                      foreach (LitJson.JsonData json in result.Rows())
                      {
                          users += string.Format("유저 : {0} / {1} : {2} \n", json["nickname"].ToString(), column, json["table"][column].ToString());
                      }
                      Debug.Log("랜덤 유저 nickname : " + users);
                  }
                  else
                  {
                      Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
                  }
              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.GetRandomUserInfo, tableName, column, value, gap, limit, result =>
             {

                 if (result.IsSuccess())
                 {
                     Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                     string users = string.Empty;
                     foreach (LitJson.JsonData json in result.Rows())
                     {
                         users += string.Format("유저 : {0} / {1} : {2} \n", json["nickname"].ToString(), column, json["table"][column].ToString());
                     }
                     Debug.Log("랜덤 유저 nickname : " + users);
                 }
                 else
                 {
                     Debug.LogError($"({backendType.ToString()}){methodName} : {result}");
                 }
             });
        }
    }
}