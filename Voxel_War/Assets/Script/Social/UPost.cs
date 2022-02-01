using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Reflection;
using System;
using UnityEngine.UI;
public partial class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void ChangeButtonToUPost()
    {
        UIManager.instance.InitButton();
        int i = 0;
        UIManager.instance.SetFunctionButton(i++, "GetPostList", new List<string>() { "PostType(Admin,Rank,User)", "int limit(default = 10)" }, GetPostList);
        UIManager.instance.SetFunctionButton(i++, "ReceivePostItem", new List<string>() { "PostType(Admin,Rank,User)", "string postIndate" }, ReceivePostItem);
        UIManager.instance.SetFunctionButton(i++, "ReceivePostItemAll", new List<string>() { "PostType(Admin,Rank,User)" }, ReceivePostAll);
        UIManager.instance.SetFunctionButton(i++, "SendUserPost", new List<string>() { "string postIndate", "string tableName", "string columnName", "string columnValue" }, SendUserPost);
        UIManager.instance.SetFunctionButton(i++, "DeleteUserPost", new List<string>() { "string postIndate" }, DeleteUserPost);

    }


    void GetPostList(InputField[] inputFields) // 10
    {
        string methodName = MethodBase.GetCurrentMethod().Name;
        string postTypeString = inputFields[0].text;

        int limit = 10;
        if (!String.IsNullOrEmpty(inputFields[1].text))
        {
            limit = Int32.Parse(inputFields[1].text);
        }

        PostType postType = PostType.Admin;
        switch (postTypeString)
        {
            case "Admin":
                postType = PostType.Admin;
                break;
            case "Rank":
                postType = PostType.Rank;
                break;
            case "User":
                postType = PostType.User;
                break;

        }

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.UPost.GetPostList(postType, limit);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            if (result.IsSuccess())
            {
                LitJson.JsonData postList = result.GetReturnValuetoJSON()["postList"];
                Debug.Log("우편 갯수 : " + postList.Count);
                for (int i = 0; i < postList.Count; i++)
                {
                    Debug.Log(string.Format("우편 제목 : {0}\n우편 내용 : {1}\n우편 inDate:{2}",
                    postList[i]["title"].ToString(), postList[i]["content"].ToString(), postList[i]["inDate"].ToString()));
                }
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.UPost.GetPostList(postType, limit, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    LitJson.JsonData postList = result.GetReturnValuetoJSON()["postList"];
                    Debug.Log("우편 갯수 : " + postList.Count);
                    for (int i = 0; i < postList.Count; i++)
                    {
                        Debug.Log(string.Format("우편 제목 : {0}\n우편 내용 : {1}\n우편 inDate:{2}",
                        postList[i]["title"].ToString(), postList[i]["content"].ToString(), postList[i]["inDate"].ToString()));
                    }
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.UPost.GetPostList, postType, limit, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                  if (result.IsSuccess())
                  {
                      LitJson.JsonData postList = result.GetReturnValuetoJSON()["postList"];
                      Debug.Log("우편 갯수 : " + postList.Count);
                      for (int i = 0; i < postList.Count; i++)
                      {
                          Debug.Log(string.Format("우편 제목 : {0}\n우편 내용 : {1}\n우편 inDate:{2}",
                          postList[i]["title"].ToString(), postList[i]["content"].ToString(), postList[i]["inDate"].ToString()));
                      }
                  }
              });
        }
    }

    void ReceivePostItem(InputField[] inputFields) // 10
    {
        string methodName = MethodBase.GetCurrentMethod().Name;


        string postTypeString = inputFields[0].text;

        PostType postType = PostType.Admin;
        switch (postTypeString)
        {
            case "Admin":
                postType = PostType.Admin;
                break;
            case "Rank":
                postType = PostType.Rank;
                break;
            case "User":
                postType = PostType.User;
                break;

        }

        string postIndate = inputFields[1].text;


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.UPost.ReceivePostItem(postType, postIndate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            if (result.IsSuccess())
            {
                LitJson.JsonData postItems = result.GetReturnValuetoJSON()["postItems"];
                Debug.Log("수령한 아이템 갯수 : " + postItems.Count);

                if (postItems.Count <= 0)
                {
                    Debug.Log("해당 우편은 아이템을 첨부하고 있지 않습니다. inDate : " + postIndate);
                }
                for (int i = 0; i < postItems.Count; i++)
                {
                    string itemInfo = string.Empty;
                    foreach (var key in postItems[i]["item"].Keys)
                    {
                        itemInfo += string.Format("{0} : {1}\n", key, postItems[i]["item"][key].ToString());
                    }
                    itemInfo += string.Format("아이템 수 : {0}", postItems[i]["itemCount"].ToString());
                    Debug.Log(itemInfo);
                }
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.UPost.ReceivePostItem(postType, postIndate, result =>
            {
                Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                if (result.IsSuccess())
                {
                    LitJson.JsonData postItems = result.GetReturnValuetoJSON()["postItems"];
                    Debug.Log("수령한 아이템 갯수 : " + postItems.Count);

                    if (postItems.Count <= 0)
                    {
                        Debug.Log("해당 우편은 아이템을 첨부하고 있지 않습니다. inDate : " + postIndate);
                    }
                    for (int i = 0; i < postItems.Count; i++)
                    {
                        string itemInfo = string.Empty;
                        foreach (var key in postItems[i]["item"].Keys)
                        {
                            itemInfo += string.Format("{0} : {1}\n", key, postItems[i]["item"][key].ToString());
                        }
                        itemInfo += string.Format("아이템 갯수 : {0}", postItems[i]["itemCount"].ToString());
                        Debug.Log(itemInfo);
                    }
                }
            });
        }
        else
        {
            SendQueue.Enqueue(Backend.UPost.ReceivePostItem, postType, postIndate, result =>
               {
                   Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                   if (result.IsSuccess())
                   {
                       LitJson.JsonData postItems = result.GetReturnValuetoJSON()["postItems"];
                       Debug.Log("수령한 아이템 갯수 : " + postItems.Count);

                       if (postItems.Count <= 0)
                       {
                           Debug.Log("해당 우편은 아이템을 첨부하고 있지 않습니다. inDate : " + postIndate);
                       }
                       for (int i = 0; i < postItems.Count; i++)
                       {
                           string itemInfo = string.Empty;
                           foreach (var key in postItems[i]["item"].Keys)
                           {
                               itemInfo += string.Format("{0} : {1}\n", key, postItems[i]["item"][key].ToString());
                           }
                           itemInfo += string.Format("아이템 갯수 : {0}", postItems[i]["itemCount"].ToString());
                           Debug.Log(itemInfo);
                       }
                   }
               });
        }
    }


    void ReceivePostAll(InputField[] inputFields) // 10
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string postTypeString = inputFields[0].text;

        PostType postType = PostType.Admin;
        switch (postTypeString)
        {
            case "Admin":
                postType = PostType.Admin;
                break;
            case "Rank":
                postType = PostType.Rank;
                break;
            case "User":
                postType = PostType.User;
                break;

        }

        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.UPost.ReceivePostItemAll(postType);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

            if (result.IsSuccess())
            {
                foreach (LitJson.JsonData postItems in result.GetReturnValuetoJSON()["postItems"])
                {
                    if (postItems.Count <= 0)
                    {
                        Debug.Log("아이템이 없는 우편 수령");
                        continue;
                    }
                    foreach (LitJson.JsonData items in postItems)
                    {
                        string itemInfo = string.Empty;

                        if (postType == PostType.User) // 유저만 리턴형식이 다름
                        {
                            foreach (var key in items.Keys)
                            {
                                itemInfo += string.Format("{0} : {1}\n", key, items[key].ToString());
                            }
                        }
                        else
                        {
                            foreach (var key in items["item"].Keys)
                            {
                                itemInfo += string.Format("{0} : {1}\n", key, items["item"][key].ToString());
                            }
                            itemInfo += string.Format("아아템 갯수 : {0}\n", items["itemCount"].ToString());
                        }
                        Debug.Log(itemInfo);
                    }
                }
            }
            else
            {
                if (result.GetErrorCode() == "NotFoundException")
                {
                    Debug.LogError("더이상 수령할 우편이 존재하지 않습니다.");
                }
            }
        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.UPost.ReceivePostItemAll(postType, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                 if (result.IsSuccess())
                 {
                     foreach (LitJson.JsonData postItems in result.GetReturnValuetoJSON()["postItems"])
                     {
                         if (postItems.Count <= 0)
                         {
                             Debug.Log("아이템이 없는 우편 수령");
                             continue;
                         }
                         foreach (LitJson.JsonData items in postItems)
                         {
                             string itemInfo = string.Empty;

                             if (postType == PostType.User) // 유저만 리턴형식이 다름
                             {
                                 foreach (var key in items.Keys)
                                 {
                                     itemInfo += string.Format("{0} : {1}\n", key, items[key].ToString());
                                 }
                             }
                             else
                             {
                                 foreach (var key in items["item"].Keys)
                                 {
                                     itemInfo += string.Format("{0} : {1}\n", key, items["item"][key].ToString());
                                 }
                                 itemInfo += string.Format("아아템 갯수 : {0}\n", items["itemCount"].ToString());
                             }
                             Debug.Log(itemInfo);
                         }
                     }
                 }
                 else
                 {
                     if (result.GetErrorCode() == "NotFoundException")
                     {
                         Debug.LogError("더이상 수령할 우편이 존재하지 않습니다.");
                     }
                 }
             });
        }
        else
        {
            SendQueue.Enqueue(Backend.UPost.ReceivePostItemAll, postType, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");

                  if (result.IsSuccess())
                  {
                      foreach (LitJson.JsonData postItems in result.GetReturnValuetoJSON()["postItems"])
                      {
                          if (postItems.Count <= 0)
                          {
                              Debug.Log("아이템이 없는 우편 수령");
                              continue;
                          }
                          foreach (LitJson.JsonData items in postItems)
                          {
                              string itemInfo = string.Empty;

                              if (postType == PostType.User) // 유저만 리턴형식이 다름
                              {
                                  foreach (var key in items.Keys)
                                  {
                                      itemInfo += string.Format("{0} : {1}\n", key, items[key].ToString());
                                  }
                              }
                              else
                              {
                                  foreach (var key in items["item"].Keys)
                                  {
                                      itemInfo += string.Format("{0} : {1}\n", key, items["item"][key].ToString());
                                  }
                                  itemInfo += string.Format("아아템 갯수 : {0}\n", items["itemCount"].ToString());
                              }
                              Debug.Log(itemInfo);
                          }
                      }
                  }
                  else
                  {
                      if (result.GetErrorCode() == "NotFoundException")
                      {
                          Debug.LogError("더이상 수령할 우편이 존재하지 않습니다.");
                      }
                  }
              });
        }
    }


    void SendUserPost(InputField[] inputFields) // 9
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;
        string tableName = inputFields[1].text;
        string columnName = inputFields[2].text;
        int columnValue = Int32.Parse(inputFields[3].text);


        Param param = new Param();
        param.Add(columnName, columnValue);
        var bro = Backend.GameData.Insert(tableName, param);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("데이터 삽입에 실패했습니다.");
            return;
        }
        PostItem postItem = new PostItem
        {
            Title = "유저 우편 발송",
            Content = "내용을 입력",
            TableName = tableName,
            RowInDate = bro.GetInDate(),
            Column = columnName
        };


        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.UPost.SendUserPost(inDate, postItem);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.Social.Post.SendPost(inDate, postItem, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

             });
        }
        else
        {
            SendQueue.Enqueue(Backend.UPost.SendUserPost, inDate, postItem, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
              });
        }
    }

    void DeleteUserPost(InputField[] inputFields) // 9
    {
        string methodName = MethodBase.GetCurrentMethod().Name;

        string inDate = inputFields[0].text;
        if (backendType == BackendFunctionTYPE.SYNC)
        {
            result = Backend.UPost.DeleteUserPost(inDate);

            Debug.Log($"({backendType.ToString()}){methodName} : {result}");

        }
        else if (backendType == BackendFunctionTYPE.ASYNC)
        {
            Backend.UPost.DeleteUserPost(inDate, result =>
             {
                 Debug.Log($"({backendType.ToString()}){methodName} : {result}");

             });
        }
        else
        {
            SendQueue.Enqueue(Backend.UPost.DeleteUserPost, inDate, result =>
              {
                  Debug.Log($"({backendType.ToString()}){methodName} : {result}");
              });
        }
    }


}