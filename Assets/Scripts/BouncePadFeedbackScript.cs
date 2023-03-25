using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class BouncePadFeedback : MonoBehaviour
{
    
    public MMFeedbacks bouncePadFeedback;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
    
        if(col.gameObject.tag =="Player")
        {
            bouncePadFeedback?.PlayFeedbacks();
        }
    }
}
