using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracking : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform playerT;
    public float distanceBelow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    Vector3 targetPosition = playerT.position;    
    Vector3 newPosition = new Vector3(targetPosition.x, targetPosition.y - distanceBelow, targetPosition.z);
    transform.position = newPosition;
    //transform.position = playerT.position;
    }
}
