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
    public void ChangeButtonToCoupon()
    {
        UIManager.instance.InitButton();

        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "CouponList", new List<string>(), CouponList);
        UIManager.instance.SetFunctionButton(i++, "UseCoupon", new List<string>() { "string couponCode" }, UseCoupon);
    }

    void CouponList(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Coupon.CouponList();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Coupon.CouponList(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");

           });
        }
        else
        {
            SendQueue.Enqueue(Backend.Coupon.CouponList, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");

           });
        }

    }

    void UseCoupon(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Coupon.UseCoupon(inputFields[0].text);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string items = string.Empty;
            for (int i = 0; i < result.GetReturnValuetoJSON()["itemObject"].Count; i++)
            {
                items += "해당 아이템의 차트 이름 : " + result.GetReturnValuetoJSON()["itemObject"][i]["item"]["chartFileName"].ToString() + "\n";
            }
            Debug.Log("뽑은 아이템 정보 : " + items);
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Coupon.UseCoupon(inputFields[0].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               string items = string.Empty;
               for (int i = 0; i < result.GetReturnValuetoJSON()["itemObject"].Count; i++)
               {
                   items += "해당 아이템의 차트 이름 : " + result.GetReturnValuetoJSON()["itemObject"][i]["item"]["chartFileName"].ToString() + "\n";
               }
               Debug.Log("뽑은 아이템 정보 : " + items);

           });
        }
        else
        {
            SendQueue.Enqueue(Backend.Coupon.UseCoupon, inputFields[0].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string items = string.Empty;
                for (int i = 0; i < result.GetReturnValuetoJSON()["itemObject"].Count; i++)
                {
                    items += "해당 아이템의 차트 이름 : " + result.GetReturnValuetoJSON()["itemObject"][i]["item"]["chartFileName"].ToString() + "\n";
                }
                Debug.Log("뽑은 아이템 정보 : " + items);

            });
        }

    }
}
