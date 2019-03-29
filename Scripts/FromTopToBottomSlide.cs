using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTopToBottomSlide : MonoBehaviour
{
    public GameObject[] gameObjects;
    public bool isReadyToStart = false;
    public float[] targetPosition;
    public float lerpSpeed = 70f;
    public float delay;

    private bool started = false;
	// Use this for initialization
	void Start ()
    {
        if (isReadyToStart && delay <= 0f)
            StartCoroutine(LerpPosition(this.transform.localPosition, new Vector3(targetPosition[0], targetPosition[1]), 0.4f));
    }

    private void Update()
    {
        if(delay > 0)
            delay -= Time.deltaTime;
        if(!started && delay <= 0f)
            if(isReadyToStart)
                StartCoroutine(LerpPosition(this.transform.localPosition, new Vector3(targetPosition[0], targetPosition[1]), this.name == "Name" ? 0.2f : 1f));
    }

    public IEnumerator LerpPosition(Vector3 start, Vector3 end, float time)
    {
        started = true;
        float startTime = Time.time;
        float endTime = startTime + time;

        while (Time.time < endTime)
        {
            float timeProgressed = (Time.time - startTime) / time;

            transform.localPosition = Vector3.Lerp(start, end, timeProgressed * Time.deltaTime * lerpSpeed);

            yield return new WaitForFixedUpdate();
        }

        if(gameObjects.Length == 2)
            foreach(GameObject mObject in gameObjects)
                mObject.GetComponent<FromTopToBottomSlide>().isReadyToStart = true;

        this.enabled = false;
    }
}
