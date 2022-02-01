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
    public void ChangeButtonToGuild1()
    {
        UIManager.instance.InitButton();
        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "CreateGuildV3", new List<string>() { "string guildName", "int goodsCount", "string metaKey1", "int metaValue1" }, CreateGuildV3);
        UIManager.instance.SetFunctionButton(i++, "ApplyGuildV3", new List<string>() { "string guildInDate" }, ApplyGuildV3);
        UIManager.instance.SetFunctionButton(i++, "WithdrawGuildV3", new List<string>(), WithdrawGuildV3);
        UIManager.instance.SetFunctionButton(i++, "GetGuildListV3", new List<string>() { "int limit" }, GetGuildListV3);
        UIManager.instance.SetFunctionButton(i++, "GetMyGuildInfoV3", new List<string>(), GetMyGuildInfoV3);
        UIManager.instance.SetFunctionButton(i++, "GetGuildIndateByGuildNameV3", new List<string>() { "string guildName" }, GetGuildIndateByGuildNameV3);
        UIManager.instance.SetFunctionButton(i++, "GetGuildInfoV3", new List<string>() { "string guildIndate" }, GetGuildInfoV3);
        UIManager.instance.SetFunctionButton(i++, "GetGuildMemberListV3", new List<string>() { "string guildIndate", "int limit" }, GetGuildMemberListV3);
        UIManager.instance.SetFunctionButton(i++, "GetRandomGuildInfoV3", new List<string>() { "int limit" }, GetRandomGuildInfoV3);
        UIManager.instance.SetFunctionButton(i++, "GetRandomGuildInfoV3(Meta)", new List<string>() { "string metaKey", "int metaValue", "int gap", "int limit" }, GetRandomGuildInfoV3Meta);

    }
    void CreateGuildV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string guildName = inputFields[0].text;
        int goodsCount = Int32.Parse(inputFields[1].text);

        Param param = new Param();
        param.Add(inputFields[2].text, Int32.Parse(inputFields[3].text));


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.CreateGuildV3(guildName, goodsCount, param);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.CreateGuildV3(guildName, goodsCount, param, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.CreateGuildV3, guildName, goodsCount, param, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               });
        }
    }

    void ApplyGuildV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string guildInDate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.ApplyGuildV3(guildInDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.ApplyGuildV3(guildInDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.ApplyGuildV3, guildInDate, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               });
        }
    }

    void WithdrawGuildV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.WithdrawGuildV3();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.WithdrawGuildV3(result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

             });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.WithdrawGuildV3, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");
               });
        }
    }

    void GetGuildListV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        int limit = Int32.Parse(inputFields[0].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetGuildListV3(limit);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string guilds = string.Empty;

            //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
            foreach (LitJson.JsonData json in result.FlattenRows())
            {
                guilds += string.Format("{0} 길드의 inDate : {1}\n", json["guildName"].ToString(), json["inDate"].ToString());
            }

            if (result.HasFirstKey())
            {
                var result2 = Backend.Social.Guild.GetGuildListV3(limit, result.FirstKeystring());

                Debug.Log($"firstKey - ({backendType.ToString()}){methodName} : {result2}");
                foreach (LitJson.JsonData json in result2.FlattenRows())
                {
                    guilds += string.Format("{0} 길드의 inDate : {1}\n", json["guildName"].ToString(), json["inDate"].ToString());
                }
                Debug.Log("길드목록 : \n" + guilds);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetGuildListV3(limit, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                 string guilds = string.Empty;

                 //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                 foreach (LitJson.JsonData json in result.FlattenRows())
                 {
                     guilds += string.Format("{0} 길드의 inDate : {1}\n", json["guildName"].ToString(), json["inDate"].ToString());
                 }

                 if (result.HasFirstKey())
                 {
                     Backend.Social.Guild.GetGuildListV3(limit, result.FirstKeystring(), result2 =>
                     {
                         Debug.Log($"firstKey - ({backendType.ToString()}){methodName} : {result2}");
                         foreach (LitJson.JsonData json in result2.FlattenRows())
                         {
                             guilds += string.Format("{0} 길드의 inDate : {1}\n", json["guildName"].ToString(), json["inDate"].ToString());
                         }
                         Debug.Log("길드목록 : \n" + guilds);
                     });
                 }
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetGuildListV3, limit, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  string guilds = string.Empty;

                  //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                  foreach (LitJson.JsonData json in result.FlattenRows())
                  {
                      guilds += string.Format("{0} 길드의 inDate : {1}\n", json["guildName"].ToString(), json["inDate"].ToString());
                  }

                  if (result.HasFirstKey())
                  {
                      SendQueue.Enqueue(Backend.Social.Guild.GetGuildListV3, limit, result.FirstKeystring(), result2 =>
                      {
                          Debug.Log($"firstKey - ({backendType.ToString()}){methodName} : {result2}");
                          foreach (LitJson.JsonData json in result2.FlattenRows())
                          {
                              guilds += string.Format("{0} 길드의 inDate : {1}\n", json["guildName"].ToString(), json["inDate"].ToString());
                          }
                          Debug.Log("길드목록 : \n" + guilds);
                      });
                  }
              });
        }
    }

    void GetMyGuildInfoV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetMyGuildInfoV3();

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            // FlattenJson()은 ["S"],["N"]이 지워진 json을 반환하는 함수
            Debug.Log("내 길드의 inDate : " + result.GetFlattenJSON()["guild"]["inDate"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetMyGuildInfoV3(result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                 // FlattenJson()은 ["S"],["N"]이 지워진 json을 반환하는 함수
                 Debug.Log("내 길드의 inDate : " + result.GetFlattenJSON()["guild"]["inDate"].ToString());

             });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetMyGuildInfoV3, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                   // FlattenJson()은 ["S"],["N"]이 지워진 json을 반환하는 함수
                   Debug.Log("내 길드의 inDate : " + result.GetFlattenJSON()["guild"]["inDate"].ToString());
               });
        }
    }
    void GetGuildIndateByGuildNameV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string guildName = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetGuildIndateByGuildNameV3(guildName);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            // FlattenJson()은 ["S"],["N"]이 지워진 json을 반환하는 함수
            Debug.Log("해당 길드의 inDate : " + result.GetFlattenJSON()["guildInDate"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetGuildIndateByGuildNameV3(guildName, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  // FlattenJson()은 ["S"],["N"]이 지워진 json을 반환하는 함수
                  Debug.Log("해당 길드의 inDate : " + result.GetFlattenJSON()["guildInDate"].ToString());

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetGuildIndateByGuildNameV3, guildName, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                    // FlattenJson()은 ["S"],["N"]이 지워진 json을 반환하는 함수
                    Debug.Log("해당 길드의 inDate : " + result.GetFlattenJSON()["guildInDate"].ToString());
                });
        }
    }

    void GetGuildInfoV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string guildIndate = inputFields[0].text;

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetGuildInfoV3(guildIndate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            // FlattenJson()은 ["S"],["N"]이 지워진 json을 반환하는 함수
            Debug.Log("해당 길드의 멤버 수 : " + result.GetFlattenJSON()["memberCount"].ToString());

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetGuildInfoV3(guildIndate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  // FlattenJson()은 ["S"],["N"]이 지워진 json을 반환하는 함수
                  Debug.Log("해당 길드의 멤버 수 : " + result.GetFlattenJSON()["memberCount"].ToString());

              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetGuildInfoV3, guildIndate, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                    // FlattenJson()은 ["S"],["N"]이 지워진 json을 반환하는 함수
                    Debug.Log("해당 길드의 멤버 수 : " + result.GetFlattenJSON()["memberCount"].ToString());
                });
        }
    }


    void GetGuildMemberListV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string guildIndate = inputFields[0].text;
        int limit = Int32.Parse(inputFields[1].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetGuildMemberListV3(guildIndate, limit);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            string guilds = string.Empty;

            //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
            foreach (LitJson.JsonData json in result.FlattenRows())
            {
                guilds += string.Format("{0} 길드원의 inDate : {1}\n", json["nickname"].ToString(), json["inDate"].ToString());
            }

            if (result.HasFirstKey())
            {
                var result2 = Backend.Social.Guild.GetGuildMemberListV3(guildIndate, limit, result.FirstKeystring());

                Debug.Log($"firstKey - ({backendType.ToString()}){methodName} : {result2}");
                foreach (LitJson.JsonData json in result2.FlattenRows())
                {
                    guilds += string.Format("{0} 길드원의 inDate : {1}\n", json["nickname"].ToString(), json["inDate"].ToString());
                }
                Debug.Log("길드원 목록 : \n" + guilds);
            }

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetGuildMemberListV3(guildIndate, limit, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                 string guilds = string.Empty;

                 //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                 foreach (LitJson.JsonData json in result.FlattenRows())
                 {
                     guilds += string.Format("{0} 길드원의 inDate : {1}\n", json["nickname"].ToString(), json["inDate"].ToString());
                 }

                 if (result.HasFirstKey())
                 {
                     Backend.Social.Guild.GetGuildMemberListV3(guildIndate, limit, result.FirstKeystring(), result2 =>
                     {
                         Debug.Log($"firstKey - ({backendType.ToString()}){methodName} : {result2}");
                         foreach (LitJson.JsonData json in result2.FlattenRows())
                         {
                             guilds += string.Format("{0} 길드원의 inDate : {1}\n", json["nickname"].ToString(), json["inDate"].ToString());
                         }
                         Debug.Log("길드원 목록 : \n" + guilds);
                     });
                 }
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetGuildMemberListV3, guildIndate, limit, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  string guilds = string.Empty;

                  //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                  foreach (LitJson.JsonData json in result.FlattenRows())
                  {
                      guilds += string.Format("{0} 길드원의 inDate : {1}\n", json["nickname"].ToString(), json["inDate"].ToString());
                  }

                  if (result.HasFirstKey())
                  {
                      SendQueue.Enqueue(Backend.Social.Guild.GetGuildMemberListV3, guildIndate, limit, result.FirstKeystring(), result2 =>
                      {
                          Debug.Log($"firstKey - ({backendType.ToString()}){methodName} : {result2}");
                          foreach (LitJson.JsonData json in result2.FlattenRows())
                          {
                              guilds += string.Format("{0} 길드원의 inDate : {1}\n", json["nickname"].ToString(), json["inDate"].ToString());
                          }
                          Debug.Log("길드원 목록 : \n" + guilds);
                      });
                  }
              });
        }
    }

    void GetRandomGuildInfoV3(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        int limit = Int32.Parse(inputFields[0].text);

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetRandomGuildInfoV3(limit);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            string guilds = string.Empty;

            //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
            foreach (LitJson.JsonData json in result.FlattenRows())
            {
                guilds += string.Format("{0} 길드의 inDate : {1}\n", json["guildName"].ToString(), json["inDate"].ToString());
            }
            Debug.Log("랜덤으로 뽑힌 길드 목록 : \n" + guilds);

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetRandomGuildInfoV3(limit, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  string guilds = string.Empty;

                  //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                  foreach (LitJson.JsonData json in result.FlattenRows())
                  {
                      guilds += string.Format("{0} 길드의 inDate : {1}\n", json["guildName"].ToString(), json["inDate"].ToString());
                  }
                  Debug.Log("랜덤으로 뽑힌 길드 목록 : \n" + guilds);


              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetRandomGuildInfoV3, limit, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                    string guilds = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        guilds += string.Format("{0} 길드의 inDate : {1}\n", json["guildName"].ToString(), json["inDate"].ToString());
                    }
                    Debug.Log("랜덤으로 뽑힌 길드 목록 : \n" + guilds);
                });
        }
    }

    void GetRandomGuildInfoV3Meta(InputField[] inputFields)
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string metaKey = inputFields[0].text;
        int metaValue = Int32.Parse(inputFields[1].text);
        int gap = Int32.Parse(inputFields[2].text);
        int limit = Int32.Parse(inputFields[3].text);


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.Social.Guild.GetRandomGuildInfoV3(metaKey, metaValue, gap, limit);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");
            string guilds = string.Empty;

            //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
            foreach (LitJson.JsonData json in result.FlattenRows())
            {
                guilds += string.Format("{0} 길드의 메타 정보 {1} : {2} / inDate : {3}\n", json["guildName"].ToString(), metaKey, json[metaKey].ToString(), json["inDate"].ToString());
            }
            Debug.Log("랜덤으로 뽑힌 길드 목록 : \n" + guilds);

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Guild.GetRandomGuildInfoV3(limit, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                  string guilds = string.Empty;

                  //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                  foreach (LitJson.JsonData json in result.FlattenRows())
                  {
                      guilds += string.Format("{0} 길드의 메타 정보 {1} : {2} / inDate : {3}\n", json["guildName"].ToString(), metaKey, json[metaKey].ToString(), json["inDate"].ToString());
                  }
                  Debug.Log("랜덤으로 뽑힌 길드 목록 : \n" + guilds);


              });
        }
        else
        {
            SendQueue.Enqueue(Backend.Social.Guild.GetRandomGuildInfoV3, limit, result =>
                {
                    Debug.Log($"({backendType.ToString()}){methodName} : {result}");
                    string guilds = string.Empty;

                    //FlattenRows()는 리턴된 값중 ["S"], ["N"]을 제거하여 Json으로 리턴하는 함수입니다.
                    foreach (LitJson.JsonData json in result.FlattenRows())
                    {
                        guilds += string.Format("{0} 길드의 메타 정보 {1} : {2} / inDate : {3}\n", json["guildName"].ToString(), metaKey, json[metaKey].ToString(), json["inDate"].ToString());
                    }
                    Debug.Log("랜덤으로 뽑힌 길드 목록 : \n" + guilds);
                });
        }
    }

}
