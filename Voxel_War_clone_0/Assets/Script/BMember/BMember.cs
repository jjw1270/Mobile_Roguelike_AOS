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

    public void ChangeButtonToBMember()
    {
        UIManager.instance.InitButton();

        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "CustomSignUp", new List<string>() { "string id", "string password", "string etc" }, CustomSignUp);
        UIManager.instance.SetFunctionButton(i++, "CustomLogin", new List<string>() { "string id", "string pw", "string etc" }, CustomLogin);
        UIManager.instance.SetFunctionButton(i++, "UpdateCustomEmail", new List<string>() { "string emailAddress" }, UpdateCustomEmail);
        UIManager.instance.SetFunctionButton(i++, "FindCustomID", new List<string>() { "string emailAddress" }, FindCustomID);
        UIManager.instance.SetFunctionButton(i++, "ConfirmCustomPassword", new List<string>() { "string password" }, ConfirmCustomPassword);
        UIManager.instance.SetFunctionButton(i++, "ResetPassword", new List<string>() { "string customGamerId", "string emailAddress" }, ResetPassword);
        UIManager.instance.SetFunctionButton(i++, "UpdatePassword", new List<string>() { "string oldPassword", "string newPassWord" }, UpdatePassword);
        UIManager.instance.SetFunctionButton(i++, "GuestLogin", new List<string>(), GuestLogin);
        UIManager.instance.SetFunctionButton(i++, "GetGuestId & DeleteGuestInfo", new List<string>(), GetGuestIdAndDeleteGuestInfo);
    }

    //커스텀 회원가입(string id, string password, string etc)
    void CustomSignUp(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.CustomSignUp(inputFields[0].text, inputFields[1].text, inputFields[2].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.CustomSignUp(inputFields[0].text, inputFields[1].text, inputFields[2].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.CustomSignUp, inputFields[0].text, inputFields[1].text, inputFields[2].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
    }

    void CustomLogin(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.CustomLogin(inputFields[0].text, inputFields[1].text, inputFields[2].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.CustomLogin(inputFields[0].text, inputFields[1].text, inputFields[2].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.CustomLogin, inputFields[0].text, inputFields[1].text, inputFields[2].text, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            });
        }
    }

    void UpdateCustomEmail(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.UpdateCustomEmail(inputFields[0].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.UpdateCustomEmail(inputFields[0].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.UpdateCustomEmail, inputFields[0].text, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }

    }


    void FindCustomID(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.FindCustomID(inputFields[0].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.FindCustomID(inputFields[0].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.FindCustomID, inputFields[0].text, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }

    }
    void ConfirmCustomPassword(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.ConfirmCustomPassword(inputFields[0].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.ConfirmCustomPassword(inputFields[0].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.ConfirmCustomPassword, inputFields[0].text, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }

    }
    void ResetPassword(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.ResetPassword(inputFields[0].text, inputFields[1].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.ResetPassword(inputFields[0].text, inputFields[1].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.ResetPassword, inputFields[0].text, inputFields[1].text, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }

    }
    void UpdatePassword(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.UpdatePassword(inputFields[0].text, inputFields[1].text);
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.UpdatePassword(inputFields[0].text, inputFields[1].text, result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.UpdatePassword, inputFields[0].text, inputFields[1].text, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }

    void GuestLogin(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.BMember.GuestLogin();
            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.BMember.GuestLogin(result =>
           {
               Debug.Log($"({backendType.ToString()}){methodName} : {result}");
           });
        }
        else
        {
            SendQueue.Enqueue(Backend.BMember.GuestLogin, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
             });
        }
    }

    void GetGuestIdAndDeleteGuestInfo(InputField[] inputFields)
    {
        string methodName = "GetGuestId";

        string id = Backend.BMember.GetGuestID();

        if (id == string.Empty)
        {
            Debug.Log($"{methodName} : 게스트 아이디 : {id}");

        }
        else
        {
            //제거를 원하지 않을 경우 해당 if문 제거
            methodName = "DeleteGuestInfo";

            Debug.Log($"{methodName} : 게스트 아이디 : {id}를 제거합니다.");
            Backend.BMember.DeleteGuestInfo();
        }

    }

}
