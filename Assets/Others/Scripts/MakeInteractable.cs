using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MakeInteractable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InteractableOn());
    }
    
    IEnumerator InteractableOn()
    {
        yield return new WaitForSeconds(7);
        this.GetComponent<Button>().interactable = true;
    }
}
