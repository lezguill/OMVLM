using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{

    public CharacterController controller;
    public Animator animator;
    public Transform camera;

    private float gravity = -9.81f;
    public float gravityMultiplier = 1.5f;
    private float verticalVelocity = 0f;
    private bool isJumping = true;
    private bool isRunning = false;
    private bool isWalking = false;
    private bool isGrounded= false;

    public float walkingSpeed;
    public float runningSpeed;
    private float actualSpeed;
    public float jumpForce;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isAlive"))
        {
            if (!animator.GetBool("isDialoguing"))
            {
                JumpManager();
                MovementManager();
            }
        }
    }
    private void MovementManager()
    {
        // Get the vertical velocity right
        CalculateGravity();

        // Get the player's Input 
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        Vector3 movement = Vector3.zero;

        // Check if player is moving
        if (direction.magnitude >= 0.1f)
        {
            isWalking = true;

            // Check if player is running
            if (Input.GetKey(KeyCode.LeftShift)) 
            {
                isRunning = true;
            }
            else 
            {
                isRunning = false;
            }
        }
        else 
        {
            isWalking = false; 
            isRunning = false;
        }

        // Update the animtor's states 
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);

        
        if (isWalking)
        {
            // Calculate the direction of the movement
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            if (isRunning)
            {
                actualSpeed = runningSpeed;
            }
            else
            {
                actualSpeed = walkingSpeed;
            }
            movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * actualSpeed;
        }

        // Add gravity or jump force
        movement.y = verticalVelocity;

        // Apply the force
        controller.Move(movement * Time.deltaTime);

    }

    private void CalculateGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity += gravity * gravityMultiplier * Time.deltaTime;
        }
    }

    private void JumpManager()
    {
        if (controller.isGrounded)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", isGrounded);
            isJumping = false;
            animator.SetBool("isJumping", isJumping);

            animator.SetBool("isFalling", false);


            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jumping");
                verticalVelocity = jumpForce;
                animator.SetBool("isJumping", true);
                isJumping = true;
            }
        }
        else 
        {
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);

            if ((isJumping && verticalVelocity < 0) || verticalVelocity <-2)
            {
                Debug.Log("Falling");
                animator.SetBool("isFalling", true);
            }
        }
    }
}
