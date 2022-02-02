using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : MonoBehaviour {
    private float moveSpeed = 1f;
    public FloatingJoystick floatingJoystick;
    public Rigidbody rb;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        rb.position += direction * moveSpeed;
        //Debug.Log(direction);
    }
}