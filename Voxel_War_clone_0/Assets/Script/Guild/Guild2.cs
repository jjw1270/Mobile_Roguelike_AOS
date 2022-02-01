using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using BackEnd.GlobalSupport;
using System.Reflection;
using System;

public partial class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeButtonToGuild2()
    {
        UIManager.instance.InitButton();
        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "ModifyGuildV3", new List<string>() { "string metaKey1", "string metaValue1", "string metaKey2", "int metaValue2" }, ModifyGuildV3);
        UIManager.instance.SetFunctionButton(i++, "ContributeGoodsV3", new List<string>() { "int goodsType(1=goods1)", "int amount" }, ContributeGoodsV3);
        UIManager.instance.SetFunctionButton(i++, "GetMyGuildGoodsV3", new List<string>(), GetMyGuildGoodsV3);
        UIManager.instance.SetFunctionButton(i++, "GetGuildGoodsByIndateV3", new List<string>() { "string guildIndate" }, GetGuildGoodsByIndateV3);

    }
    void ModifyGuildV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;


        Param param = new Param();
        param.Add(inputFields[0].text, inputFields[1].text);
        param.Add(inputFields[2].text, Int32.Parse(inputFields[3].text));


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.ModifyGuildV3(param);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.ModifyGuildV3(param, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.ModifyGuildV3, param, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                });
        }
    }

    void ContributeGoodsV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        goodsType goods = (goodsType)Int32.Parse(inputFields[0].text);
        int amount = Int32.Parse(inputFields[1].text);


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.ContributeGoodsV3(goods, amount);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.ContributeGoodsV3(goods, amount, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.ContributeGoodsV3, goods, amount, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               });
        }
    }

    void GetMyGuildGoodsV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        goodsType goods = (goodsType)Int32.Parse(inputFields[0].text);
        int amount = Int32.Parse(inputFields[1].text);


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetMyGuildGoodsV3();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("굿즈1의 총 사용량 : " + result.GetFlattenJSON()["goods"]["totalGoods1Amount"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetMyGuildGoodsV3(result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  Debug.Log("굿즈1의 총 사용량 : " + result.GetFlattenJSON()["goods"]["totalGoods1Amount"].ToString());
              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetMyGuildGoodsV3, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                   Debug.Log("굿즈1의 총 사용량 : " + result.GetFlattenJSON()["goods"]["totalGoods1Amount"].ToString());

               });
        }
    }

    void GetGuildGoodsByIndateV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string guildIndate = inputFields[0].text;
        goodsType goods = (goodsType)Int32.Parse(inputFields[0].text);
        int amount = Int32.Parse(inputFields[1].text);


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetGuildGoodsByIndateV3(guildIndate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Debug.Log("굿즈1의 총 사용량 : " + result.GetFlattenJSON()["goods"]["totalGoods1Amount"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetGuildGoodsByIndateV3(guildIndate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  Debug.Log("굿즈1의 총 사용량 : " + result.GetFlattenJSON()["goods"]["totalGoods1Amount"].ToString());
              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetGuildGoodsByIndateV3, guildIndate, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                    Debug.Log("굿즈1의 총 사용량 : " + result.GetFlattenJSON()["goods"]["totalGoods1Amount"].ToString());

                });
        }
    }



}
