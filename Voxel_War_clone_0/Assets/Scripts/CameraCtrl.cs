using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject target;

    private float offsetX = 0;
    private float offsetY = 30;
    private float offsetZ = -30;
    public float DelayTime = 0.5f;
    // Update is called once per frame
    void Update()
    {
        Vector3 FixedPos = new Vector3(
            target.transform.position.x + offsetX,
            target.transform.position.y + offsetY,
            target.transform.position.z + offsetZ);
        Debug.Log(FixedPos);
        transform.position = Vector3.Lerp(transform.position, FixedPos, Time.deltaTime * DelayTime);
    }

}
