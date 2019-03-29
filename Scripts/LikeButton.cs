using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeButton : MonoBehaviour
{

    public UnityEngine.UI.Button button;

	// Use this for initialization
	void Start ()
    {
        button.onClick.AddListener(RouteToGooglePlay);
	}

    void RouteToGooglePlay()
    {
        Application.OpenURL("market://details?id=" + Application.productName);
    }
}
