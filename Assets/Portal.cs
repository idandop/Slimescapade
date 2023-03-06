using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
       //public GameObject playerMovement;
       public GameObject otherPortal; // Assign the other portal GameObject in the Inspector
       public GameObject player;
       
       private bool notPortal;
       private bool top;
       private bool bot;
       private bool left;
       private bool right;
       private float topOther;
       private float bottomOther;
       private float leftOther;
       private float rightOther;
       private Vector3 outPos;
       //private bool bot;
       //private bool top;
      // public transform playerPosition;
      

// Start is called before the first frame update
    void Start()
    {
      //notPortal = true;
      // Vector3 topOther = topOther;
      // Vector3 bottomOther = bottomOther;
      // Vector3 leftOther = leftOther;
       //Vector3 rightOther = rightOther;
    }

    // Update is called once per frame
    void Update()
    {
     //Collider2D col = GetComponent<Collider2D>();
       //float top = transform.position.y;// + col.bounds.extents.y;
      // float bottom = transform.position.y - col.bounds.extents.y;
     //  float left = transform.position.x - col.bounds.extents.x;
      // float right = transform.position.x + col.bounds.extents.x;

       Collider2D colOther = otherPortal.GetComponent<Collider2D>();
       float topOther = otherPortal.transform.position.y + colOther.bounds.extents.y;
       float bottomOther = otherPortal.transform.position.y - colOther.bounds.extents.y;
       float leftOther = otherPortal.transform.position.x - colOther.bounds.extents.x;
       float rightOther = otherPortal.transform.position.x;// + colOther.bounds.extents.x;
    
    
    
    notPortal = player.GetComponent<PlayerMovement>().notPortal;//Vector3 playerPosition = player.transform.position;
    bool top = player.GetComponent<PlayerMovement>().portalTop;
    bool bot = player.GetComponent<PlayerMovement>().portalBottom;
    bool left = player.GetComponent<PlayerMovement>().portalLeft;
    bool right = player.GetComponent<PlayerMovement>().portalRight;
    //Debug.Log(notPortal);
    //Debug.Log(left);
    if(notPortal == true)
          {
             
            if (left == true){
                Debug.Log("crack");
                Vector3 outPos = new Vector3(rightOther,otherPortal.transform.position.y);
                player.transform.position = outPos;
                left = false;
                 

            }
          
          
            
          }
    }
  private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject == player) //(col.gameObject.tag =="Player") // Make sure the object entering the portal is the player
        {
        //Debug.Log("crack");   
        //Vector3 outPos = otherPortal.transform.position; // Get the position of the other portal
        
           // newPos.z = other.transform.position.z; // Maintain the same Z position as the player
          
          }
}
          private void OnCollisionExit2D(Collision2D col)
          {
        //Debug.Log("exit");
        //off = true;

         }


}

