using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    public float selfRotate = 15f;
    public float rotateSpeed;
    public GameObject pivot; /* il faut créer un gameobject vide et 
    le définir en tant que child du personnage et le choisir comme
    le centre de rotation de l'objet
    */


    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,selfRotate,0); 
        transform.RotateAround(pivot.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        
    }
    
 
}
