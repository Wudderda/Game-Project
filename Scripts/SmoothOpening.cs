using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothOpening : MonoBehaviour
{

    [Header("Attributes")]
    public float duration = 1.2f;

    private UnityEngine.UI.Image image;
	// Use this for initialization
	void Start ()
    {
        image = this.GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (duration >= 0)
        {
            duration -= Time.deltaTime;
            float newA = image.color.a - (image.color.a / duration) * Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, newA);
        }

        else
        {
            image.enabled = false;
            this.enabled = false;
        }
    }
}
