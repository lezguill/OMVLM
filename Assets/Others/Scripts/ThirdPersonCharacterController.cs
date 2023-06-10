using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{

    public CharacterController ctrl;
    public Animator anim;
    public GameObject mainCam;
    public GameObject moveCam;
    public GameObject aimCam;
    public GameObject crosshair;
    public ProjectileManager projectile;

    private float aimingTransitionTime;
    private float gravity = -9.81f;
    public float gravityMultiplier = 1.5f;
    private float verticalVelocity = 0f;
    private bool isJumping = true;
    private bool isRunning = false;
    private bool isWalking = false;
    private bool isGrounded = false;

    public bool isAiming = false;

    public float walkingSpeed;
    public float runningSpeed;
    private float actualSpeed;
    public float jumpForce;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        aimingTransitionTime = mainCam.GetComponent<Cinemachine.CinemachineBrain>().m_DefaultBlend.m_Time;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isAlive"))
        {
            if (!anim.GetBool("isDialoguing"))
            {
                JumpManager();
                MovementManager();
                AimManager();
            }
        }
    }
    private void MovementManager()
    {
        // Get the vertical velocity right
        CalculateGravity();

        Vector3 movement = Vector3.zero;

        if (!isAiming)
        {
            // Get the player's Input 
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical);

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
            anim.SetBool("isWalking", isWalking);
            anim.SetBool("isRunning", isRunning);

            if (isWalking)
            {
                // Calculate the direction of the movement
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.transform.eulerAngles.y;
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
        }

        // Add gravity or jump force
        movement.y = verticalVelocity;

        // Apply the force
        ctrl.Move(movement * Time.deltaTime);

    }

    private void CalculateGravity()
    {
        if (ctrl.isGrounded && verticalVelocity < 0)
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
        if (ctrl.isGrounded)
        {
            isGrounded = true;
            anim.SetBool("isGrounded", isGrounded);
            isJumping = false;
            anim.SetBool("isJumping", isJumping);

            anim.SetBool("isFalling", false);


            if (Input.GetButtonDown("Jump") && !isAiming)
            {
                verticalVelocity = jumpForce;
                anim.SetBool("isJumping", true);
                isJumping = true;
            }
        }
        else
        {
            isGrounded = false;
            anim.SetBool("isGrounded", isGrounded);

            if ((isJumping && verticalVelocity < 0) || verticalVelocity < -2)
            {
                anim.SetBool("isFalling", true);
            }
        }
    }

    private void AimManager()
    {
        if (Input.GetButtonDown("Aim") && !isAiming && projectile.isReady)
        {
            isAiming = true;
            moveCam.SetActive(false);
            aimCam.SetActive(true);
            this.transform.rotation = Quaternion.Euler(0f, mainCam.transform.eulerAngles.y, 0f);
            anim.SetBool("isWalking", false);
            StartCoroutine(ShowCrosshair());
        }
        else if (Input.GetButtonDown("Aim") && isAiming)
        {
            ExitAimingMode();
        }
    }

    IEnumerator ShowCrosshair()
    {
        yield return new WaitForSeconds(aimingTransitionTime);
        crosshair.SetActive(true);
    }

    IEnumerator GetOutOfAimingMode()
    {
        yield return new WaitForSeconds(aimingTransitionTime);
        isAiming = false;
    }

    public void ExitAimingMode()
    {
        aimCam.SetActive(false);
        moveCam.SetActive(true);
        crosshair.SetActive(false);
        StartCoroutine(GetOutOfAimingMode());
    }
}
