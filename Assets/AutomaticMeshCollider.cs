using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticMeshCollider : MonoBehaviour
{
   void Start() {
        Transform[] children = GetComponentsInChildren<Transform>(true);

       
        for (int i = 1; i < children.Length; i++)
        {
           
            MeshCollider meshCollider = children[i].gameObject.AddComponent<MeshCollider>();

        }
    }
}
