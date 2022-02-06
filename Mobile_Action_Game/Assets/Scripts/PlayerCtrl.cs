using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public FloatingJoystick floatingJoystick;
    public float moveSpeed = 10f;
    Animator anim;

    void Awake(){
        anim = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;

        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + direction);

        anim.SetBool("isMoving", direction != Vector3.zero);
    }
}
