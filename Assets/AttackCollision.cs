using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    public EnemyAI enemy;
    public bool touchingPlayer=false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }
}
