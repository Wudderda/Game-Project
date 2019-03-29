using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissappearEffect : MonoBehaviour
{
    public float lifeTime = 0.5f;

    private float timer;
	// Use this for initialization
	void Start ()
    {
        timer = lifeTime;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
            Destroy(this.gameObject);
	}


}
