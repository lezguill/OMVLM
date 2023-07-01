using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public float selfRotate;
    public Transform target; // The target object to follow 
    public ThirdPersonCharacterController character;
    public float aimingOffset = 2f;

    public bool isAttacking = false;
    public bool willAttack = false; // if the ball is ready to be thrown
    public bool willRecover = false; // if the ball is ready to recover
    public bool isReady = true; // if the ball is ready to be charged
    public bool isAiming = false;
    public bool canTravel = true; // if the ball can still move towards where it should go depending on the state

    public float recoverySpeed = 4f;
    public float followSpeed = 2f;
    public float attackSpeed = 10f;
    public float defaultSpeed = 5f; // The speed at which the object moves towards a destination
    public float range = 10f; // The distance from the object to the target position
    public float power = 0; // multiplier that goes up when charging
    public float baseDamage = 10;

    private Vector3 startingPosition;
    private Vector3 chargingPosition;
    private Vector3 recoveryPosition;
    private Vector3 attackPosition;

    private void Update()
    { 
        if (character.isAiming && isReady)
        {
            if (!willAttack) { startingPosition = transform.position; }
        // Charging State: while right click is hold & the ball is ready
            if (canTravel || Input.GetButtonUp("Aim") || Input.GetButtonDown("Aim"))
            {
                chargingPosition = target.position + (Vector3.right+Vector3.up) * aimingOffset;
                canTravel = CanTravelTowards(chargingPosition, defaultSpeed);
                power += 0.1f;
                attackPosition = startingPosition + character.transform.forward * range * power;
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
                    character.ExitAimingMode();
                    canTravel = CanTravelTowards(attackPosition, attackSpeed);
                    isAttacking = canTravel;
                    willRecover = true;
                    isReady = false;
                }
                else if (Input.GetButton("Aim")) // To start recovery press right click
                {
                    willAttack = false;
                    canTravel = true;
                    isReady = false;
                }
            }
            else if (willRecover)
            // Revcovery State: after Attacking State & right click pressed
            {
                if (canTravel)
                {
                    recoveryPosition = target.position;
                    canTravel = CanTravelTowards(recoveryPosition, recoverySpeed);
                    isReady = false;
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
                CanTravelTowards(target.position, followSpeed);
                ResetStates();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((willAttack || willRecover) && !isAttacking && !isAiming)
            {
                ResetStates();
            }
        }       
        if (other.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(Mathf.CeilToInt(baseDamage * power));
                
            }
        }
    }

    public void ResetStates()
    {
        willRecover = false;
        willAttack = false;
        canTravel = true;
        isReady = true;
        power = 0f;
    }
}
