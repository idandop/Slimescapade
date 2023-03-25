using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class awake : MonoBehaviour
{
    // Start is called before the first frame update
   public ParticleSystem jump;
    // Update is called once per frame
    void Awake()
    {
        jump.Play();
        if (gameObject.name.Contains("(Clone)"))
        {
        Destroy(gameObject,3f);
        }
    }
    void Update()
    {
      //  jump.Play();
    }
}
