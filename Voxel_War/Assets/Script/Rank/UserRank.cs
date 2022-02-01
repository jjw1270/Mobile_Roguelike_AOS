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
    public void ChangeButtonToUserRank()
    {
        UIManager.instance.InitButton();

        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "GetRankTableList", new List<string>() { "int limit", "string offset" }, GetRankTableList);
        UIManager.instance.SetFunctionButton(i++, "UpdateUserScore", new List<string>() { "string rankUuid", "string tableName", "string rowIndate", "string updateColumn", "int updateValue" }, UpdateUserScore);
        UIManager.instance.SetFunctionButton(i++, "GetRankList", new List<string>() { "string rankUuid", "int limit", "int offset" }, GetRankList);
        UIManager.instance.SetFunctionButton(i++, "GetMyRank", new List<string>() { "string rankUuid", "int gap" }, GetMyRank);
        UIManager.instance.SetFunctionButton(i++, "GetUserRank", new List<string>() { "string rankUuid", "string userInDate", "int gap" }, GetUserRank);
        UIManager.instance.SetFunctionButton(i++, "GetRankListByScore", new List<string>() { "string rankUuid", "int score" }, GetRankListByScore);
        UIManager.instance.SetFunctionButton(i++, "GetRankRewardList", new List<string>() { "string rankUuid" }, GetRankRewardList);

    }

    void GetRankTableList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.User.GetRankTableList();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string uuids = string.Empty;
            foreach (LitJson.JsonData json in result.Rows())
            {
                uuids += string.Format("랭킹 이름 : {0} / uuid : {1}\n", json["title"]["S"].ToString(), json["uuid"]["S"].ToString());
            }
            Debug.Log("랭킹 목록 : " + uuids);
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.User.GetRankTableList(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");

               string uuids = string.Empty;
               foreach (LitJson.JsonData json in result.Rows())
               {
                   uuids += string.Format("랭킹 이름 : {0} / uuid : {1}\n", json["title"]["S"].ToString(), json["uuid"]["S"].ToString());
               }
               Debug.Log("랭킹 목록 : " + uuids);

           });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.User.GetRankTableList, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string uuids = string.Empty;
                foreach (LitJson.JsonData json in result.Rows())
                {
                    uuids += string.Format("랭킹 이름 : {0} / uuid : {1}\n", json["title"]["S"].ToString(), json["uuid"]["S"].ToString());
                }
                Debug.Log("랭킹 목록 : " + uuids);
            });
        }
    }

    void UpdateUserScore(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        string tableName = inputFields[1].text;
        string rowIndate = inputFields[2].text;
        string paramName = inputFields[3].text;
        int paramValue = Int32.Parse(inputFields[4].text);

        Param param = new Param();
        param.Add(paramName, paramValue);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.User.UpdateUserScore(rankUuid, tableName, rowIndate, param);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.User.UpdateUserScore(rankUuid, tableName, rowIndate, param, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.User.UpdateUserScore, rankUuid, tableName, rowIndate, param, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
    }

    void GetRankList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        int limit = Int32.Parse(inputFields[1].text);
        int offset = Int32.Parse(inputFields[2].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.User.GetRankList(rankUuid, limit, offset);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            if (result.IsSuccess())
            {
                string ranks = string.Empty;

                //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                }
                Debug.Log("랭커 목록 : \n" + ranks);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.User.GetRankList(rankUuid, limit, offset, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               if (result.IsSuccess())
               {
                   string ranks = string.Empty;

                   //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                   foreach (LitJson.JsonData json in result.FlattenRows())
                   {
                       ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                   }
                   Debug.Log("랭커 목록 : \n" + ranks);
               }
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.User.GetRankList, rankUuid, limit, offset, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                    }
                    Debug.Log("랭커 목록 : \n" + ranks);
                }
            });
        }
    }

    void GetMyRank(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        int gap = Int32.Parse(inputFields[1].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.User.GetMyRank(rankUuid, gap);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            if (result.IsSuccess())
            {
                string ranks = string.Empty;

                //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                }
                Debug.Log("랭커 목록 : \n" + ranks);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.User.GetMyRank(rankUuid, gap, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               if (result.IsSuccess())
               {
                   string ranks = string.Empty;

                   //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                   foreach (LitJson.JsonData json in result.FlattenRows())
                   {
                       ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                   }
                   Debug.Log("랭커 목록 : \n" + ranks);
               }
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.User.GetMyRank, rankUuid, gap, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                    }
                    Debug.Log("랭커 목록 : \n" + ranks);
                }
            });
        }
    }


    void GetUserRank(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        string userIndate = inputFields[1].text;

        int gap = Int32.Parse(inputFields[2].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.User.GetUserRank(rankUuid, userIndate, gap);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            if (result.IsSuccess())
            {
                string ranks = string.Empty;

                //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                }
                Debug.Log("랭커 목록 : \n" + ranks);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.User.GetUserRank(rankUuid, userIndate, gap, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                if (result.IsSuccess())
                {
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                    }
                    Debug.Log("랭커 목록 : \n" + ranks);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.User.GetUserRank, rankUuid, userIndate, gap, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                    }
                    Debug.Log("랭커 목록 : \n" + ranks);
                }
            });
        }
    }


    void GetRankListByScore(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;

        int score = Int32.Parse(inputFields[1].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.User.GetRankListByScore(rankUuid, score);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            if (result.IsSuccess())
            {
                if (result.Rows().Count <= 0)
                {
                    Debug.Log("해당 랭킹에 유저가 존재하지 않습니다.");
                    return;
                }
                string ranks = string.Empty;

                //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                }
                Debug.Log("랭커 목록 : \n" + ranks);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.User.GetRankListByScore(rankUuid, score, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                if (result.IsSuccess())
                {
                    if (result.Rows().Count <= 0)
                    {
                        Debug.Log("해당 랭킹에 유저가 존재하지 않습니다.");
                        return;
                    }
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                    }
                    Debug.Log("랭커 목록 : \n" + ranks);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.User.GetRankListByScore, rankUuid, score, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    if (result.Rows().Count <= 0)
                    {
                        Debug.Log("해당 랭킹에 유저가 존재하지 않습니다.");
                        return;
                    }
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["nickname"].ToString());
                    }
                    Debug.Log("랭커 목록 : \n" + ranks);
                }
            });
        }
    }


    void GetRankRewardList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.User.GetRankRewardList(rankUuid);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            if (result.IsSuccess())
            {
                string ranks = string.Empty;

                //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    ranks += string.Format("{0}위 ~ {1}점까지 받을 아이템 : {2}\n", json["startRank"].ToString(), json["endRank"].ToString(), json["rewardItems"].ToString());
                }
                Debug.Log("랭크 보상 목록 : \n" + ranks);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.User.GetRankRewardList(rankUuid, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                if (result.IsSuccess())
                {

                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 ~ {1}점까지 받을 아이템 : {2}\n", json["startRank"].ToString(), json["endRank"].ToString(), json["rewardItems"].ToString());
                    }
                    Debug.Log("랭크 보상 목록 : \n" + ranks);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.User.GetRankRewardList, rankUuid, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 ~ {1}점까지 받을 아이템 : {2}\n", json["startRank"].ToString(), json["endRank"].ToString(), json["rewardItems"].ToString());
                    }
                    Debug.Log("랭크 보상 목록 : \n" + ranks);
                }
            });
        }
    }

}
