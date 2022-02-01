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
    public void ChangeButtonToOperation()
    {
        UIManager.instance.InitButton();

        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "NoticeList", new List<string>() { "int limit", "string offset" }, NoticeList);
        UIManager.instance.SetFunctionButton(i++, "NoticeOne", new List<string>() { "string inDate" }, NoticeOne);
        UIManager.instance.SetFunctionButton(i++, "GetTempNotice", new List<string>(), GetTempNotice);
        UIManager.instance.SetFunctionButton(i++, "EventList", new List<string>() { "int limit", "string offset" }, EventList);
        UIManager.instance.SetFunctionButton(i++, "EventOne", new List<string>() { "string indate" }, EventOne);
        UIManager.instance.SetFunctionButton(i++, "GetPolicy", new List<string>(), GetPolicy);

    }

    void NoticeList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Notice.NoticeList(Int32.Parse(inputFields[0].text));

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("최신 NoticeList 의 제목 : " + result.Rows()[0]["title"]["S"].ToString());

            if (result.IsSuccess())
            {
                string offset = result.LastEvaluatedKeyString();
                if (!string.IsNullOrEmpty(offset))
                {
                    // 2. 불러온 공지사항 x개 이후의 10개 불러오기
                    result = Backend.Notice.NoticeList(10, offset);
                    Debug.Log($"offset을 이용한 ({backendType.ToString()}){methodName} : {result}");
                }
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Notice.NoticeList(Int32.Parse(inputFields[0].text), result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("최신 NoticeList 의 제목 : " + result.Rows()[0]["title"]["S"].ToString());

                if (result.IsSuccess())
                {
                    string offset = result.LastEvaluatedKeyString();
                    if (!string.IsNullOrEmpty(offset))
                    {
                        // 2. 불러온 공지사항 x개 이후의 10개 불러오기
                        Backend.Notice.NoticeList(10, result2 =>
                        {
                            Debug.Log($"offset을 이용한 ({backendType.ToString()}){methodName} : {result2}");

                        });
                    }
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Notice.NoticeList, Int32.Parse(inputFields[0].text), result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("최신 NoticeList 의 제목 : " + result.Rows()[0]["title"]["S"].ToString());

                if (result.IsSuccess())
                {
                    string offset = result.LastEvaluatedKeyString();
                    if (!string.IsNullOrEmpty(offset))
                    {
                        // 2. 불러온 공지사항 x개 이후의 10개 불러오기
                        SendQueue.Enqueue(Backend.Notice.NoticeList, 10, result2 =>
                       {
                           Debug.Log($"offset을 이용한 ({backendType.ToString()}){methodName} : {result2}");
                       });
                    }
                }
            });
        }
    }

    void NoticeOne(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Notice.NoticeOne(inputFields[0].text);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("해당 NoticeOne 의 제목 : " + result.GetReturnValuetoJSON()["row"]["title"]["S"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Notice.NoticeOne(inputFields[0].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("해당 NoticeOne 의 제목 : " + result.GetReturnValuetoJSON()["row"]["title"]["S"].ToString());


            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Notice.NoticeOne, inputFields[0].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("해당 NoticeOne 의 제목 : " + result.GetReturnValuetoJSON()["row"]["title"]["S"].ToString());

            });
        }

    }

    void GetTempNotice(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            string resultString = Backend.Notice.GetTempNotice();

            Debug.Log($"({backendType.ToString()}){methodName} : {resultString}");
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Notice.GetTempNotice(resultString =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {resultString}");

           });
        }
        else
        {
            Debug.LogError(methodName + "의 SendQueue 형식은 지원하지 않습니다.");
        }
    }

    void EventList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Event.EventList(Int32.Parse(inputFields[0].text));

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("최신 EventList 의 제목 : " + result.Rows()[0]["title"]["S"].ToString());
            if (result.IsSuccess())
            {
                string offset = result.LastEvaluatedKeyString();
                if (!string.IsNullOrEmpty(offset))
                {
                    // 2. 불러온 이벤트 x개 이후의 10개 불러오기
                    result = Backend.Event.EventList(10, offset);
                    Debug.Log($"offset을 이용한 ({backendType.ToString()}){methodName} : {result}");
                }
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Event.EventList(Int32.Parse(inputFields[0].text), result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("최신 EventList 의 제목 : " + result.Rows()[0]["title"]["S"].ToString());

                if (result.IsSuccess())
                {
                    string offset = result.LastEvaluatedKeyString();
                    if (!string.IsNullOrEmpty(offset))
                    {
                        // 2. 불러온 이벤트 x개 이후의 10개 불러오기
                        Backend.Event.EventList(10, result2 =>
                        {
                            Debug.Log($"offset을 이용한 ({backendType.ToString()}){methodName} : {result2}");

                        });
                    }
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Event.EventList, Int32.Parse(inputFields[0].text), result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("최신 EventList 의 제목 : " + result.Rows()[0]["title"]["S"].ToString());

                if (result.IsSuccess())
                {
                    string offset = result.LastEvaluatedKeyString();
                    if (!string.IsNullOrEmpty(offset))
                    {
                        // 2. 불러온 이벤트 x개 이후의 10개 불러오기
                        SendQueue.Enqueue(Backend.Event.EventList, 10, result2 =>
                       {
                           Debug.Log($"offset을 이용한 ({backendType.ToString()}){methodName} : {result2}");
                       });
                    }
                }
            });
        }
    }

    void EventOne(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Event.EventOne(inputFields[0].text);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("해당 EventOne 의 제목 : " + result.GetReturnValuetoJSON()["row"]["title"]["S"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Event.EventOne(inputFields[0].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("해당 EventOne 의 제목 : " + result.GetReturnValuetoJSON()["row"]["title"]["S"].ToString());


            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Event.EventOne, inputFields[0].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("해당 EventOne 의 제목 : " + result.GetReturnValuetoJSON()["row"]["title"]["S"].ToString());

            });
        }
    }

    void GetPolicy(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Notice.GetPolicy();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            Debug.Log("서비스 이용 약관 제목 : " + result.GetReturnValuetoJSON()["terms"].ToString());
            Debug.Log("서비스 이용 약관 URL : " + result.GetReturnValuetoJSON()["termsURL"].ToString());
            Debug.Log("개인 정보 처리 방침 제목 : " + result.GetReturnValuetoJSON()["privacy"].ToString());
            Debug.Log("개인 정보 처리 방침 URL : " + result.GetReturnValuetoJSON()["privacyURL"].ToString());


        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Notice.GetPolicy(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");

               Debug.Log("서비스 이용 약관 제목 : " + result.GetReturnValuetoJSON()["terms"].ToString());
               Debug.Log("서비스 이용 약관 URL : " + result.GetReturnValuetoJSON()["termsURL"].ToString());
               Debug.Log("개인 정보 처리 방침 제목 : " + result.GetReturnValuetoJSON()["privacy"].ToString());
               Debug.Log("개인 정보 처리 방침 URL : " + result.GetReturnValuetoJSON()["privacyURL"].ToString());
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.Notice.GetPolicy, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                Debug.Log("서비스 이용 약관 제목 : " + result.GetReturnValuetoJSON()["terms"].ToString());
                Debug.Log("서비스 이용 약관 URL : " + result.GetReturnValuetoJSON()["termsURL"].ToString());
                Debug.Log("개인 정보 처리 방침 제목 : " + result.GetReturnValuetoJSON()["privacy"].ToString());
                Debug.Log("개인 정보 처리 방침 URL : " + result.GetReturnValuetoJSON()["privacyURL"].ToString());
            });
        }
    }



}
