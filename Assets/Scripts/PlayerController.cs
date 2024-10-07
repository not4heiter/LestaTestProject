using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float jumpForce = 5f;
    public float rotateSpeed = 15f;
    public CharacterController controller;

    private float verticalVelocity;
    private Vector3 moveDirection;
    public float gravityScale = 2f;

    public Animator anim;
    public Transform pivot;

    public GameObject playerModel;

    public bool canMove = true;

    void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (canMove)
        {
            float yStore = verticalVelocity;
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            verticalVelocity = yStore;

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

            anim.SetBool("isGrounded", controller.isGrounded);
            anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
            anim.SetFloat("PositionY", playerModel.GetComponent<Transform>().position.y);

            if (controller.isGrounded && Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
    }

    void FixedUpdate()
    {
        if (!controller.isGrounded)
        {
            verticalVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }
        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
