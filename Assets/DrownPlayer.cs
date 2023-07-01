using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownPlayer : MonoBehaviour
{
    public ThirdPersonCharacterController player;
    public Transform playerTransform;
    public Vector3 respawnPoint;

    public AnimationClip crossfade;
    public Animator respawn;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacterController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ThirdPersonCharacterController>().Die();
            respawn.SetTrigger("Start");
            StartCoroutine(RespawnPlayer());
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3);
        player.ctrl.enabled = false;
        playerTransform.position = respawnPoint;
        player.ctrl.enabled = true;
        player.anim.ResetTrigger("DeathAction");
        player.anim.SetBool("isAlive", true);
    }
}
