using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using System.Reflection;
public partial class BackendManager : MonoBehaviour
{

    public void ChangeButtonToChart()
    {
        UIManager.instance.InitButton();

        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "GetChartList", new List<string>(), GetChartList);
        UIManager.instance.SetFunctionButton(i++, "GetChartContents", new List<string>() { "string chartUUID(selectedChartFileId)" }, GetChartContents);
        UIManager.instance.SetFunctionButton(i++, "GetOneChartAndSave", new List<string>() { "string chartFileId", "string chartName" }, GetOneChartAndSave);
        UIManager.instance.SetFunctionButton(i++, "GetAllChartAndSave", new List<string>() { "bool isChartKeyIsName(default = false)" }, GetAllChartAndSave);
        UIManager.instance.SetFunctionButton(i++, "GetLocalChartData", new List<string>() { "string chartKey" }, GetLocalChartData);
        UIManager.instance.SetFunctionButton(i++, "DeleteLocalChartData", new List<string>() { "string chartKey" }, DeleteLocalChartData);

    }
    void GetChartList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Chart.GetChartList();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("최신 차트의 selectedChartFileId : " + result.Rows()[0]["selectedChartFileId"]["N"].ToString());
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Chart.GetChartList(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               Debug.Log("최신 차트의 selectedChartFileId : " + result.Rows()[0]["selectedChartFileId"]["N"].ToString());

           });
        }
        else
        {
            SendQueue.Enqueue(Backend.Chart.GetChartList, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               Debug.Log("최신 차트의 selectedChartFileId : " + result.Rows()[0]["selectedChartFileId"]["N"].ToString());

           });
        }

    }

    void GetChartContents(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string coloumnName = "column1";

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Chart.GetChartContents(inputFields[0].text);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string AllColumn1 = string.Empty;
            for (int i = 0; i < result.Rows().Count; i++)
            {
                AllColumn1 += result.Rows()[i][coloumnName]["S"].ToString() + "\n";
            }

            Debug.Log("해당 차트의 컬럼들 : " + AllColumn1);
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Chart.GetChartContents(inputFields[0].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");

               string AllColumn1 = string.Empty;
               for (int i = 0; i < result.Rows().Count; i++)
               {
                   AllColumn1 += result.Rows()[i][coloumnName]["S"].ToString() + "\n";
               }

               Debug.Log("해당 차트의 컬럼들 : " + AllColumn1);

           });
        }
        else
        {
            SendQueue.Enqueue(Backend.Chart.GetChartContents, inputFields[0].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");

               string AllColumn1 = string.Empty;
               for (int i = 0; i < result.Rows().Count; i++)
               {
                   AllColumn1 += result.Rows()[i][coloumnName]["S"].ToString() + "\n";
               }

               Debug.Log("해당 차트의 컬럼들 : " + AllColumn1);

           });
        }
    }

    void GetOneChartAndSave(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string coloumnName = "column1";

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Chart.GetOneChartAndSave(inputFields[0].text, inputFields[1].text);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string AllColumn1 = string.Empty;
            for (int i = 0; i < result.Rows().Count; i++)
            {
                AllColumn1 += result.Rows()[i][coloumnName]["S"].ToString() + "\n";
            }

            Debug.Log("저장된 해당 차트의 컬럼들 : " + AllColumn1);
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Chart.GetOneChartAndSave(inputFields[0].text, inputFields[1].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string AllColumn1 = string.Empty;
                for (int i = 0; i < result.Rows().Count; i++)
                {
                    AllColumn1 += result.Rows()[i][coloumnName]["S"].ToString() + "\n";
                }

                Debug.Log("저장된 해당 차트의 컬럼들 : " + AllColumn1);

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Chart.GetOneChartAndSave, inputFields[0].text, inputFields[1].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string AllColumn1 = string.Empty;
                for (int i = 0; i < result.Rows().Count; i++)
                {
                    AllColumn1 += result.Rows()[i][coloumnName]["S"].ToString() + "\n";
                }

                Debug.Log("저장된 해당 차트의 컬럼들 : " + AllColumn1);

            });
        }
    }

    void GetAllChartAndSave(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        bool isChartKeyIsName = false;
        if (inputFields[0].text == "true")
        {
            isChartKeyIsName = true;
        }
        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Chart.GetAllChartAndSave(isChartKeyIsName);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("저장된 최신 차트의 selectedChartFileId : " + result.Rows()["selectedChartFileId"]["N"].ToString());
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Chart.GetAllChartAndSave(isChartKeyIsName, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               Debug.Log("저장된 최신 차트의 selectedChartFileId : " + result.Rows()["selectedChartFileId"]["N"].ToString());

           });
        }
        else
        {
            SendQueue.Enqueue(Backend.Chart.GetAllChartAndSave, isChartKeyIsName, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("저장된 최신 차트의 selectedChartFileId : " + result.Rows()["selectedChartFileId"]["N"].ToString());

            });
        }

    }

    void GetLocalChartData(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string resultString = Backend.Chart.GetLocalChartData(inputFields[0].text);
        Debug.Log($"{methodName} : {resultString}");


        string AllColumn1 = string.Empty;
        string coloumnName = "column1";

        LitJson.JsonData json = LitJson.JsonMapper.ToObject(resultString);

        for (int i = 0; i < json["rows"].Count; i++)
        {
            AllColumn1 += result.Rows()[i][coloumnName]["S"].ToString() + "\n";
        }

        Debug.Log("저장된 해당 차트의 컬럼들 : " + AllColumn1);
    }

    void DeleteLocalChartData(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        try
        {
            Backend.Chart.DeleteLocalChartData(inputFields[0].text);
            Debug.Log($"{methodName} : 성공");


        }
        catch (System.Exception e)
        {
            Debug.Log($"({backendType.ToString()}){methodName} : {e.ToString()}");
        }
    }

}