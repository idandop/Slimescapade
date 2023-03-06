using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    //public float jumpHeight;
    public float MaxFallSpeed;
    public float jumpHeight;
    
    [HideInInspector]
    public bool notPortal;
    [HideInInspector]
    public bool portalTop;
    [HideInInspector]
    public bool portalBottom;
    [HideInInspector]
    public bool portalLeft;
    [HideInInspector]
    public bool portalRight;
   
    
    private int jumps;
    private float move;
    private float speedOG;
    private float wallJump;
    private float bounceHeight;
    private bool reverse;
    private bool grounded;
    private bool wallTouch;//True when enter on wall, False when exit wallTouch
    private bool onWall;//True when on wall and space down
    private bool canWallJump;
    private bool canReverse;
    
    


    private Rigidbody2D  rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reverse = true;
        wallTouch = false;
        speedOG = speed;
        onWall = false;
       // bounceHeight = 12;
       // canReverse = true;
        
    }

    // Update is called once per frame
    void Update()
    {
     //Debug.Log(jumps);
        //Debug.Log(reverse);
        //Debug.Log(move);
       // Debug.Log(wallTouch);
        //Debug.Log(grounded);  
       // Debug.Log(move);
        //Debug.Log(wallJump);
        //Debug.Log(canReverse);
        //Debug.Log(rb.gravityScale);
        // Debug.Log(spacedown);
        //Debug.Log(rb.velocity.y);
        //Debug.Log(bounceHeight);
        //Debug.Log(Mathf.Abs(rb.velocity.y)*2);

        if (rb.velocity.y < MaxFallSpeed)
        {
            //Debug.Log("MaxFallSpeed");
            rb.velocity = new Vector2(speed * move, MaxFallSpeed);

        }
        else
        {
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
        }


        float newVelocityY = (float)rb.velocity.y;
        
        if(Mathf.Abs(newVelocityY) < 10)
        {
            bounceHeight= 13.0f;

        }
        else{
        //Debug.Log("poggers");
        bounceHeight=(Mathf.Abs(newVelocityY)*1.25f);
        }
//      
       
        

         if(onWall)
           {
            //Debug.Log("wall");
            canReverse = false;
            rb.velocity = new Vector2(speed * move, -1);


           }
        
        if (wallTouch && Input.GetKey(KeyCode.Space))//Wall Slide
        {
           
           speed = 0;
           onWall = true;
           //Debug.Log("touch");
           rb.velocity = new Vector2(rb.velocity.x, -1.0f);
          
           if (Input.GetKeyUp(KeyCode.Space))
           {
           //Debug.Log("wallJump");
           jumps = 1;
           speed = speedOG;
           onWall = false;
           }

           
        }
        if (reverse){
            move = -1; //Left
            
            }
        
        else{
            move = 1; //Right
        }


       if (Input.GetKeyUp(KeyCode.Space) && onWall == true)//Detatch from wall 
        {
        // Debug.Log("up");
         jumps = 2;
        if (canWallJump == true)
        {
         wallJump += 1;// gives one wall jump
        }
         speed = speedOG;
         canReverse = true;
         onWall = false;
        }
        
       if (Input.GetKeyDown(KeyCode.Space))
       {
            Jump();
       }
       if (grounded == true)
       {
            jumps = 0;//Reset Jumps
            wallJump = 0;
            canWallJump = true;

       }
           
        
    }

    private void Jump()
    {
        
        if (jumps < 2)
        {
        rb.velocity = new Vector2(rb.velocity.x,jumpHeight);
        jumps++; // Increment Jumps
        grounded = false; //Off Ground

        }
        else if(wallJump >= 1)
        {
        rb.velocity = new Vector2(rb.velocity.x,jumpHeight);
        wallJump -= 1;
        canWallJump = false;
        }

    }

    private void Bounce()
    {
        
       
        rb.velocity = new Vector2(rb.velocity.x,bounceHeight);
       

    }

    private void OnCollisionEnter2D(Collision2D col)
    {

    if(col.gameObject.tag =="Ground")
    {
       //Debug.Log("touch");
       grounded = true;
       if(!onWall)
       {
       canReverse = true;
       }
    }

    if (col.gameObject.tag =="Wall") 
    {
        //Debug.Log("bozo");
        if (canReverse == true){
        reverse = !reverse; //Flips reverse
        canReverse = false;
        }
        wallTouch = true;
      
    }
     if (col.gameObject.tag =="Platform") // Platform Border Detection
    {
        if(!onWall)
       {
       canReverse = true;
       }
        //canReverse = true;
        ContactPoint2D contact = col.contacts[0];
        Vector2 normal = contact.normal;

        // Check if collision is from the top of the platform
        if (normal.y > 0.5f)
        {
            // Stop player from falling through the platform
            //Debug.Log("top touch");
            grounded = true;
           
           // if(!onWall)
          //  {
          //  canReverse = true;
           // }
           
            
           
        }

        // Check if collision is from the bottom of the platform
        else if (normal.y < -0.5f)
        {
            
            
            //Debug.Log("bottom touch");
            // if(!onWall)
          // {
          //  canReverse = true;
          //  }
           
        }

        // Check if collision is from the side of the platform
        else if (Mathf.Abs(normal.x) > 0.5f)
        {
            
          //Debug.Log("side touch");
         //  if(!onWall)
          //  {
          //  canReverse = true;
           // }
          if (canReverse == true){ 
          reverse = !reverse; //Flips reverse
          //canReverse = false;
          }
        }
          
       
      
    }
    
     if (col.gameObject.tag =="BouncePad") // BouncePad Border 
    {
       if(!onWall)
       {
       canReverse = true;
       }
       // canReverse = true;
        ContactPoint2D contact = col.contacts[0];
        Vector2 normal = contact.normal;

        // Check if collision is from the top of the platform
        if (normal.y > 0.5f)
        {
            // Stop player from falling through the platform
            //Debug.Log("top touch");
           // grounded = true;
           
            Bounce();
            //if(!onWall)
           // {
           // canReverse = true;
           // }
           
            
           
        }

        // Check if collision is from the bottom of the platform
        else if (normal.y < -0.5f)
        {
            
            //Debug.Log("bottom touch");
           
        }

        // Check if collision is from the side of the platform
        else if (Mathf.Abs(normal.x) > 0.5f)
        {
            
          //Debug.Log("side touch");
          if (canReverse == true){ 
          reverse = !reverse; //Flips reverse
          canReverse = false;
          }

          
       
      
    }


    }
    if (col.gameObject.tag =="Conveyor") // Conveyor Border Detection
    {
        if(!onWall)
       {
       canReverse = true;
       }
       // canReverse = true;
        ContactPoint2D contact = col.contacts[0];
        Vector2 normal = contact.normal;

        // Check if collision is from the top of the platform
        if (normal.y > 0.5f)
        {
            // Stop player from falling through the platform
            //Debug.Log("top touch");
            //grounded = true;
           
           // if(!onWall)
           // {
           // canReverse = true;
           // }
           
            
           
        }

        // Check if collision is from the bottom of the platform
        else if (normal.y < -0.5f)
        {
            
           // Debug.Log("bottom touch");
           //  if(!onWall)
          // {
           // canReverse = true;
          //  }
           
            if (canReverse == true){ 
            reverse = !reverse; //Flips reverse
          
             }
        

        }

        // Check if collision is from the side of the platform
        else if (Mathf.Abs(normal.x) > 0.5f)
        {
        //    if(!onWall)
           // {
           // canReverse = true;
           // }
         // Debug.Log("side touch");
          if (canReverse == true){ 
          reverse = !reverse; //Flips reverse
          
          }
        }
          
       
      
    }
    if (col.gameObject.tag == "Portal")
    {
        notPortal = false;
       // if(!onWall)
      // {
      // canReverse = true;
      // }
       // canReverse = true;
        ContactPoint2D contact = col.contacts[0];
        Vector2 normal = contact.normal;

        // Check if collision is from the top of the platform
        if (normal.y > 0.5f)
        {
            // Stop player from falling through the platform
            //Debug.Log("top touch");
           
           
           
        portalTop = true;
            
           
        }

        // Check if collision is from the bottom of the platform
        else if (normal.y < -0.5f)
        {
            
          portalBottom = true;
           
            

        }

         else if (normal.x > 0.5f)
          {
        // Collision occurred on right of object
        Debug.Log("right");  
        portalRight = true;
         }
         else if (normal.x < -0.5f)
          {
        // Collision occurred on left of object
        Debug.Log("left");  
        portalLeft = true;
          }
    }
    if (col.gameObject.tag != "Portal")
    {
    
    notPortal = true;
    }
    }
    private void OnCollisionExit2D(Collision2D col)
{
    if (col.gameObject.tag == "Wall")
    {
        wallTouch = false;
        //onWall = false;
    }
    if (col.gameObject.tag =="Platform") 
    {    
     //Debug.Log("off");
     //    canReverse = true;
    }  

    if (col.gameObject.tag =="BouncePad") 
    {    
     //Debug.Log("off");
       //  canReverse = true;
    }  
    if (col.gameObject.tag =="Conveyor") 
    {    
     //Debug.Log("off");
     //    canReverse = true;
    }  
          
       
      
    }
    
}

