using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //References
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform groundCheck;

    //Variables
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    Vector3 playerVelocity;


    //Gravity
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;


    private void Update()
    {
        GroundChecker();
        Movement();
        Jump();
        CheckAboveCollision();
        Gravity();
    }

    void GroundChecker()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundCheckRadius, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            characterController.slopeLimit = 45;
            playerVelocity.y = 0;
        }
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = Vector3.Normalize(transform.right * horizontalInput + transform.forward * verticalInput);
        characterController.Move(move * speed * Time.fixedDeltaTime);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            characterController.slopeLimit = 100;
            playerVelocity.y = Mathf.Sqrt(-2 * gravityValue * jumpHeight);
        }
    }
    void CheckAboveCollision()
    {
        if ((characterController.collisionFlags & CollisionFlags.Above) != 0)
        {
            playerVelocity.y = -2f;
        }

    }
    void Gravity()
    {
        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        characterController.Move(playerVelocity * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}

