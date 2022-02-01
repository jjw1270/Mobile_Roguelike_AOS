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

    public void ChangeButtonToFriend()
    {
        UIManager.instance.InitButton();
        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "GetFriendList", new List<string>() { "int limit", "int offset" }, GetFriendList);
        UIManager.instance.SetFunctionButton(i++, "RequestFriend", new List<string>() { "string userIndate" }, RequestFriend);
        UIManager.instance.SetFunctionButton(i++, "GetSentRequestList", new List<string>() { "int limit", "int offset" }, GetSentRequestList);
        UIManager.instance.SetFunctionButton(i++, "GetReceivedRequestList", new List<string>() { "int limit", "int offset" }, GetReceivedRequestList);
        UIManager.instance.SetFunctionButton(i++, "RevokeSentRequest", new List<string>() { "string userIndate" }, RevokeSentRequest);
        UIManager.instance.SetFunctionButton(i++, "AcceptFriend", new List<string>() { "string userIndate" }, AcceptFriend);
        UIManager.instance.SetFunctionButton(i++, "RejectFriend", new List<string>() { "string userIndate" }, RejectFriend);
        UIManager.instance.SetFunctionButton(i++, "BreakFriend", new List<string>() { "string userIndate" }, BreakFriend);

    }

    void GetFriendList(InputField[] inputFields) // 1
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        int limit = Int32.Parse(inputFields[0].text);
        int offset = Int32.Parse(inputFields[1].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Friend.GetFriendList(limit, offset);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            if (result.IsSuccess())
            {
                string friends = string.Empty;
                foreach (LitJson.JsonData json in result.Rows())
                {
                    friends += string.Format("친구의 이름 : {0} / inDate : {1}\n", json["nickname"]["S"].ToString(), json["inDate"]["S"].ToString());
                }
                Debug.Log("친구 목록 : \n" + friends);
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Friend.GetFriendList(limit, offset, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string friends = string.Empty;
                    foreach (LitJson.JsonData json in result.Rows())
                    {
                        friends += string.Format("친구의 이름 : {0} / inDate : {1}\n", json["nickname"]["S"].ToString(), json["inDate"]["S"].ToString());
                    }
                    Debug.Log("친구 목록 : \n" + friends);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Friend.GetFriendList, limit, offset, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                 if (result.IsSuccess())
                 {
                     string friends = string.Empty;
                     foreach (LitJson.JsonData json in result.Rows())
                     {
                         friends += string.Format("친구의 이름 : {0} / inDate : {1}\n", json["nickname"]["S"].ToString(), json["inDate"]["S"].ToString());
                     }
                     Debug.Log("친구 목록 : \n" + friends);
                 }
             });
        }
    }

    void RequestFriend(InputField[] inputFields) // 2
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Friend.RequestFriend(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Friend.RequestFriend(inDate, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Friend.RequestFriend, inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }

    void GetSentRequestList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        int limit = Int32.Parse(inputFields[0].text);
        int offset = Int32.Parse(inputFields[1].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Friend.GetSentRequestList(limit, offset);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            if (result.IsSuccess())
            {
                string friends = string.Empty;
                foreach (LitJson.JsonData json in result.Rows())
                {
                    friends += string.Format("친구 요청 보낸 유저의 이름 : {0} / inDate : {1}\n", json["nickname"]["S"].ToString(), json["inDate"]["S"].ToString());
                }
                Debug.Log("친구 요청 보낸 유저 목록 : \n" + friends);
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Friend.GetSentRequestList(limit, offset, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string friends = string.Empty;
                    foreach (LitJson.JsonData json in result.Rows())
                    {
                        friends += string.Format("친구 요청 보낸 유저의 이름 : {0} / inDate : {1}\n", json["nickname"]["S"].ToString(), json["inDate"]["S"].ToString());
                    }
                    Debug.Log("친구 요청 보낸 유저 목록 : \n" + friends);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Friend.GetSentRequestList, limit, offset, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                 if (result.IsSuccess())
                 {
                     string friends = string.Empty;
                     foreach (LitJson.JsonData json in result.Rows())
                     {
                         friends += string.Format("친구 요청 보낸 유저의 이름 : {0} / inDate : {1}\n", json["nickname"]["S"].ToString(), json["inDate"]["S"].ToString());
                     }
                     Debug.Log("친구 요청 보낸 유저 목록 : \n" + friends);
                 }
             });
        }
    }

    void GetReceivedRequestList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        int limit = Int32.Parse(inputFields[0].text);
        int offset = Int32.Parse(inputFields[1].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Friend.GetReceivedRequestList(limit, offset);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            if (result.IsSuccess())
            {
                string friends = string.Empty;
                foreach (LitJson.JsonData json in result.Rows())
                {
                    friends += string.Format("친구 요청 받은 유저의 이름 : {0} / inDate : {1}\n", json["nickname"]["S"].ToString(), json["inDate"]["S"].ToString());
                }
                Debug.Log("친구 요청 받은 유저 목록 : \n" + friends);
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Friend.GetReceivedRequestList(limit, offset, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string friends = string.Empty;
                    foreach (LitJson.JsonData json in result.Rows())
                    {
                        friends += string.Format("친구 요청 받은 유저의 이름 : {0} / inDate : {1}\n", json["nickname"]["S"].ToString(), json["inDate"]["S"].ToString());
                    }
                    Debug.Log("친구 받은 보낸 유저 목록 : \n" + friends);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Friend.GetReceivedRequestList, limit, offset, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                 if (result.IsSuccess())
                 {
                     string friends = string.Empty;
                     foreach (LitJson.JsonData json in result.Rows())
                     {
                         friends += string.Format("친구 받은 보낸 유저의 이름 : {0} / inDate : {1}\n", json["nickname"]["S"].ToString(), json["inDate"]["S"].ToString());
                     }
                     Debug.Log("친구 받은 보낸 유저 목록 : \n" + friends);
                 }
             });
        }
    }


    void RevokeSentRequest(InputField[] inputFields) // 2
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Friend.RevokeSentRequest(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Friend.RevokeSentRequest(inDate, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Friend.RevokeSentRequest, inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }

    void AcceptFriend(InputField[] inputFields) // 2
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Friend.AcceptFriend(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Friend.AcceptFriend(inDate, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Friend.AcceptFriend, inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }


    void RejectFriend(InputField[] inputFields) // 2
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Friend.RejectFriend(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Friend.RejectFriend(inDate, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Friend.RejectFriend, inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }


    void BreakFriend(InputField[] inputFields) // 2
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Friend.BreakFriend(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Friend.BreakFriend(inDate, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Friend.BreakFriend, inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }

}