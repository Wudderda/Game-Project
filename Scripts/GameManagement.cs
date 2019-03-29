using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;

    private HUDManager hudManager;
    private int score;
    private int gameCurrency;
    public int highestScore;
    private int toBeAdded;

    public int Score
    {
        get { return this.score;  }
    }

    public int GameCurrency
    {
        get { return this.gameCurrency;  }
    }

    private void Awake()
    {
        if (!instance)
        {
            gameCurrency = PlayerPrefs.GetInt("GAME_CURRENCY");
            highestScore = PlayerPrefs.GetInt("HIGHEST_SCORE");
            instance = this;
            instance.score = 0;
            instance.toBeAdded = 0;
        }

        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        instance.hudManager = FindObjectOfType<HUDManager>();
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            instance.toBeAdded = instance.score / 50;
            instance.hudManager.UpdateGainText(instance.toBeAdded);
            //IncreaseGameCurrency(0);
        }
    }

    public void IncreaseScore(int amount)
    {
        instance.score += amount;

        if(hudManager)
            hudManager.UpdateScoreText(score);
    }

    public void ResetScore()
    {
        instance.score = 0;

        if(hudManager)
            hudManager.UpdateScoreText(0);
    }

    public void IncreaseGameCurrency(int amount)
    {

        Debug.Log("Game Currency update in gamemanagement called with amount " + amount + " " + toBeAdded);

        if (amount == 0)
            instance.gameCurrency += toBeAdded;
        else
            instance.gameCurrency += amount;

        PlayerPrefsManager.UpdateGameCurrency(instance.gameCurrency);
        if (hudManager)
            hudManager.UpdateCurrencyText(gameCurrency);
    }

    public void SetHighestScore()
    {
        
        if (score <= highestScore)
            return;

        instance.highestScore = score;

        PlayerPrefsManager.UpdateHighestScore(score);
    }

}
