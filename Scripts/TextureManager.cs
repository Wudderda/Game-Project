using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{

    public static TextureManager instance;

    public Sprite selectedTextureImage;
    public Texture selectedTexture;

    [SerializeField] private Texture[] ballTextures;
    [SerializeField] private Sprite[] ballImages;
    private void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        UpdateSelectedTexture();
    }
    

    public void UpdateSelectedTexture()
    {
        // Find selected ball texture
        string selectedTextureName = PlayerPrefs.GetString("SELECTED_TEXTURE");
        int selectedTextureIndex = 0;
        if (selectedTextureName[selectedTextureName.Length - 2] != '_')
            selectedTextureIndex += 10 * (selectedTextureName[selectedTextureName.Length - 2] - '0');
        selectedTextureIndex += selectedTextureName[selectedTextureName.Length - 1] - '0';

        // Assign it to a selected texture
        selectedTexture = ballTextures[selectedTextureIndex];
        selectedTextureImage = ballImages[selectedTextureIndex];
    }


}
