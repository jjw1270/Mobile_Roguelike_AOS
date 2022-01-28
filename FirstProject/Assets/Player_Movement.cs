using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;

    private Player_Input player_Input;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player_Input = GetComponent<Player_Input>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()  //물리 갱신 주기에 맞춰 실행됨
    {
        Rotate();
        Move();
        playerAnimator.SetFloat("Move", player_Input.move);
    }

    private void Move()

    {
        Vector3 moveDistance = player_Input.move * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    private void Rotate()
    {
        float turn = player_Input.rotate * rotateSpeed * Time.deltaTime;
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
    }
}
