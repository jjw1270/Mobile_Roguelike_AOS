using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float moveSpeed = 5;
    private Rigidbody charRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        charRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

        charRigidbody.velocity = inputDir * moveSpeed;

        transform.LookAt(transform.position + inputDir);

    }
}
