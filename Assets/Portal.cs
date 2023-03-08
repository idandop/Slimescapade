using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
       //public GameObject playerMovement;
       public GameObject otherPortal; // Assign the other portal GameObject in the Inspector
       public GameObject player;
       
       private bool notPortal;
       private Vector3 outPos;
       

// Start is called before the first frame update
    void Start()
    {
     
    }
    // Update is called once per frame
    void Update()
    {
   
    notPortal = player.GetComponent<PlayerMovement>().notPortal;//Vector3 playerPosition = player.transform.position;
   
   
    }
  private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject == player) //(col.gameObject.tag =="Player") // Make sure the object entering the portal is the player
        {
        if(notPortal == true)
          {
          Vector3 newPos = otherPortal.transform.position; // Get the position of the other portal
         newPos.z = player.transform.position.z; // Maintain the same Z position as the player

          player.transform.position = newPos; // Teleport the player to the other portal
          }
          }
}
          private void OnCollisionExit2D(Collision2D col)
          {
        //Debug.Log("exit");
        //off = true;

         }


}

