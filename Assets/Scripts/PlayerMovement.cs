using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float MaxFallSpeed;
    public float jumpHeight;

    public bool GoRight;

    public MMFeedbacks shakeFeedback;
    public MMFeedbacks shakeSmallFeedback;
    public MMFeedbacks shakeBigFeedback;
    [HideInInspector]
    public GameObject myPrefabJumpPickup;
    [HideInInspector]
    public GameObject jumpParticle;

    public AudioClip jumpSound;
    public AudioClip bounceSound;
    public AudioClip springSound;
    public AudioClip portalSound;
    
    
    [HideInInspector]
   
    public bool notPortal;
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool canHide;
    [HideInInspector]
    public bool bounceFeedback;
   
  
   
    private int jumps;
    private float move;
    private float speedOG;
    private float wallJump;
    private float bounceHeight;
    private bool reverse;
    private bool wallTouch;//True when enter on wall, False when exit wallTouch
    private bool onWall;//True when on wall and space down
    private bool canWallJump;
    private bool canReverse;
    private bool up;
    private bool canFreeJump;
    private bool hasCollided;
    private bool maxSpeed;
    private GameObject[] instances;
    
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer; // Reference to the Sprite Renderer component



    private Rigidbody2D  rb;
    // Start is called before the first frame update
    void Start()
    {
        
        if(GoRight == true)
        {
            reverse = false;
        }
        else{
            reverse = true;
        }
    
    rb = GetComponent<Rigidbody2D>();
    audioSource = GetComponent<AudioSource>();
        
        wallTouch = false;
        speedOG = speed;
        onWall = false;
       // bounceHeight = 12;
       // canReverse = true;
         spriteRenderer = GetComponent<SpriteRenderer>();
        instances = GameObject.FindGameObjectsWithTag("JumpPickup");
        // Set the initial color of the sprite
        DeactivateAllInstances();
        ActivateAllInstances();
      
            
            
        
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
        //Debug.Log(rb.velocity.y);
       // Debug.Log(up);
      // Debug.Log(canHide);
      //Debug.Log(canFreeJump);
      
      // Debug.Log(maxSpeed);



    

        if(rb.velocity.y > -1)
       {
           up = true;
       }
       else{
           up = false;
       }
        
       if (rb.velocity.y < -12)
        {
            
            maxSpeed = true;

        }
        else
        {
     
        maxSpeed = false;
        }


        if (rb.velocity.y < MaxFallSpeed)
        {
            //Debug.Log("MaxFallSpeed");
            rb.velocity = new Vector2(speed * move, MaxFallSpeed);
            //maxSpeed = true;

        }
        else
        {
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
        //maxSpeed = false;
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
          
           //if (Input.GetKeyUp(KeyCode.Space))
          // {
           //Debug.Log("wallJump");
          // jumps = 1;
          // speed = speedOG;
         //  onWall = false;
         //  }

           
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
            ActivateAllInstances();

       }
           
        
    }


      private void ActivateAllInstances()
    {
        // Find all instances of the prefab in the scene
        
        //GameObject[] instances = GameObject.FindGameObjectsWithTag("JumpPickup");
        // Activate each instance
        foreach (GameObject instance in instances)
        {
        //Debug.Log(instance)    ;
        instance.SetActive(true);
        }
    }
     private void DeactivateAllInstances()
    {
        // Find all instances of the prefab in the scene
        
        //GameObject[] instances = GameObject.FindGameObjectsWithTag("JumpPickup");
        // Activate each instance
        foreach (GameObject instance in instances)
        {
        //Debug.Log(instance)    ;
        instance.SetActive(false);
        }
    }


    private void Jump()
    {
        
        //Debug.Log("jump");
        if (jumps < 2 && canFreeJump == false)
        {
        rb.velocity = new Vector2(rb.velocity.x,jumpHeight);
        jumps++; // Increment Jumps
        grounded = false; //Off Ground
        //JumpFeedback?.PlayFeedbacks();
        JumpParticle();
        audioSource.PlayOneShot(jumpSound);

        }
        else if(wallJump >= 1 && canFreeJump == false)
        {
        rb.velocity = new Vector2(rb.velocity.x,jumpHeight);
        wallJump -= 1;
        canWallJump = false;
        JumpParticle();
        audioSource.PlayOneShot(jumpSound);
        }

    }
     public void freeJump()
    {
        
        //Debug.Log("free");
        rb.velocity = new Vector2(rb.velocity.x,jumpHeight+1.5f);
        audioSource.PlayOneShot(jumpSound);
        JumpParticle();
        grounded = false; //Off Ground

      

    }
    private void portalJump()
    {
        audioSource.PlayOneShot(portalSound);
       if(up == true)
       {
        rb.velocity = new Vector2(rb.velocity.x,7);
        //Debug.Log("up");
       }
        else{
        rb.velocity = new Vector2(rb.velocity.x,-2);
       //Debug.Log("down");
        }
        grounded = false; //Off Ground


   

    }
     private void VariableShake()
    {
    //Debug.Log(maxSpeed);   
     if(maxSpeed == true)
        {
            shakeBigFeedback?.PlayFeedbacks();
           // Debug.Log("Big");
        }
        else
        {
            shakeSmallFeedback?.PlayFeedbacks();
            //Debug.Log("small");
        }
        
    }
    private void Bounce()
    {
        
       
        rb.velocity = new Vector2(rb.velocity.x,bounceHeight);
       

    }

    private void JumpParticle()
    {
    Vector3 spawnPos = transform.position + new Vector3(0f, -0.85f, 0f);
    Instantiate(jumpParticle, spawnPos, Quaternion.identity);
    //Destroy(jumpParticle, 3f);
    }
  
    
  
    
    private void OnCollisionEnter2D(Collision2D col)
    {
   
   // Debug.Log(col);
   if(col.gameObject.tag =="Exit")
    {
    SceneManager.LoadScene("End");
    }

    bounceFeedback = false;
   
    if(col.gameObject.tag =="Ground")
    {
       if (hasCollided==false)
    {
        audioSource.PlayOneShot(bounceSound);
        VariableShake();
         //Debug.Log("bonk");
         hasCollided = true;
        
       
    }  
       grounded = true;
       if(!onWall)
       {
       canReverse = true;
       }
    }

    if (col.gameObject.tag =="Wall") 
    {
        if (hasCollided==false)
    {
        audioSource.PlayOneShot(bounceSound);
        shakeFeedback?.PlayFeedbacks();
        // Debug.Log("bonk");
         hasCollided = true;
        
       
    } 
        //Debug.Log("bozo");
        if (canReverse == true){
        reverse = !reverse; //Flips reverse
        canReverse = false;
        }
        wallTouch = true;
      
    }
     if (col.gameObject.tag =="Platform") // Platform Border Detection
    {
        
     if (hasCollided==false)
    {
        audioSource.PlayOneShot(bounceSound);
        VariableShake();
      //   Debug.Log("bonk");
         hasCollided = true;
        
       
    } 
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
      // Debug.Log("agh");
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
             audioSource.PlayOneShot(springSound);
             shakeBigFeedback?.PlayFeedbacks();
             bounceFeedback = true;
            //if(!onWall)
           // {
           // canReverse = true;
           // }
           
            
           
        }

        // Check if collision is from the bottom of the platform
        else if (normal.y < -0.5f)
        {
            audioSource.PlayOneShot(bounceSound); 
            shakeSmallFeedback?.PlayFeedbacks();
            //Debug.Log("bottom touch");
           
        }

        // Check if collision is from the side of the platform
        else if (Mathf.Abs(normal.x) > 0.5f)
        {
            audioSource.PlayOneShot(bounceSound);
            shakeSmallFeedback?.PlayFeedbacks();
          //Debug.Log("side touch");
          if (canReverse == true){ 
          reverse = !reverse; //Flips reverse
          
          }

          
       
      
    }


    }
    if (col.gameObject.tag =="Conveyor") // Conveyor Border Detection
    {
         if (hasCollided==false)
    {
        audioSource.PlayOneShot(bounceSound);
       //  Debug.Log("bonk");
       VariableShake();
         hasCollided = true;
        
       
    } 
        
        if(!onWall)
       {
       canReverse = true;
       }
       // canReverse = true;
        ContactPoint2D contact = col.contacts[0];
        Vector2 normal = contact.normal;
         float zRotation = col.transform.eulerAngles.z;
    
          // Print the rotation
        //Debug.Log(zRotation);

        // Check if collision is from the top of the platform
        if (normal.y > 0.5f)
        {
            // Stop player from falling through the platform
           // Debug.Log("top touch");
            grounded = true;
           
           // if(!onWall)
           // {
           // canReverse = true;
           // }
           
            
           
        }

        // Check if collision is from the bottom of the platform
        else if (normal.y < -0.5f)
        {
            
            //Debug.Log("bottom touch");
           //  if(!onWall)
          // {
           // canReverse = true;
          //  }
           
           
          if (reverse == true && zRotation < 90)
          {

          if (canReverse == true){ 
            //Debug.Log("rev");
            reverse = !reverse; //Flips reverse
          
             }
          }
          if (reverse == false && zRotation > 90)
          {

          if (canReverse == true){ 
            //Debug.Log("rev");
            reverse = !reverse; //Flips reverse
          
             }
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
          if (canReverse == true ){ 
          
          reverse = !reverse; //Flips reverse
          
          }
        }
          
       
      
    }
    
    if (col.gameObject.tag == "Portal")
    {
        notPortal = false;
        if(!onWall)
       {
       canReverse = true;
       }
       // canReverse = true;
        ContactPoint2D contact = col.contacts[0];
        Vector2 normal = contact.normal;
        portalJump();
        // Check if collision is from the top of the platform
        if (normal.y > 0.5f)
        {
            // Stop player from falling through the platform
            //Debug.Log("top touch");
           
           
     
            
           
        }

        // Check if collision is from the bottom of the platform
        else if (normal.y < -0.5f)
        {
            
       
            

        }

         else if (normal.x > 0.5f)
          {
        // Collision occurred on right of object
       
         }
         else if (normal.x < -0.5f)
          {
        // Collision occurred on left of object
 
          }
    }
    if (col.gameObject.tag != "Portal")
    {
    
    notPortal = true;
    }
    }

    private void OnTriggerStay2D(Collider2D col)
{
     if (col.gameObject.tag =="JumpPickup") 
    {
        if(!onWall)
       {
       canReverse = true;
       }
      //  spriteRenderer.color = Color.red;
        canFreeJump = true;
        //canHide = true;
       
      
    }
}
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag =="JumpPickup") 
    {
       // spriteRenderer.color = Color.white;
        //GameObject collidedObject = col.gameObject;
        canFreeJump = false;
       // canHide = false;
    }
    }
    private void OnCollisionExit2D(Collision2D col)
{
    
   
    
    
    if (hasCollided == true)
    {
      
      hasCollided = false;  
    }

    if (col.gameObject.tag == "Wall")
    {
     
    wallTouch = false;
        //onWall = false;
    }
     if (col.gameObject.tag == "Ground")
    {
      //Debug.Log("buh");
    //hasCollided = false;
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

