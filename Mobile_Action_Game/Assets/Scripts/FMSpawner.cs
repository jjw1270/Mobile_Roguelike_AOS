using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMSpawner : MonoBehaviour
{
    public GameObject[] fieldMonsters;
    void Start()
    {
        for(int i = 0; i<transform.childCount; i++){
            GameObject fieldMonster = Instantiate(fieldMonsters[Random.Range(0, fieldMonsters.Length)],
                transform.GetChild(i).position, transform.GetChild(i).rotation);
        }
    }
}
