using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isDone = false;
    public GameObject obj;

    public float rotation;
    private void OnCollisionEnter(Collision collision)
    {
        Invoke("DestroyItself", 1f);
    }

    void DestroyItself()
    {
        obj.GetComponent<SceneManagement>().LoadScene("GameOver");
        Destroy(this.gameObject);
    }

    private void Update()
    {
        rotation = this.transform.localEulerAngles.y;
        if (isDone)
            EndlessRotation();
    }

    private void EndlessRotation()
    {
        rotation += 100f * Time.deltaTime;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(this.transform.localEulerAngles.x, rotation, this.transform.localEulerAngles.z), 100f * Time.deltaTime);
    }
}
