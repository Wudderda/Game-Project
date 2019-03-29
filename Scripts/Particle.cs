using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float lifeTime = 1.5f;
    // Use this for initialization
	void Start ()
    {
        //Invoke("DestroyItself", lifeTime);
	}


    void DestroyItself()
    {
        //Destroy(this.gameObject);
    }
	    
}
