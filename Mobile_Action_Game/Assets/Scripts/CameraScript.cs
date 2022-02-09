using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 CamRotation = new Vector3(45,-45,0);
    public Vector3 CamPosition = new Vector3(0,7,-3);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + CamPosition;
    }
}
