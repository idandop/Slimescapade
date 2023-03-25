using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
     public GameObject player;
       
    // private bool grounded;
     private bool jump;
     //private bool canHide;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log(jump);
       
    //Debug.Log(player.GetComponent<PlayerMovement>().grounded);
        
        //bool canHide = player.GetComponent<PlayerMovement>().canHide;
        


      // if(player.GetComponent<PlayerMovement>().grounded == true)
      // {
        
      //  gameObject.SetActive(true);
     //  }
       if(jump == true)
       {
        if(Input.GetKeyDown(KeyCode.Space))
        {
          //  Debug.Log("Bye");
            player.GetComponent<PlayerMovement>().freeJump();
            gameObject.SetActive(false);
            
        }
       }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        
        //Debug.Log("col");
        
        //{
            jump = true;
       // }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("col");
        
        //{
            jump = false;
       // }
    }
}
