using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class LoginField : MonoBehaviour
{
    public InputField idInput;
    public InputField myIndateInput;

    public InputField userNicknameInput;

    public InputField userIndateInput;

    public Text nickname;

    public void SignInAndLogin()
    {
        // Debug.Log(Backend.BMember.Logout());

        var result = Backend.BMember.CustomLogin(idInput.text, idInput.text);

        if (result.IsSuccess())
        {
            Debug.Log("CustomLogin : " + result);
            GetNickName();
            GetIndate();
        }
        else
        {
            Debug.Log("로그인에 실패하셨습니다\n회원가입을 시도합니다 : " + result);

            result = Backend.BMember.CustomSignUp(idInput.text, idInput.text);

            Debug.Log("CustomSignUp : " + result);

            Debug.Log("닉네임 업데이트" + Backend.BMember.UpdateNickname(idInput.text));

            nickname.text = idInput.text;
        }
    }

    public void LogOut()
    {
        Debug.Log(Backend.BMember.Logout());
    }

    public void AutoLogin()
    {
        try
        {
            Debug.Log(Backend.BMember.Logout());
        }
        catch
        {

        }
        Debug.Log(Backend.BMember.LoginWithTheBackendToken());
        GetNickName();
    }

    void GetNickName()
    {
        nickname.text = Backend.UserNickName;
    }
    void GetIndate()
    {
        myIndateInput.text = Backend.UserInDate;
    }

    public void SearchUser()
    {
        Backend.Social.GetUserInfoByNickName(userNicknameInput.text, callback =>
        {
            Debug.Log("검색 결과 : " + callback);
            userIndateInput.text = callback.GetReturnValuetoJSON()["row"]["inDate"].ToString();
        });

    }
}
