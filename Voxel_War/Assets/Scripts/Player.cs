using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : MonoBehaviour {
    private float moveSpeed = 0.4f;
    private float rotationSpeed = 5f;
    private Vector3 direction;
    public FloatingJoystick floatingJoystick;
    Rigidbody rb;
    Animator animator;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        rb.position += direction * moveSpeed;
        if(direction != Vector3.zero){
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            rb.rotation = Quaternion.Euler(0, angle, 0);
        }

        AnimatonUpdate();
    }

    void AnimatonUpdate(){
        if(direction == Vector3.zero){
            animator.SetBool("isRunning", false);
        }
        else{
            animator.SetBool("isRunning", true);
        }
    }
}