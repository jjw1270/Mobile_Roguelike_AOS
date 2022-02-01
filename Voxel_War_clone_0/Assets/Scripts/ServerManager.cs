using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class ServerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            // 초기화 성공 시 로직
            Debug.Log("초기화 성공!");
            
        }
        else
        {
            // 초기화 실패 시 로직
            Debug.LogError("초기화 실패!");
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        //Backend.AsyncPoll();
    }*/

    public void SignUp(){
        string id = "ID";   //아이디 입력란 추가할것
        string password = "PW";   //비밀번호 입력란

        var bro = Backend.BMember.CustomSignUp(id, password);
        if (bro.IsSuccess()){
            Debug.Log("회원가입 성공!");
        }
        else{
            Debug.LogError("회원가입 실패");
            Debug.LogError(bro);
        }
    }

    public void OnClickSignUpButton(){
        SignUp();
    }

}
