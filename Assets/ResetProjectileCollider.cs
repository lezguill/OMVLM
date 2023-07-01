using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProjectileCollider : MonoBehaviour
{
    public ProjectileManager projectile;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            if ((projectile.willAttack || projectile.willRecover) && !projectile.isAttacking && !projectile.isAiming)
            {
                projectile.ResetStates();
            }
        }
    }
}
