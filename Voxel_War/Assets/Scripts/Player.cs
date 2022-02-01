using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : MonoBehaviour {
 
    Rigidbody rb;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
 
 
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
 
        Vector3 movement = new Vector3(moveHorizontal*2f, 0, moveVertical*2f);
 
        rb.AddForce(movement);
    }
}