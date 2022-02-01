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
    public void ChangeButtonToGuildRank()
    {
        UIManager.instance.InitButton();

        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "GetRankTableList", new List<string>() { "int limit", "string offset" }, GetRankTableListGuild);
        UIManager.instance.SetFunctionButton(i++, "UpdateGuildMetaData", new List<string>() { "string rankUuid", "string metaKey", "int value" }, UpdateGuildMetaData);
        UIManager.instance.SetFunctionButton(i++, "ContributeGuildGoods", new List<string>() { "string rankUuid", "int goodsType(1=goods1)", "int value" }, ContributeGuildGoods);
        UIManager.instance.SetFunctionButton(i++, "UseGuildGoods", new List<string>() { "string rankUuid", "int goodsType(1=goods1)", "int value" }, UseGuildGoods);
        UIManager.instance.SetFunctionButton(i++, "GetRankList", new List<string>() { "string rankUuid", "int limit", "int offset" }, GetRankListGuild);
        UIManager.instance.SetFunctionButton(i++, "GetMyGuildRank", new List<string>() { "string rankUuid", "int gap" }, GetMyGuildRank);
        UIManager.instance.SetFunctionButton(i++, "GetGuildRank", new List<string>() { "string rankUuid", "string guildIndate", "int gap" }, GetGuildRank);
        UIManager.instance.SetFunctionButton(i++, "GetRankListByScore", new List<string>() { "string rankUuid", "int score" }, GetRankListByScoreGuild);
        UIManager.instance.SetFunctionButton(i++, "GetRankRewardList", new List<string>() { "string rankUuid" }, GetRankRewardListGuild);

    }

    void GetRankTableListGuild(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.Guild.GetRankTableList();

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
            Backend.URank.Guild.GetRankTableList(result =>
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
            SendQueue.Enqueue(Backend.URank.Guild.GetRankTableList, result =>
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

    void UpdateGuildMetaData(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        string metaKey = inputFields[1].text;
        int metaValue = Int32.Parse(inputFields[2].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.Guild.UpdateGuildMetaData(rankUuid, metaKey, metaValue);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.Guild.UpdateGuildMetaData(rankUuid, metaKey, metaValue, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.Guild.UpdateGuildMetaData, rankUuid, metaKey, metaValue, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }

    void ContributeGuildGoods(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        goodsType goods = (goodsType)Int32.Parse(inputFields[1].text);
        int amount = Int32.Parse(inputFields[2].text);


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.Guild.ContributeGuildGoods(rankUuid, goods, amount);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.Guild.ContributeGuildGoods(rankUuid, goods, amount, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.Guild.ContributeGuildGoods, rankUuid, goods, amount, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }

    void UseGuildGoods(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        goodsType goods = (goodsType)Int32.Parse(inputFields[1].text);
        int amount = Int32.Parse(inputFields[2].text);


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.Guild.UseGuildGoods(rankUuid, goods, amount);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.Guild.UseGuildGoods(rankUuid, goods, amount, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.Guild.UseGuildGoods, rankUuid, goods, amount, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }

    void GetRankListGuild(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        int limit = Int32.Parse(inputFields[1].text);
        int offset = Int32.Parse(inputFields[2].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.Guild.GetRankList(rankUuid, limit, offset);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            if (result.IsSuccess())
            {
                string ranks = string.Empty;

                //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                }
                Debug.Log("길드 랭커 목록 : \n" + ranks);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.Guild.GetRankList(rankUuid, limit, offset, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               if (result.IsSuccess())
               {
                   string ranks = string.Empty;

                   //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                   foreach (LitJson.JsonData json in result.FlattenRows())
                   {
                       ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                   }
                   Debug.Log("길드 랭커 목록 : \n" + ranks);
               }
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.Guild.GetRankList, rankUuid, limit, offset, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                    }
                    Debug.Log("길드 랭커 목록 : \n" + ranks);
                }
            });
        }
    }

    void GetMyGuildRank(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        int gap = Int32.Parse(inputFields[1].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.Guild.GetMyGuildRank(rankUuid, gap);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            if (result.IsSuccess())
            {
                string ranks = string.Empty;

                //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                }
                Debug.Log("길드 랭커 목록 : \n" + ranks);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.Guild.GetMyGuildRank(rankUuid, gap, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               if (result.IsSuccess())
               {
                   string ranks = string.Empty;

                   //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                   foreach (LitJson.JsonData json in result.FlattenRows())
                   {
                       ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                   }
                   Debug.Log("길드 랭커 목록 : \n" + ranks);
               }
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.Guild.GetMyGuildRank, rankUuid, gap, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                    }
                    Debug.Log("길드 랭커 목록 : \n" + ranks);
                }
            });
        }
    }


    void GetGuildRank(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;
        string guildIndate = inputFields[1].text;

        int gap = Int32.Parse(inputFields[2].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.Guild.GetGuildRank(rankUuid, guildIndate, gap);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            if (result.IsSuccess())
            {
                string ranks = string.Empty;

                //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                }
                Debug.Log("길드 랭커 목록 : \n" + ranks);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.Guild.GetGuildRank(rankUuid, guildIndate, gap, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                if (result.IsSuccess())
                {
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                    }
                    Debug.Log("길드 랭커 목록 : \n" + ranks);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.Guild.GetGuildRank, rankUuid, guildIndate, gap, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    string ranks = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                    }
                    Debug.Log("길드 랭커 목록 : \n" + ranks);
                }
            });
        }
    }


    void GetRankListByScoreGuild(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;

        int score = Int32.Parse(inputFields[1].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.Guild.GetRankListByScore(rankUuid, score);

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
                    ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                }
                Debug.Log("길드 랭커 목록 : \n" + ranks);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.URank.Guild.GetRankListByScore(rankUuid, score, result =>
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
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                    }
                    Debug.Log("길드 랭커 목록 : \n" + ranks);
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.URank.Guild.GetRankListByScore, rankUuid, score, result =>
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
                        ranks += string.Format("{0}위 {1}점 이름 : {2}\n", json["rank"].ToString(), json["score"].ToString(), json["guildName"].ToString());
                    }
                    Debug.Log("길드 랭커 목록 : \n" + ranks);
                }
            });
        }
    }


    void GetRankRewardListGuild(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string rankUuid = inputFields[0].text;


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.URank.Guild.GetRankRewardList(rankUuid);

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
            Backend.URank.Guild.GetRankRewardList(rankUuid, result =>
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
            SendQueue.Enqueue(Backend.URank.Guild.GetRankRewardList, rankUuid, result =>
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
