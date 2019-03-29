using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GainMergeTopObject : MonoBehaviour
{

    public UnityEngine.UI.Image image;
    public TextMeshProUGUI text;
    public Transform targetTransform;

    private Vector3 firstPosition;
    private bool isDone = false;
    private bool second = false;
	
	void Start ()
    {
        firstPosition = this.transform.position;
        StartCoroutine(LerpPosition(this.transform.position, new Vector3(targetTransform.position.x, targetTransform.position.y - 10, targetTransform.position.z), 0.4f));
	}
	
	void Update ()
    {
		if(isDone)
        {
            if(!second)
                GameManagement.instance.IncreaseGameCurrency(0);
            image.enabled = false;
            text.enabled = false;
            isDone = false;
        }
	}

    public IEnumerator LerpPosition(Vector3 start, Vector3 end, float time)
    {
        float startTime = Time.time;
        float endTime = startTime + time;

        while (Time.time < endTime)
        {
            float timeProgressed = (Time.time - startTime) / time;

            transform.position = Vector3.Lerp(start, end, timeProgressed);

            yield return new WaitForFixedUpdate();
        }

        isDone = true;
    }

    public void Reactivate()
    {
        isDone = false;
        this.transform.position = firstPosition;
        image.enabled = true;
        text.enabled = true;
        StartCoroutine(LerpPosition(this.transform.position, new Vector3(targetTransform.position.x, targetTransform.position.y - 10, targetTransform.position.z), 0.4f));
        second = true;
    }
}
