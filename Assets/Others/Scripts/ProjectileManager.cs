using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public float selfRotate;
    public Transform target; // The target object to follow 
    public ThirdPersonCharacterController character;
    public float aimingOffset = 2f;


    public bool willAttack = false; // if the ball is ready to be thrown
    public bool willRecover = false; // if the ball is ready to recover
    public bool isReady = true; // if the ball is ready to be charged
    public bool isAiming;
    public bool canTravel = true; // if the ball can still move towards where it should go depending on the state

    public float recoverySpeed = 4f;
    public float followSpeed = 2f;
    public float attackSpeed = 10f;
    public float defaultSpeed = 5f; // The speed at which the object moves towards a destination
    public float range = 10f; // The distance from the object to the target position
    public float power = 0; // multiplier that goes up when charging

    private Vector3 startingPosition;
    private Vector3 chargingPosition;
    private Vector3 recoveryPosition;
    private Vector3 attackVector;

    private void Start()
    {
        chargingPosition = target.position + Vector3.right * aimingOffset;
        attackVector = character.transform.forward * range;
        recoveryPosition = target.position;
    }
    private void Update()
    {

         isAiming = character.isAiming;

        if (isAiming && isReady)
        {
        // Charging State: while right click is hold & the ball is ready
            if (canTravel)
            {
                canTravel = CanTravelTowards(chargingPosition, defaultSpeed);
                power += 0.1f;
                startingPosition = transform.position;
                willAttack = true;
            }
            else
            {
                isReady = false;
                canTravel = true;
            }
        }
        else
        {
            if (willAttack)
            // Attacking State: after Charging State && ( right click was released || or || ball arrived to chargingPosition )
            {
                if (canTravel)
                {
                    canTravel = CanTravelTowards(startingPosition + attackVector * power, attackSpeed);
                    willRecover = true;
                }
                else if (isAiming) // To start recovery press right click
                {
                    willAttack = false;
                    canTravel = true;
                }
            }
            else if (willRecover)
            // Revcovery State: after Attacking State & right click pressed
            {
                if (canTravel)
                {
                    canTravel = CanTravelTowards(recoveryPosition, recoverySpeed);
                }
                else
                {
                    willRecover = false;
                    isReady = true;
                    canTravel = true;
                }
            }
            else if (isReady)
            // Neutral State: if not in any other state - ball follows target
            {
                willRecover = false;
                willAttack = false;
                CanTravelTowards(target.position, followSpeed);
                canTravel = true;
            }
        }
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
