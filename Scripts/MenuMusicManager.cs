using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    public static MenuMusicManager instance;

    private AudioSource audioSource;
    // Use this for initialization
    private void Awake()
    {
        if (!instance)
        {
            audioSource = this.GetComponent<AudioSource>();
            instance = this;
            audioSource.Play();
            audioSource.volume = 1f * PlayerPrefs.GetInt("SOUND_KEY");
        }

        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

}
