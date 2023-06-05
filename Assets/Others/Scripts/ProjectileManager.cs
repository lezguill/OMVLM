using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public float selfRotate;
    public Transform target; // The target object to follow 
    public ThirdPersonCharacterController character;
    public float normalFollowSpeed = 5f; // The speed at which the object follows the target
    public float recoveryFollowSpeed = 5f; // The speed at which the object gets back to the target after the throw
    public float attackFollowSpeed = 10f;
    public float aimingOffset = 2f;

    public bool isCharging = false; // if the ball is ready to be thrown
    public bool isReady = true;
    public bool isShooting = false;

    public float smoothSpeed = 5f; // The speed at which the object moves towards the target
    public float range = 5f; // The distance from the object to the target position

    private Vector3 chargingPosition;
    private Vector3 recoveryPosition;
    private Vector3 attackPosition;

    private Vector3 targetPosition;

    private void Start()
    {
        chargingPosition = target.position + Vector3.right * aimingOffset;
        attackPosition = transform.position + character.transform.forward * range;
        recoveryPosition = target.position;
    }
    private void Update()
    {
        


        /*
        if (isReady)
        {
            if (character.isAiming)
            {
                // the ball travels to the side of the player by an X offset
                CanTravelTowards(chargingPosition, normalFollowSpeed);
                isCharging = true;
            }
            else if (isShooting)
            {
                // the ball travels to destination to attack
                isShooting = CanTravelTowards(attackPosition, attackFollowSpeed);
            }
            else
            {
                // the ball travels back towards target
                CanTravelTowards(recoveryPosition, recoveryFollowSpeed);
            }
        }
        else
        {
            // the ball travels back towards target
            isReady = !CanTravelTowards(recoveryPosition, recoveryFollowSpeed);
            if (isReady)
            {
                isCharging = false;
            }
        }
        */
    }

    private bool CanTravelTowards(Vector3 destination, float speed)
    {
        Vector3 travel = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
        if (transform.position != travel)
        {
            transform.position = travel;
            return true;
        }
        else
        {
            return false;
        }
    }
}
