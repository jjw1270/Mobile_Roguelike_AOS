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

    public void ChangeButtonToBMember2()
    {
        UIManager.instance.InitButton();

        int i = 0;

        UIManager.instance.SetFunctionButton(i++, "LoginWithTheBackendToken", new List<string>(), LoginWithTheBackendToken);
        UIManager.instance.SetFunctionButton(i++, "RefreshTheBackendToken", new List<string>(), RefreshTheBackendToken);
        UIManager.instance.SetFunctionButton(i++, "IsAccessTokenAlive", new List<string>(), IsAccessTokenAlive);
        UIManager.instance.SetFunctionButton(i++, "GetUserInfo", new List<string>(), GetUserInfo);
        UIManager.instance.SetFunctionButton(i++, "UpdateNickname", new List<string>() { "string nickname" }, UpdateNickname);
        UIManager.instance.SetFunctionButton(i++, "CheckNicknameDuplication", new List<string>() { "string nickname" }, CheckNicknameDuplication);
        UIManager.instance.SetFunctionButton(i++, "UpdateCountryCode", new List<string>() { "CountryCode code = SoutKorea" }, UpdateCountryCode);
        UIManager.instance.SetFunctionButton(i++, "GetMyCountryCode", new List<string>(), GetMyCountryCode);
        UIManager.instance.SetFunctionButton(i++, "GetCountryCodeByIndate", new List<string>() { "string otherUserIndate" }, GetCountryCodeByIndate);
        UIManager.instance.SetFunctionButton(i++, "Logout", new List<string>(), Logout);
        UIManager.instance.SetFunctionButton(i++, "SignOut", new List<string>(), SignOut);
    }


    //토큰을 이용하여 로그인
    void LoginWithTheBackendToken(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.LoginWithTheBackendToken();
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.LoginWithTheBackendToken(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.LoginWithTheBackendToken, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
    }

    void RefreshTheBackendToken(InputField[] inputFields) // 
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.RefreshTheBackendToken();
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.RefreshTheBackendToken(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.RefreshTheBackendToken, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
    }

    void IsAccessTokenAlive(InputField[] inputFields) // 뒤끝 AccessToken이 유효한지 확인
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.IsAccessTokenAlive();
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.IsAccessTokenAlive(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.IsAccessTokenAlive, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
    }


    void GetUserInfo(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.GetUserInfo();
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            //로그인한 유저의 닉네임 가져오기
            string nickname = result.GetReturnValuetoJSON()["row"]["nickname"].ToString();
            Debug.Log("내 닉네임 : " + nickname);

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.GetUserInfo(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");

               //로그인한 유저의 닉네임 가져오기
               string nickname = result.GetReturnValuetoJSON()["row"]["nickname"].ToString();
               Debug.Log("내 닉네임 : " + nickname);

           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.GetUserInfo, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                //로그인한 유저의 닉네임 가져오기
                string nickname = result.GetReturnValuetoJSON()["row"]["nickname"].ToString();
                Debug.Log("내 닉네임 : " + nickname);
            });
        }
    }

    void UpdateNickname(InputField[] inputFields) // CreateName과 동급
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.UpdateNickname(inputFields[0].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.UpdateNickname(inputFields[0].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.UpdateNickname, inputFields[0].text, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }

    }

    void CheckNicknameDuplication(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.CheckNicknameDuplication(inputFields[0].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.CheckNicknameDuplication(inputFields[0].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.CheckNicknameDuplication, inputFields[0].text, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }



    void UpdateCountryCode(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.UpdateCountryCode(CountryCode.SouthKorea);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.UpdateCountryCode(CountryCode.SouthKorea, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.UpdateCountryCode, CountryCode.SouthKorea, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }

    }

    void GetMyCountryCode(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.GetMyCountryCode();
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string country = result.GetReturnValuetoJSON()["country"]["S"].ToString();
            Debug.Log("내 국가코드 : " + country);
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.GetMyCountryCode(result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string country = result.GetReturnValuetoJSON()["country"]["S"].ToString();
                Debug.Log("내 국가코드 : " + country);

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.GetMyCountryCode, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                 //국가코드 string으로 변환
                 string country = result.GetReturnValuetoJSON()["country"]["S"].ToString();
                 Debug.Log("내 국가코드 : " + country);
             });
        }


    }

    void GetCountryCodeByIndate(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.GetCountryCodeByIndate(inputFields[0].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string country = result.GetReturnValuetoJSON()["country"]["S"].ToString();
            Debug.Log("내 국가코드 : " + country);
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.GetCountryCodeByIndate(inputFields[0].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                string country = result.GetReturnValuetoJSON()["country"]["S"].ToString();
                Debug.Log("내 국가코드 : " + country);

            });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.GetCountryCodeByIndate, inputFields[0].text, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                 //국가코드 string으로 변환
                 string country = result.GetReturnValuetoJSON()["country"]["S"].ToString();
                 Debug.Log("내 국가코드 : " + country);
             });
        }

    }



    void Logout(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.Logout();
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.Logout(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.Logout, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }

    }

    //회원탈퇴 : 7일이 지나야 회원탈퇴가 진행되며 어플을 종료한 후 다시 실행해야 기기에 저장된 액세스 토큰이 제거됩니다.
    void SignOut(InputField[] inputFields) // 탈퇴
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.SignOut();
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            Application.Quit();

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.SignOut(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               Application.Quit();

           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.SignOut, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                Application.Quit();

            });
        }

    }
}
