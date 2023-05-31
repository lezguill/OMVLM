using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    public AudioSource deathSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Animator>().SetTrigger("Death");
            other.GetComponent<Animator>().SetBool("Alive", false);
            deathSound.Play();
        }
    }
}
