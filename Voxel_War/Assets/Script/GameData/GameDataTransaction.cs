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
    List<TransactionValue> transactionWriteList = new List<TransactionValue>();
    List<TransactionValue> transactionReadList = new List<TransactionValue>();


    public void ChangeButtonToGameDataTransaction()
    {
        UIManager.instance.InitButton();
        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "ResetTransactionList", new List<string>(), ResetTransactionList);
        UIManager.instance.SetFunctionButton(i++, "TransactionWrite", new List<string>(), TransactionWriteV2);
        UIManager.instance.SetFunctionButton(i++, "TransactionRead", new List<string>(), TransactionReadV2);
        UIManager.instance.SetFunctionButton(i++, "AddInsert", new List<string>() { "string tableName", "string columnName1", "string columnValue1", "string columnName2", "int intValue2" }, AddSetInsert);
        UIManager.instance.SetFunctionButton(i++, "AddGetV2(inDate)", new List<string>() { "string tableName", "string rowIndate", "string owner_inDate" }, AddSetGetV2Indate);
        UIManager.instance.SetFunctionButton(i++, "AddGet(Where)", new List<string>() { "string tableName", "string whereColumnName", "string whereColumnValue" }, AddSetGetWhere);
        UIManager.instance.SetFunctionButton(i++, "AddUpdateV2(inDate)", new List<string>() { "string tableName", "string rowIndate", "string owner_inDate", "string columnName", "string columnValue" }, AddSetUpdateV2Indate);
        UIManager.instance.SetFunctionButton(i++, "AddUpdate(Where)", new List<string>() { "string tableName", "string whereColumnName", "string whereColumnValue", "string columnName", "string valueValue" }, AddSetUpdateWhere);
        UIManager.instance.SetFunctionButton(i++, "AddDeleteV2(inDate)", new List<string>() { "string tableName", "string rowIndate", "string owner_inDate" }, AddSetDeleteV2Indate);
        UIManager.instance.SetFunctionButton(i++, "AddDelete(Where)", new List<string>() { "string tableName", "string whereColumnName", "string whereColumnValue" }, AddSetDeleteWhere);

    }
    void ResetTransactionList(InputField[] inputFields)
    {
        transactionWriteList.Clear();
        transactionReadList.Clear();

        Debug.Log("transactionWriteList / transactionReadList 초기화 성공헀습니다.");
    }
    void TransactionWriteV2(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.TransactionWriteV2(transactionWriteList);
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.TransactionWriteV2(transactionWriteList, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.TransactionWriteV2, transactionWriteList, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }
    void TransactionReadV2(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.GameData.TransactionReadV2(transactionReadList);
            string data = string.Empty;
            foreach (LitJson.JsonData json in result.GetReturnValuetoJSON()["Responses"])
            {
                data += json["inDate"]["S"].ToString() + "\n";
            }
            Debug.Log("불러온 데이터들의 inDate : " + data);

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.GameData.TransactionReadV2(transactionReadList, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                 string data = string.Empty;
                 foreach (LitJson.JsonData json in result.GetReturnValuetoJSON()["Responses"])
                 {
                     data += json["inDate"]["S"].ToString() + "\n";
                 }
                 Debug.Log("불러온 데이터들의 inDate : " + data);

             });
        }
        else
        {
            SendQueue.Enqueue(Backend.GameData.TransactionReadV2, transactionReadList, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                 string data = string.Empty;
                 foreach (LitJson.JsonData json in result.GetReturnValuetoJSON()["Responses"])
                 {
                     data += json["inDate"]["S"].ToString() + "\n";
                 }
                 Debug.Log("불러온 데이터들의 inDate : " + data);
             });
        }
    }

    void AddSetInsert(InputField[] inputFields)
    {
        Param param = new Param();
        string tableName = inputFields[0].text;
        param.Add(inputFields[1].text, inputFields[2].text);
        param.Add(inputFields[3].text, System.Int32.Parse(inputFields[4].text));

        string methodName = MethodBase.GetCurrentMethod().Name;

        transactionWriteList.Add(TransactionValue.SetInsert(tableName, param));
        Debug.Log("TransactionValue.SetInsert 삽입 성공했습니다.");

    }

    void AddSetGetV2Indate(InputField[] inputFields)
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
        transactionReadList.Add(TransactionValue.SetGetV2(tableName, inDate, owner_inDate));
        Debug.Log("TransactionValue.SetGetV2 삽입 성공했습니다.");

    }

    void AddSetGetWhere(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string whereColumn = inputFields[1].text;
        string whereValue = inputFields[2].text;

        Where where = new Where();

        where.Equal(whereColumn, whereValue);

        transactionReadList.Add(TransactionValue.SetGet(tableName, where));
        Debug.Log("TransactionValue.SetGet 삽입 성공했습니다.");



    }



    void AddSetUpdateV2Indate(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string inDate = inputFields[1].text;
        string owner_inDate = inputFields[2].text;
        //아무것도 안쓰면 owner_indate는 자신의 inDate를 가르킨다.
        if (owner_inDate == string.Empty || owner_inDate == "")
        {
            owner_inDate = Backend.UserInDate;
        }

        string updateColumn = inputFields[3].text;
        string updateValue = inputFields[4].text;



        Param param = new Param();
        param.Add(updateColumn, updateValue);

        transactionWriteList.Add(TransactionValue.SetUpdateV2(tableName, inDate, owner_inDate, param));
        Debug.Log("TransactionValue.SetUpdateV2 삽입 성공했습니다.");


    }

    void AddSetUpdateWhere(InputField[] inputFields)
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

        transactionWriteList.Add(TransactionValue.SetUpdate(tableName, where, param));
        Debug.Log("TransactionValue.SetUpdate 삽입 성공했습니다.");

    }


    void AddSetDeleteV2Indate(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string inDate = inputFields[1].text;

        string owner_inDate = inputFields[2].text;
        //아무것도 안쓰면 owner_indate는 자신의 inDate를 가르킨다.
        if (owner_inDate == string.Empty || owner_inDate == "")
        {
            owner_inDate = Backend.UserInDate;
        }

        transactionWriteList.Add(TransactionValue.SetDeleteV2(tableName, inDate, owner_inDate));
        Debug.Log("TransactionValue.SetDeleteV2 삽입 성공했습니다.");

    }

    void AddSetDeleteWhere(InputField[] inputFields)
    {
        string tableName = inputFields[0].text;
        string whereColumn = inputFields[1].text;
        string whereValue = inputFields[2].text;

        Where where = new Where();

        where.Equal(whereColumn, whereValue);

        transactionWriteList.Add(TransactionValue.SetDelete(tableName, where));
        Debug.Log("TransactionValue.SetDelete 삽입 성공했습니다.");

    }
}
