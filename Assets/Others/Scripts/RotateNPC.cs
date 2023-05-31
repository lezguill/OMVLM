using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateNPC : MonoBehaviour
{
    public Transform npc;
    // Start is called before the first frame update
    public void RotateTowardPlayer()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        npc.transform.LookAt(player.position);
        npc.transform.rotation = Quaternion.Euler(0f, npc.transform.rotation.eulerAngles.y, 0f);

        player.transform.LookAt(npc.position);
        player.transform.rotation = Quaternion.Euler(0f, player.transform.rotation.eulerAngles.y, 0f);
    }
}
