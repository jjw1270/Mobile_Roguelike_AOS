using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
public partial class BackendManager : MonoBehaviour
{
    public static BackendManager instance;

    public Dropdown dropDown;

    public enum BackendFunctionTYPE
    {
        SYNC,
        ASYNC,
        SENDQUEUE
    }

    public BackendFunctionTYPE backendType = BackendFunctionTYPE.SYNC;

    Queue<BackendFunc> BackendFuncList = new Queue<BackendFunc>();
    // Start is called before the first frame update
    BackendReturnObject result;

    public delegate void BackendFunc();

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        var bro = Backend.Initialize(true);

        if (bro.IsSuccess())
        {
            Debug.Log("Backend Initialize 성공");
            SetDropDown();
            ChangeButtonToBMember();
        }
        else
        {
            Debug.Log("Backend Initialize 실패");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Backend.AsyncPoll();
    }

    void SetDropDown()
    {
        dropDown.onValueChanged.AddListener(SetBackendType);
    }



    public void SetBackendType(int selectValue)
    {
        switch (selectValue)
        {
            case 0:
                Debug.Log("뒤끝 함수 호출 방식을 동기로 설정하였습니다");
                backendType = BackendFunctionTYPE.SYNC;
                break;

            case 1:
                Debug.Log("뒤끝 함수 호출 방식을 비동기로 설정하였습니다");
                backendType = BackendFunctionTYPE.ASYNC;
                break;

            case 2:
                Debug.Log("뒤끝 함수 호출 방식을 SendQueue로 설정하였습니다");
                backendType = BackendFunctionTYPE.SENDQUEUE;
                break;
        }
    }

}
