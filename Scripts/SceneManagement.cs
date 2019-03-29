using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public float autoLoadTimeDelay = 2f;

    private void Awake()
    {
        if (autoLoadTimeDelay != 0)
            Invoke("LoadNextScene", autoLoadTimeDelay);
    }

    public void LoadSceneAndReset(string name)
    {
        GameManagement.instance.ResetScore();
        SceneManager.LoadScene(name);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void QuitApplication()
    {
        Application.Quit();
    }
}
