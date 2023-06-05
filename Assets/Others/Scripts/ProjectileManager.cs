using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public float selfRotate;
    public Transform target; // The target object to follow 
    public float followSpeed = 5f; // The speed at which the object follows the target

    private void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;

            // Normalize the direction vector
            direction.Normalize();

            // Calculate the desired position to move towards
            Vector3 targetPosition = target.position - direction;

            // Move towards the target position using Lerp for smooth movement
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        // Rotate around itself
        transform.Rotate(0, selfRotate, 0);

    }

}
