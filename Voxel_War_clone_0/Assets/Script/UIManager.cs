using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
public class UIManager : MonoBehaviour
{

    public GameObject FunctionGroup;

    [HideInInspector] public static UIManager instance;

    [HideInInspector] public FunctionButton[] FunctionObjects;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        FunctionObjects = FunctionGroup.GetComponentsInChildren<FunctionButton>();
        SetFunctionGroups();
    }


    // public void InitButton()
    // {
    //     for (int i = 0; i < 18; i++)
    //     {
    //         func[i] = null;
    //         ButtonGroup.transform.GetChild(i).gameObject.SetActive(true);
    //     }
    // }
    public void InitButton()
    {

        for (int i = 0; i < FunctionObjects.Length; i++)
        {
            FunctionObjects[i].gameObject.SetActive(false);

        }
    }

    void SetFunctionGroups()
    {
        for (int i = 0; i < 11; i++)
        {
            FunctionObjects[i].GetComponent<FunctionButton>().Init();
        }
    }

    public void SetFunctionButton(int num, string buttonName, List<string> parameterList, System.Action<InputField[]> callback)
    {
        FunctionObjects[num].gameObject.SetActive(true);
        FunctionObjects[num].GetComponent<FunctionButton>().SetButton(buttonName, parameterList, callback);
    }
}
