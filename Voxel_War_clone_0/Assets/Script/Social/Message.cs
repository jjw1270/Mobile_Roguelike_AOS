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

    public void ChangeButtonToMessage()
    {
        UIManager.instance.InitButton();
        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "SendMessage", new List<string>() { "string recieverIndate", "string message" }, SendMessage);
        UIManager.instance.SetFunctionButton(i++, "GetReceivedMessageList", new List<string>(), GetReceivedMessageList);
        UIManager.instance.SetFunctionButton(i++, "GetSentMessageList", new List<string>(), GetSentMessageList);
        UIManager.instance.SetFunctionButton(i++, "GetReceivedMessage", new List<string>() { "string inDate" }, GetReceivedMessage);
        UIManager.instance.SetFunctionButton(i++, "GetSentMessage", new List<string>() { "string inDate" }, GetSentMessage);
        UIManager.instance.SetFunctionButton(i++, "DeleteReceivedMessage", new List<string>() { "string inDate" }, DeleteReceivedMessage);
        UIManager.instance.SetFunctionButton(i++, "DeleteSentMessage", new List<string>() { "string inDate" }, DeleteSentMessage);

    }

    void SendMessage(InputField[] inputFields) // 9
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;
        string message = inputFields[1].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Message.SendMessage(inDate, message);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Message.SendMessage(inDate, message, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

             });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Message.SendMessage, inDate, message, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
              });
        }
    }

    void GetReceivedMessageList(InputField[] inputFields) // 10
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Message.GetReceivedMessageList();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            if (result.IsSuccess())
            {
                string messages = string.Empty;
                foreach (LitJson.JsonData json in result.Rows())
                {
                    messages += string.Format("쪽지의 inDate : {0} / 내용 : {1}\n", json["inDate"]["S"].ToString(), json["content"]["S"].ToString());
                }
                Debug.Log("받은 메세지 내용 : \n" + messages);
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Message.GetReceivedMessageList(result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string messages = string.Empty;
                    foreach (LitJson.JsonData json in result.Rows())
                    {
                        messages += string.Format("쪽지의 inDate : {0} / 내용 : {1}\n", json["inDate"]["S"].ToString(), json["content"]["S"].ToString());
                    }
                    Debug.Log("받은 메세지 내용 : \n" + messages);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Message.GetReceivedMessageList, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                  if (result.IsSuccess())
                  {
                      string messages = string.Empty;
                      foreach (LitJson.JsonData json in result.Rows())
                      {
                          messages += string.Format("쪽지의 inDate : {0} / 내용 : {1}\n", json["inDate"]["S"].ToString(), json["content"]["S"].ToString());
                      }
                      Debug.Log("받은 메세지 내용 : \n" + messages);
                  }
              });
        }

    }
    void GetSentMessageList(InputField[] inputFields) // 11
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Message.GetSentMessageList();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            if (result.IsSuccess())
            {
                string messages = string.Empty;
                foreach (LitJson.JsonData json in result.Rows())
                {
                    messages += string.Format("쪽지의 inDate : {0} / 내용 : {1}\n", json["inDate"]["S"].ToString(), json["content"]["S"].ToString());
                }
                Debug.Log("보낸 메세지 내용 : \n" + messages);
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Message.GetSentMessageList(result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string messages = string.Empty;
                    foreach (LitJson.JsonData json in result.Rows())
                    {
                        messages += string.Format("쪽지의 inDate : {0} / 내용 : {1}\n", json["inDate"]["S"].ToString(), json["content"]["S"].ToString());
                    }
                    Debug.Log("보낸 메세지 내용 : \n" + messages);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Message.GetSentMessageList, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                  if (result.IsSuccess())
                  {
                      string messages = string.Empty;
                      foreach (LitJson.JsonData json in result.Rows())
                      {
                          messages += string.Format("쪽지의 inDate : {0} / 내용 : {1}\n", json["inDate"]["S"].ToString(), json["content"]["S"].ToString());
                      }
                      Debug.Log("보낸 메세지 내용 : \n" + messages);
                  }
              });
        }
    }
    void GetReceivedMessage(InputField[] inputFields) // 12
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Message.GetReceivedMessage(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            string content = string.Format("보낸사람 : {0} / 내용 - {1}",
            result.GetReturnValuetoJSON()["row"]["senderNickname"]["S"].ToString(), result.GetReturnValuetoJSON()["row"]["content"]["S"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Message.GetReceivedMessage(inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                 string content = string.Format("보낸사람 : {0} / 내용 - {1}",
                 result.GetReturnValuetoJSON()["row"]["senderNickname"]["S"].ToString(), result.GetReturnValuetoJSON()["row"]["content"]["S"].ToString());
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Message.GetReceivedMessage, inDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  string content = string.Format("보낸사람 : {0} / 내용 - {1}",
                    result.GetReturnValuetoJSON()["row"]["senderNickname"]["S"].ToString(), result.GetReturnValuetoJSON()["row"]["content"]["S"].ToString());
              });
        }
    }

    void GetSentMessage(InputField[] inputFields) // 13
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Message.GetSentMessage(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            string content = string.Format("보낸사람 : {0} / 내용 - {1}",
            result.GetReturnValuetoJSON()["row"]["senderNickname"]["S"].ToString(), result.GetReturnValuetoJSON()["row"]["content"]["S"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Message.GetSentMessage(inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                 string content = string.Format("보낸사람 : {0} / 내용 - {1}",
                 result.GetReturnValuetoJSON()["row"]["senderNickname"]["S"].ToString(), result.GetReturnValuetoJSON()["row"]["content"]["S"].ToString());
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Message.GetSentMessage, inDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  string content = string.Format("보낸사람 : {0} / 내용 - {1}",
                    result.GetReturnValuetoJSON()["row"]["senderNickname"]["S"].ToString(), result.GetReturnValuetoJSON()["row"]["content"]["S"].ToString());
              });
        }
    }

    void DeleteReceivedMessage(InputField[] inputFields) // 14
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Message.DeleteReceivedMessage(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Message.DeleteReceivedMessage(inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Message.DeleteReceivedMessage, inDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
              });
        }

    }

    void DeleteSentMessage(InputField[] inputFields) // 15
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Message.DeleteSentMessage(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Message.DeleteSentMessage(inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Message.DeleteSentMessage, inDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
              });
        }
    }

}