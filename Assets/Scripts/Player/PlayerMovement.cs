using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //References
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform groundCheck;

    //Variables
    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    Vector3 playerVelocity;


    //Gravity
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    //Stamina
    [SerializeField] float stamina;
    [SerializeField] float staminaUseMultiplier;
    [SerializeField] float staminaReloadMultiplier;
    bool canRun;

    //UI
    [SerializeField] Slider slider;

    private void Start()
    {
        slider.maxValue = 100;
    }
    private void Update()
    {
        GroundChecker();
        Run();
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
        characterController.Move(move * speed * Time.deltaTime);
    }
    void Run()
    {
        Debug.Log("Speed: " + speed);

        if (Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            speed = runSpeed;
            UseStamina();
            if (stamina <= 0)
            {
                canRun = false;
            }
        }
        else
        {
            StartCoroutine(ReloadStamina());
            speed = walkSpeed;
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            characterController.slopeLimit = 100;
            playerVelocity.y = Mathf.Sqrt(-2 * gravityValue * jumpHeight * Time.deltaTime);
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
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
    void UseStamina()
    {

        stamina -= Time.deltaTime * staminaUseMultiplier;
        slider.value = stamina;
        //Debug.Log("Stamina: " + stamina);
    }
    IEnumerator ReloadStamina()
    {
        yield return new WaitForSeconds(1f);
        if (stamina <= 100)
        {
            stamina += Time.deltaTime * staminaReloadMultiplier;
            slider.value = stamina;
        }
        if (stamina > 15)
        {
            canRun = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}

