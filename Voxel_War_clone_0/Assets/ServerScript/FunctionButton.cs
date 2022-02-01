using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FunctionButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public GameObject inputManager;

    int parametersMax = 5;
    public void Init()
    {
        parametersMax = 5;

        button = GetComponentInChildren<Button>();

        for (int i = 0; i < parametersMax; i++)
        {
            inputManager = GetComponentInChildren<GridLayoutGroup>().gameObject;

            inputManager.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    public void SetButton(string buttonName, List<string> parameters, System.Action<InputField[]> callback)
    {
        button.GetComponentInChildren<Text>().text = buttonName;

        for (int i = 0; i < parameters.Count; i++)
        {
            inputManager.transform.GetChild(i).gameObject.SetActive(true);
            inputManager.transform.GetChild(i).GetComponentInChildren<Text>().text = parameters[i];
        }

        for (int i = parameters.Count; i < parametersMax; i++)
        {
            inputManager.transform.GetChild(i).gameObject.SetActive(false);

        }


        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() => callback(inputManager.GetComponentsInChildren<InputField>()));

    }
}
