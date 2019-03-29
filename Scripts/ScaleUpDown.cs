using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpDown : MonoBehaviour
{
    [Header("Attributes")]
    public float targetScale = 1.5f;
    public bool isScaling = false;
	
	// Update is called once per frame
	void Update ()
    {
        if (!isScaling)
            StartCoroutine(LerpPosition(this.transform.localScale, Vector3.one * targetScale, 0.4f));
	}

    public IEnumerator LerpPosition(Vector3 start, Vector3 end, float time)
    {
        isScaling = true;
        float startTime = Time.time;
        float endTime = startTime + time;

        while (Time.time < endTime)
        {
            float timeProgressed = (Time.time - startTime) / time;

            transform.localScale = Vector3.Lerp(start, end, timeProgressed);

            yield return new WaitForFixedUpdate();
        }

        targetScale = targetScale == 1.5f ? 1f : 1.5f;
        isScaling = false;
    }

}
