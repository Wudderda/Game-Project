using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highestScoreText;
    public TextMeshProUGUI gainText;

	// Use this for initialization
	void Start ()
    {
        // Update Text Objects in the current Scene
        UpdateScoreText(GameManagement.instance.Score);
        UpdateCurrencyText(GameManagement.instance.GameCurrency);

        // Enable/Disable UI Elements

        if (SceneManager.GetActiveScene().name == "CoreGame")
            scoreText.enabled = true;

        else if(SceneManager.GetActiveScene().name == "GameOver")
        {
            UpdateHighestScoreText(PlayerPrefs.GetInt("HIGHEST_SCORE"));
            scoreText.enabled = true;
        }

        else
            scoreText.enabled = false;

	}

    public void UpdateCurrencyText(int amount)
    {
        currencyText.text = amount.ToString();
    }

    public void UpdateScoreText(int amount)
    {
        scoreText.text = amount.ToString();
    }

    public void UpdateHighestScoreText(int amount)
    {
        highestScoreText.text = "BEST\n" + amount.ToString();
    }

    public void UpdateGainText(int amount)
    {
        gainText.text = "+" + amount.ToString();
    }

}
