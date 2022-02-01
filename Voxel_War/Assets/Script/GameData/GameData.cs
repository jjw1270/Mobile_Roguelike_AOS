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
    public void ChangeButtonToGameData()
    {
        UIManager.instance.InitButton();
        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "GetTableList", new List<string>(), GetTableList);
        UIManager.instance.SetFunctionButton(i++, "Insert", new List<string>() { "string tableName", "string columnName1", "string columnValue1", "string columnName2", "int intValue2" }, Insert);
        UIManager.instance.SetFunctionButton(i++, "GetV2(inDate)", new List<string>() { "string tableName", "string rowIndate", "string owner_inDate" }, GetV2Indate);
        UIManager.instance.SetFunctionButton(i++, "Get(Where)", new List<string>() { "string tableName", "string whereColumnName", "string whereColumnValue" }, GetWhere);
        UIManager.instance.SetFunctionButton(i++, "GetMyData", new List<string>() { "string tableName" }, GetMyData);
        UIManager.instance.SetFunctionButton(i++, "UpdateV2(inDate)", new List<string>() { "string tableName", "string rowIndate", "string owner_inDate", "string columnName", "string columnValue" }, UpdateV2Indate);
        UIManager.instance.SetFunctionButton(i++, "Update(Where)", new List<string>() { "string tableName", "string whereColumnName", "string whereColumnValue", "string columnName", "string valueValue" }, UpdateWhere);
        UIManager.instance.SetFunctionButton(i++, "UpdateWithCalculationV2(inDate)", new List<string>() { "string tableName", "string rowIndate", "string owner_inDate", "string columnName", "int intValue(plus)" }, UpdateWithCalculationV2Indate);
        UIManager.instance.SetFunctionButton(i++, "UpdateWithCalculation(Where)", new List<string>() { "string tableName", "string whereColumnName", "string whereColumnValue", "string columnName", "int intValue(plus)" }, UpdateWithCalculationWhere);
        UIManager.instance.SetFunctionButton(i++, "DeleteV2(inDate)", new List<string>() { "string tableName", "string rowIndate", "string owner_inDate" }, DeleteV2Indate);
        UIManager.instance.SetFunctionButton(i++, "Delete(Where)", new List<string>() { "string tableName", "string whereColumnName", "string whereColumnValue" }, DeleteWhere);

    }
    void GetTableList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.GetTableList();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string tables = string.Empty;
            var json = result.GetReturnValuetoJSON()["tables"];
            for (int i = 0; i < json.Count; i++)
            {
                tables += "테이블 이름 : " + json[i]["tableName"].ToString() + "\n";
            }
            Debug.Log(tables);

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.GetTableList(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               string tables = string.Empty;
               var json = result.GetReturnValuetoJSON()["tables"];
               for (int i = 0; i < json.Count; i++)
               {
                   tables += "테이블 이름 : " + json[i]["tableName"].ToString() + "\n";
               }
               Debug.Log(tables);

           });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.GetTableList, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                string tables = string.Empty;
                var json = result.GetReturnValuetoJSON()["tables"];
                for (int i = 0; i < json.Count; i++)
                {
                    tables += "테이블 이름 : " + json[i]["tableName"].ToString() + "\n";
                }
                Debug.Log(tables);
            });
        }
    }
    void Insert(InputField[] inputFields)
    {
        Param param = new Param();
        string tableName = inputFields[0].text;
        param.Add(inputFields[1].text, inputFields[2].text);
        param.Add(inputFields[3].text, System.Int32.Parse(inputFields[4].text));

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.Insert(tableName, param);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("삽입한 데이터의 rowIndate : " + result.GetInDate());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.Insert(tableName, param, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("삽입한 데이터의 rowIndate : " + result.GetInDate());
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.Insert, tableName, param, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Debug.Log("삽입한 데이터의 rowIndate : " + result.GetInDate());

            });
        }
    }

    void GetV2Indate(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string inDate = inputFields[1].text;
        string owner_inDate = inputFields[2].text;

        //아무것도 안쓰면 owner_indate는 자신의 inDate를 가르킨다.
        if (owner_inDate == string.Empty || owner_inDate == "")
        {
            owner_inDate = Backend.UserInDate;
        }


        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.GetV2(tableName, inDate, owner_inDate);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            Debug.Log("해당 데이터의 owner_inDate : " + result.GetReturnValuetoJSON()["row"]["owner_inDate"]["S"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.GetV2(tableName, inDate, owner_inDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  Debug.Log("해당 데이터의 owner_inDate : " + result.GetReturnValuetoJSON()["row"]["owner_inDate"]["S"].ToString());

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.GetV2, tableName, inDate, owner_inDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  Debug.Log("해당 데이터의 owner_inDate : " + result.GetReturnValuetoJSON()["row"]["owner_inDate"]["S"].ToString());
              });
        }
    }

    void GetWhere(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string whereColumn = inputFields[1].text;
        string whereValue = inputFields[2].text;

        Where where = new Where();

        where.Equal(whereColumn, whereValue);

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.Get(tableName, where);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string data = string.Empty;
            foreach (LitJson.JsonData json in result.Rows())
            {
                data += json["owner_inDate"]["S"].ToString() + "\n";
            }
            Debug.Log("해당 데이터들의 owner_inDate : " + data);

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.Get(tableName, where, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                 string data = string.Empty;
                 foreach (LitJson.JsonData json in result.Rows())
                 {
                     data += json["owner_inDate"]["S"].ToString() + "\n";
                 }
                 Debug.Log("해당 데이터들의 owner_inDate : " + data);
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.Get, tableName, where, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                 string data = string.Empty;
                 foreach (LitJson.JsonData json in result.Rows())
                 {
                     data += json["owner_inDate"]["S"].ToString() + "\n";
                 }
                 Debug.Log("해당 데이터들의 owner_inDate : " + data);
             });
        }
    }

    void GetMyData(InputField[] inputFields)
    {
        // 자신의 데이터를 찾고 해당 데이터의 inDate찾기.

        string myIndate = string.Empty;

        string tableName = inputFields[0].text;

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.Get(tableName, new Where());
            Debug.Log($"({backendType.ToString()}){methodName}-Where : {result}");

            if (result.Rows().Count > 0)
            {
                myIndate = result.Rows()[0]["inDate"]["S"].ToString();
                Debug.Log("나의 최신 게임 데이터 inDate : " + myIndate);

                //GetMyData를 통해 받은 자신의 rowIndate를 저장한 후 이용
                var result2 = Backend.GameData.GetMyData(tableName, myIndate);
                Debug.Log($"({backendType.ToString()}){methodName} - InDate : {result2}");
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.GetMyData(tableName, new Where(), result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName}-Where : {result}");
                 if (result.Rows().Count > 0)
                 {
                     myIndate = result.Rows()[0]["inDate"]["S"].ToString();
                     Debug.Log("나의 최신 게임 데이터 inDate : " + myIndate);

                     //GetMyData를 통해 받은 자신의 rowIndate를 저장한 후 이용
                     Backend.GameData.GetMyData(tableName, myIndate, result2 =>
                    {
                        Debug.Log($"({backendType.ToString()}){methodName}-InDate : {result2}");

                    });
                 }
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.GetMyData, tableName, new Where(), result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName}-Where : {result}");
                 if (result.Rows().Count > 0)
                 {
                     myIndate = result.Rows()[0]["inDate"]["S"].ToString();
                     Debug.Log("나의 최신 게임 데이터 inDate : " + myIndate);

                     //GetMyData를 통해 받은 자신의 rowIndate를 저장한 후 이용
                     SendQueue.Enqueue(Backend.GameData.GetMyData, tableName, myIndate, result2 =>
                    {
                        Debug.Log($"({backendType.ToString()}){methodName}-InDate : {result2}");

                    });
                 }

             });
        }
    }


    void UpdateV2Indate(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string inDate = inputFields[1].text;
        string owner_inDate = inputFields[2].text;
        string updateColumn = inputFields[3].text;
        string updateValue = inputFields[4].text;


        //아무것도 안쓰면 owner_indate는 자신의 inDate를 가르킨다.
        if (owner_inDate == string.Empty || owner_inDate == "")
        {
            owner_inDate = Backend.UserInDate;
        }

        Param param = new Param();
        param.Add(updateColumn, updateValue);

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.UpdateV2(tableName, inDate, owner_inDate, param);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.UpdateV2(tableName, inDate, owner_inDate, param, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.UpdateV2, tableName, inDate, owner_inDate, param, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
              });
        }
    }

    void UpdateWhere(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string whereColumn = inputFields[1].text;
        string whereValue = inputFields[2].text;

        string updateColumn = inputFields[3].text;
        string updateValue = inputFields[4].text;

        Where where = new Where();
        where.Equal(whereColumn, whereValue);

        Param param = new Param();
        param.Add(updateColumn, updateValue);

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.Update(tableName, where, param);
            Debug.Log($"({backendType.ToString()}){methodName}-Where : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.Update(tableName, where, param, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName}-Where : {result}");

             });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.Update, tableName, where, param, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName}-Where : {result}");
             });
        }
    }

    void UpdateWithCalculationV2Indate(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string inDate = inputFields[1].text;
        string owner_inDate = inputFields[2].text;
        string updateColumn = inputFields[3].text;
        int updateValue = Int32.Parse(inputFields[4].text);

        //아무것도 안쓰면 owner_indate는 자신의 inDate를 가르킨다.
        if (owner_inDate == string.Empty || owner_inDate == "")
        {
            owner_inDate = Backend.UserInDate;
        }

        Param param = new Param();
        param.AddCalculation(updateColumn, GameInfoOperator.addition, updateValue);

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.UpdateWithCalculationV2(tableName, inDate, owner_inDate, param);

            Debug.Log($"({backendType.ToString()}){methodName}-inDate : {result}");


        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.UpdateWithCalculationV2(tableName, inDate, owner_inDate, param, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName}-inDate : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.UpdateWithCalculationV2, tableName, inDate, owner_inDate, param, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName}-inDate : {result}");
              });
        }
    }

    void UpdateWithCalculationWhere(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string whereColumn = inputFields[1].text;
        string whereValue = inputFields[2].text;

        string updateColumn = inputFields[3].text;
        int updateValue = Int32.Parse(inputFields[4].text);

        Where where = new Where();
        where.Equal(whereColumn, whereValue);

        Param param = new Param();
        param.AddCalculation(updateColumn, GameInfoOperator.addition, updateValue);

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.UpdateWithCalculation(tableName, where, param);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");


        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.UpdateWithCalculation(tableName, where, param, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

             });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.UpdateWithCalculation, tableName, where, param, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }

    void DeleteV2Indate(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string inDate = inputFields[1].text;
        string owner_inDate = inputFields[2].text;
        //아무것도 안쓰면 owner_indate는 자신의 inDate를 가르킨다.
        if (owner_inDate == string.Empty || owner_inDate == "")
        {
            owner_inDate = Backend.UserInDate;
        }

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.DeleteV2(tableName, inDate, owner_inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.DeleteV2(tableName, inDate, owner_inDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.DeleteV2, tableName, inDate, owner_inDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
              });
        }
    }

    void DeleteWhere(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string whereColumn = inputFields[1].text;
        string whereValue = inputFields[2].text;

        Where where = new Where();

        where.Equal(whereColumn, whereValue);

        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.Delete(tableName, where);
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.Delete(tableName, where, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.Delete, tableName, where, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }
}
