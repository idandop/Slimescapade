using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class BouncePadFeedbackScript : MonoBehaviour
{
    
    public MMFeedbacks bouncePadFeedback;
    public GameObject player;
    private bool feedback;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    
    {
        feedback = player.GetComponent<PlayerMovement>().bounceFeedback;
        
    }
   
 private void OnCollisionExit2D(Collision2D col)
{
       // Debug.Log(feedback);
        if (feedback){
          bouncePadFeedback?.PlayFeedbacks();
        }
}
}
