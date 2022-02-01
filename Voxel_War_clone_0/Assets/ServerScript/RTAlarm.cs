using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using System.Reflection;

public partial class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void ChangeButtonToRTAlarm()
    {
        UIManager.instance.InitButton();

        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "Connect", new List<string>(), Connect);
        UIManager.instance.SetFunctionButton(i++, "DisConnect", new List<string>(), Connect);
        UIManager.instance.SetFunctionButton(i++, "UserIsConnectByIndate", new List<string>() { "string userIndate" }, CheckUserIsConnect);

    }

    void Connect(InputField[] inputFields)
    {
        SetHandler();
        Backend.Notification.Connect();
        Debug.Log(MethodBase.GetCurrentMethod().Name + " : " + result);

    }

    void DisConnect(InputField[] inputFields)
    {
        Backend.Notification.DisConnect();
        Debug.Log(MethodBase.GetCurrentMethod().Name + " : " + result);

    }


    void CheckUserIsConnect(InputField[] inputFields)
    {
        ////[deprecated] 5.5.1
        //Backend.Notification.CheckUserIsConnect(inputFields[0].text);
        Backend.Notification.UserIsConnectByIndate(inputFields[0].text);
    }

    void SetHandler()
    {
        Backend.Notification.OnDisConnect = (string Reason) => { Debug.Log("Result : " + Reason); };

        //친구
        Backend.Notification.OnAuthorize = (bool result, string Reason) => { Debug.Log(result + Reason + "입장"); };

        Backend.Notification.OnReceivedFriendRequest = () => { Debug.Log("친구 요청 도착"); };
        Backend.Notification.OnAcceptedFriendRequest = () => { Debug.Log("친구 요청 수락"); };
        Backend.Notification.OnRejectedFriendRequest = () => { Debug.Log("친구 요청 거절"); };

        //길드
        Backend.Notification.OnReceivedGuildApplicant = () => { Debug.Log("길드 가입 신청 도착"); };
        Backend.Notification.OnApprovedGuildJoin = () => { Debug.Log("길드 가입 신청 수락"); };
        Backend.Notification.OnRejectedGuildJoin = () => { Debug.Log("길드 가입 신청 거절"); };

        //쪽지 우편
        Backend.Notification.OnReceivedMessage = () => { Debug.Log("새 쪽지 도착"); };
        Backend.Notification.OnReceivedUserPost = () => { Debug.Log("새 유저 우편 도착"); };

        Backend.Notification.OnFriendConnected = (string inDate, string nickname) => { Debug.Log($"{nickname}({inDate})님이 연결되었습니다"); };
        Backend.Notification.OnFriendDisconnected = (string inDate, string nickname) => { Debug.Log($"{nickname}({inDate})님이 연결해제되었습니다"); };
        Backend.Notification.OnIsConnectUser = (bool isConnect, string nickName, string gamerIndate) => { Debug.Log($"{nickName}({gamerIndate}) 님의 접속 여부 : {isConnect}"); };
        //[deprecated] 5.5.1 Backend.Notification.OnIsConnect = (bool isConnect) => { Debug.Log(isConnect ? "현재 접속중입니다." : "접속되어 있지 않습니다."); };

        Debug.Log("실시간 알림 핸들러가 설정되었습니다");
    }


}