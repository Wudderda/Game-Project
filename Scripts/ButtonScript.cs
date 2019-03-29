using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private int cost = 0;
    private Button button;
    public TextMeshProUGUI text;
    public Image image;
    public int texture_ID;

	void Start ()
    {
        cost = (int)this.GetComponentInParent<TextureScript>().price;
        button = this.GetComponent<Button>();
        button.onClick.AddListener(Purchase);
        if (PlayerPrefs.GetInt("TEXTURE_" + texture_ID.ToString()) == 1)
            SetText();
        else
            text.text = cost.ToString();
	}
	
    void SetText()
    {
        button.enabled = false;
        image.enabled = false;
        text.text = "PURCHASED";
        text.fontSize = 30;
        text.rectTransform.rect.Set(-149.5f, -45.30f, 0f, 0f);
        text.rectTransform.sizeDelta = new Vector2(0f, 0f);
        text.transform.localPosition = new Vector3(0, 0);
    }

    void Purchase()
    {
        if(GameManagement.instance.GameCurrency >= cost)
        {
            GameManagement.instance.IncreaseGameCurrency(-cost);
            PlayerPrefsManager.OpenTextureModel(texture_ID);
            SetText();
        }

    }
}
