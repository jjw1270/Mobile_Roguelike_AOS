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
    public void ChangeButtonToGuild3()
    {
        UIManager.instance.InitButton();
        int i = 0;

        UIManager.instance.SetFunctionButton(i++, "NominateMasterV3", new List<string>() { "string userIndate" }, NominateMasterV3);
        UIManager.instance.SetFunctionButton(i++, "NominateViceMasterV3", new List<string>() { "string userIndate" }, NominateViceMasterV3);
        UIManager.instance.SetFunctionButton(i++, "ReleaseViceMasterV3", new List<string>() { "string userIndate" }, ReleaseViceMasterV3);
        UIManager.instance.SetFunctionButton(i++, "UseGoodsV3", new List<string>() { "int goodsType(1=goods1)", "int amount(< 0)" }, UseGoodsV3);
        UIManager.instance.SetFunctionButton(i++, "SetRegistrationValueV3", new List<string>() { "string true/false" }, SetRegistrationValueV3);
        UIManager.instance.SetFunctionButton(i++, "UpdateCountryCodeV3", new List<string>() { "int countryCode(KR=21)" }, UpdateCountryCodeV3);
        UIManager.instance.SetFunctionButton(i++, "GetApplicantsV3", new List<string>() { "int limit", "int offset" }, GetApplicantsV3);
        UIManager.instance.SetFunctionButton(i++, "ApproveApplicantV3", new List<string>() { "string gamerIndate" }, ApproveApplicantV3);
        UIManager.instance.SetFunctionButton(i++, "RejectApplicantV3", new List<string>() { "string gamerIndate" }, RejectApplicantV3);
        UIManager.instance.SetFunctionButton(i++, "ExpelMemberV3", new List<string>() { "string gamerIndate" }, ExpelMemberV3);

    }
    void NominateMasterV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string gamerIndate = inputFields[0].text;


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.NominateMasterV3(gamerIndate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.NominateMasterV3(gamerIndate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.NominateMasterV3, gamerIndate, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                });
        }
    }

    void NominateViceMasterV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string gamerIndate = inputFields[0].text;


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.NominateViceMasterV3(gamerIndate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.NominateViceMasterV3(gamerIndate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.NominateViceMasterV3, gamerIndate, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                });
        }
    }


    void ReleaseViceMasterV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string gamerIndate = inputFields[0].text;


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.ReleaseViceMasterV3(gamerIndate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.ReleaseViceMasterV3(gamerIndate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.ReleaseViceMasterV3, gamerIndate, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                });
        }
    }


    void UseGoodsV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        goodsType goods = (goodsType)Int32.Parse(inputFields[0].text);
        int amount = Int32.Parse(inputFields[1].text);

        if (amount > 0)
        {
            Debug.Log("굿즈의 사용은 음수만 허용됩니다. ");
            return;
        }


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.UseGoodsV3(goods, amount);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.UseGoodsV3(goods, amount, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.UseGoodsV3, goods, amount, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               });
        }
    }


    void SetRegistrationValueV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        bool isSetReg = false;

        if (inputFields[0].text.Contains("true"))
        {
            isSetReg = true;
        }


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.SetRegistrationValueV3(isSetReg);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.SetRegistrationValueV3(isSetReg, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.SetRegistrationValueV3, isSetReg, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               });
        }
    }


    void UpdateCountryCodeV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        CountryCode cd = (CountryCode)Int32.Parse(inputFields[0].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.UpdateCountryCodeV3(cd);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.UpdateCountryCodeV3(cd, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.UpdateCountryCodeV3, cd, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               });
        }
    }

    void GetApplicantsV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        int limit = Int32.Parse(inputFields[0].text);
        int offset = Int32.Parse(inputFields[1].text);


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetApplicantsV3(limit, offset);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string users = string.Empty;

            foreach (LitJson.JsonData json in result.FlattenRows())
            {
                users += string.Format("이름 : {0}/ inDate : {1}", json["inDate"].ToString(), json["nickname"].ToString());
            }
            Debug.Log("신청한 유저 : \n" + users);

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetApplicantsV3(limit, offset, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string users = string.Empty;

                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    users += string.Format("이름 : {0}/ inDate : {1}", json["inDate"].ToString(), json["nickname"].ToString());
                }
                Debug.Log("신청한 유저 : \n" + users);

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetApplicantsV3, limit, offset, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                string users = string.Empty;

                foreach (LitJson.JsonData json in result.FlattenRows())
                {
                    users += string.Format("이름 : {0}/ inDate : {1}", json["inDate"].ToString(), json["nickname"].ToString());
                }
                Debug.Log("신청한 유저 : \n" + users);
            });
        }
    }

    void ApproveApplicantV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string gamerIndate = inputFields[0].text;


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.ApproveApplicantV3(gamerIndate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.ApproveApplicantV3(gamerIndate, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.ApproveApplicantV3, gamerIndate, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
    }


    void RejectApplicantV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string gamerIndate = inputFields[0].text;


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.RejectApplicantV3(gamerIndate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.RejectApplicantV3(gamerIndate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.RejectApplicantV3, gamerIndate, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                });
        }
    }


    void ExpelMemberV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string gamerIndate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.ExpelMemberV3(gamerIndate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.ExpelMemberV3(gamerIndate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.ExpelMemberV3, gamerIndate, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                });
        }
    }

}
