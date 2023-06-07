using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AutomaticMeshCollider : MonoBehaviour
{
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            Transform[] children = GetComponentsInChildren<Transform>(true);

            for (int i = 1; i < children.Length; i++)
            {
                MeshCollider meshCollider = children[i].gameObject.GetComponent<MeshCollider>();
                if (meshCollider == null)
                {
                    meshCollider = children[i].gameObject.AddComponent<MeshCollider>();
                }
            }
        }
    }
}

