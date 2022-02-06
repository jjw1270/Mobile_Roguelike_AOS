using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Vector3 CamRotation = new Vector3(45,-45,0);
    public Vector3 CamPosition = new Vector3(4,7,-4);

    private Transform target_ = null;

    public void SetTarget(GameObject target){
        target_ = target.transform;
    }

    void Update()
    {}
    void LateUpdate(){
        if(target_ == null)
            return;
        transform.position = target_.position + CamPosition;
        //transform.localRotation = Quaternion.Euler(CamRotation);
    }
}
