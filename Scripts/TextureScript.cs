using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TextureScript : MonoBehaviour
{
    public float[] backgroundRGB;
    public Sprite[] sprites;
    public GameObject icon;
    public GameObject greenBorder;
    public GameObject backgroundImageObject;

    public float price = 0f;
    private Button button;
    private Image iconImage;
    private Image greenBorderImage;
    private Image backgroundImage;
    // Use this for initialization
	void Start ()
    {
        backgroundImage = backgroundImageObject.GetComponent<Image>();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(ChangeSprite);
        iconImage = icon.GetComponent<Image>();
        greenBorderImage = greenBorder.GetComponent<Image>();
        UpdateChildObjects();
	}

    private void Update()
    {
        UpdateChildObjects();
    }

    void UpdateChildObjects()
    {
        if (PlayerPrefs.GetInt(this.name) == 1)
        {
            backgroundImage.color = new Color(backgroundRGB[0] / 255f, backgroundRGB[1] / 255f, backgroundRGB[2] / 255f);
            iconImage.sprite = sprites[1];
        }
        else
        {
            backgroundImage.color = new Color(111f / 255f, 111f / 255f, 109f / 255f);
            iconImage.sprite = sprites[0];
        }  

        if (PlayerPrefs.GetString("SELECTED_TEXTURE") == this.name)
            greenBorderImage.color = new Color(0f, 107f, 13f);
        else
            greenBorderImage.color = new Color(202f, 202f, 202f);
    }

    void ChangeSprite()
    {
        if (PlayerPrefs.GetInt(this.name) != 1)
            return;

        // Update TextureManager
        PlayerPrefs.SetString("SELECTED_TEXTURE", this.name);
        TextureManager.instance.UpdateSelectedTexture();
    }

}
