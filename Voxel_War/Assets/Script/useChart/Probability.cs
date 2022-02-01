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

    public void ChangeButtonToProbability()
    {
        UIManager.instance.InitButton();

        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "GetProbabilityCardList", new List<string>(), GetProbabilityCardList);
        UIManager.instance.SetFunctionButton(i++, "GetProbability", new List<string>() { "string CardFileUuid" }, GetProbability);
        UIManager.instance.SetFunctionButton(i++, "GetProbabilitys", new List<string>() { "string CardFileID(selectedProbabilityFileId)", "int count" }, GetProbabilitys);
    }

    void GetProbabilityCardList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Probability.GetProbabilityCardList();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            string charts = string.Empty;
            for (int i = 0; i < result.Rows().Count; i++)
            {
                charts += result.Rows()[i]["probabilityName"]["S"].ToString() + " 의 차트 ID : " + result.Rows()["selectedProbabilityFileId"]["N"].ToString();
                charts += "\n";
            }
            Debug.Log("최신 확률 차트 : " + charts);
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Probability.GetProbabilityCardList(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               string charts = string.Empty;
               for (int i = 0; i < result.Rows().Count; i++)
               {
                   charts += result.Rows()[i]["probabilityName"]["S"].ToString() + " 의 차트 ID : " + result.Rows()["selectedProbabilityFileId"]["N"].ToString();
                   charts += "\n";
               }
               Debug.Log("최신 확률 차트 : " + charts);
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.Probability.GetProbabilityCardList, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               string charts = string.Empty;
               for (int i = 0; i < result.Rows().Count; i++)
               {
                   charts += result.Rows()[i]["probabilityName"]["S"].ToString() + " 의 차트 ID : " + result.Rows()["selectedProbabilityFileId"]["N"].ToString();
                   charts += "\n";
               }
               Debug.Log("최신 확률 차트 : " + charts);
           });
        }

    }

    void GetProbability(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Probability.GetProbability(inputFields[0].text);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("뽑은 아이템의 확률 : " + result.GetReturnValuetoJSON()["element"]["percent"]["S"].ToString());


        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Probability.GetProbability(inputFields[0].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               Debug.Log("뽑은 아이템의 확률 : " + result.GetReturnValuetoJSON()["element"]["percent"]["S"].ToString());


           });
        }
        else
        {
            SendQueue.Enqueue(Backend.Probability.GetProbability, inputFields[0].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               Debug.Log("뽑은 아이템의 확률 : " + result.GetReturnValuetoJSON()["element"]["percent"]["S"].ToString());
           });
        }

    }

    void GetProbabilitys(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Probability.GetProbabilitys(inputFields[0].text, Int32.Parse(inputFields[1].text));

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string items = string.Empty;
            for (int i = 0; i < result.GetReturnValuetoJSON()["elements"].Count; i++)
            {
                items += "아이템 num : " + result.GetReturnValuetoJSON()["elements"][i]["num"]["S"].ToString()
                 + " / 확률 : " + result.GetReturnValuetoJSON()["elements"][i]["percent"]["S"].ToString() + "\n";
            }
            Debug.Log("뽑은 아이템 정보 : " + items);


        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Probability.GetProbabilitys(inputFields[0].text, Int32.Parse(inputFields[1].text), result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");

               string items = string.Empty;
               for (int i = 0; i < result.GetReturnValuetoJSON()["elements"].Count; i++)
               {
                   items += "아이템 num : " + result.GetReturnValuetoJSON()["elements"][i]["num"]["S"].ToString()
                    + " / 확률 : " + result.GetReturnValuetoJSON()["elements"][i]["percent"]["S"].ToString() + "\n";
               }
               Debug.Log("뽑은 아이템 정보 : " + items);


           });
        }
        else
        {
            SendQueue.Enqueue(Backend.Probability.GetProbabilitys, inputFields[0].text, Int32.Parse(inputFields[1].text), result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string items = string.Empty;
                for (int i = 0; i < result.GetReturnValuetoJSON()["elements"].Count; i++)
                {
                    items += "아이템 num : " + result.GetReturnValuetoJSON()["elements"][i]["num"]["S"].ToString()
                     + " / 확률 : " + result.GetReturnValuetoJSON()["elements"][i]["percent"]["S"].ToString() + "\n";
                }
                Debug.Log("뽑은 아이템 정보 : " + items);
            });
        }
    }

}