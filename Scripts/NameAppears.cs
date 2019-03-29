using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameAppears : MonoBehaviour {


    public float targetPoint = -152.7f;

	// Use this for initialization
	void Start ()
    {
	    foreach(Transform child in this.transform)
        {
            StartCoroutine(LerpPosition(child, child.transform.localPosition, new Vector3(targetPoint, child.transform.localPosition.y, child.transform.localPosition.z), 0.9f));
            targetPoint += 100;
        }

        this.enabled = false;
	}

    public IEnumerator LerpPosition(Transform child, Vector3 start, Vector3 end, float time)
    {
        float startTime = Time.time;
        float endTime = startTime + time;

        while (Time.time < endTime)
        {
            float timeProgressed = (Time.time - startTime) / time;

            child.transform.localPosition = Vector3.Lerp(start, end, timeProgressed);

            yield return new WaitForFixedUpdate();
        }
    }
}
