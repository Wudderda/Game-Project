using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public Sprite[] sprites;
    public UnityEngine.UI.Button button;

    public int currentSpriteIndex;

	// Use this for initialization
	void Start ()
    {
        currentSpriteIndex = PlayerPrefs.GetInt("SOUND_KEY");
        UpdateSprite();
	}
	
    // Switch on/off volume
    public void UpdateSoundSettings()
    {
        currentSpriteIndex = currentSpriteIndex == 1 ? 0 : 1;
        PlayerPrefsManager.UpdateSound(currentSpriteIndex);
        UpdateSprite();
    }

    void UpdateSprite()
    {
        button.GetComponent<UnityEngine.UI.Image>().sprite = sprites[currentSpriteIndex];
    }
}
