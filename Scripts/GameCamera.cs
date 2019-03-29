using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [Header("Attributes")]
    public float lerpTime = 0.7f;

    [Header("Third Party Objects")]
    public GameObject floor;
    public GameObject wall;
    public GameObject floorCollider;
    public GameObject cube;

    public void SetPosition(Vector3 targetPosition)
    {
        StartCoroutine(LerpPosition(this.transform, this.transform.position, targetPosition, lerpTime));
        floorCollider.transform.position = targetPosition + new Vector3(0f, -7f, 84f);
        cube.transform.position = new Vector3(targetPosition.x, cube.transform.position.y, cube.transform.position.z);
    }

    // Update the position of Camera and Floor smoothly
    public IEnumerator LerpPosition(Transform currTransform, Vector3 start, Vector3 end, float time)
    {
        float startTime = Time.time;
        float endTime = startTime + time;

        while (Time.time < endTime)
        {
            float timeProgressed = (Time.time - startTime) / time;

            currTransform.position = Vector3.Lerp(start, end, timeProgressed);
            floor.transform.position = Vector3.Lerp(floor.transform.position, new Vector3(end.x, floor.transform.position.y, floor.transform.position.z), timeProgressed);
            wall.transform.position = Vector3.Lerp(wall.transform.position, new Vector3(end.x, wall.transform.position.y, wall.transform.position.z), timeProgressed);

            yield return new WaitForFixedUpdate();
        }
    }
}
