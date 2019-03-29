using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public UnityEngine.UI.Image texturesImage;

    private void Update()
    {
        texturesImage.sprite = TextureManager.instance.selectedTextureImage;    
    }

}
