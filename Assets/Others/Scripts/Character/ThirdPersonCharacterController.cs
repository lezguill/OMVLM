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
    private float velocity = 0f;
    private bool jumped = true;
    private bool running = false;
    private bool walking = false;

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
        ApplyGravity();
        if (animator.GetBool("Alive"))
        {
            if (!animator.GetBool("Dialogue"))
            {
                JumpManager();
                MovementManager();
                animator.SetBool("Grounded", controller.isGrounded);
                animator.SetBool("Walking", walking);
                animator.SetBool("Running", running);
            }
        }
    }
    private void MovementManager()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            running = true;
        }
        else
        {
            running = false;
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        Vector3 movement = Vector3.zero;

        if (direction.magnitude >= 0.1f)
        {
            walking = true;
        }
        else
        {
            walking = false;
            running = false;
        }

        if (walking)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            if (running)
            {
                actualSpeed = runningSpeed;
            }
            else
            {
                actualSpeed = walkingSpeed;
            }
            movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * actualSpeed;
        }
        movement.y = velocity;
        ApplyMovement(movement);
    }

    private void ApplyMovement(Vector3 movement)
    {
        
        controller.Move(movement * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity<0)
        {
            velocity = -1f;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }
    }

    private void JumpManager()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded && !jumped)
        {
            velocity = jumpForce;
            animator.SetTrigger("Jump");
            jumped = true;
        }

        if (animator.GetBool("Grounded") && jumped)
        {
            jumped = false;
            animator.SetTrigger("Landing");
        }
    }

}
