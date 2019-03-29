using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    static string highestScoreKey = "HIGHEST_SCORE";
    static string gameCurrencyKey = "GAME_CURRENCY";
    static string textureKey = "TEXTURE_";
    static string selectedTexture = "SELECTED_TEXTURE";
    static string soundKey = "SOUND_KEY";

    public static PlayerPrefsManager instance;

	void Awake ()
    {
        if (!instance)
        {
            if (!PlayerPrefs.HasKey(highestScoreKey))
                PlayerPrefs.SetInt(highestScoreKey, 0);
            if (!PlayerPrefs.HasKey(gameCurrencyKey))
                PlayerPrefs.SetInt(gameCurrencyKey, 0);

            
            for (int i = 1; i < 25; ++i)
            {
                if (!PlayerPrefs.HasKey(textureKey + i.ToString()))
                    PlayerPrefs.SetInt(textureKey + i.ToString(), 0);
            }

            
            PlayerPrefs.SetInt(textureKey + 0.ToString(), 1);

            if (!PlayerPrefs.HasKey(selectedTexture))
                PlayerPrefs.SetString(selectedTexture, "TEXTURE_0");

            
            if (!PlayerPrefs.HasKey(soundKey))
                PlayerPrefs.SetInt(soundKey, 1);

            instance = this;
        }

        else if (instance != this)
            Destroy(this.gameObject);

        
        DontDestroyOnLoad(this.gameObject);
	}

    

    public static void UpdateHighestScore(int amount)
    {
        PlayerPrefs.SetInt(highestScoreKey, amount);
    }

    public static void UpdateGameCurrency(int amount)
    {
        Debug.Log("Caution currency changed. Amount to be passed: " + amount);
        PlayerPrefs.SetInt(gameCurrencyKey, amount);
    }

    public static void OpenTextureModel(int index)
    {
        PlayerPrefs.SetInt(textureKey + index.ToString(), 1);
    }

    public static void UpdateSound(int volume)
    {
        PlayerPrefs.SetInt(soundKey, volume);
    }
}
