using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulateDraw : MonoBehaviour
{
    [SerializeField] RawImage drawPanel;
    Vector2 lastPoint = Vector2.zero;
    Texture2D texture;
    void Start()
    {
        texture = Instantiate(drawPanel.mainTexture) as Texture2D;
        //for (int y = 0; y < texture.height; y++) // for testing the texture
        //{
        //    for (int x = 0; x < texture.width; x++)
        //    {
        //        if (Mathf.PerlinNoise(x / 20.0f, y / 20.0f) * 100 < 40)
        //            texture.SetPixel(x, y, Color.yellow);
        //    }
        //}
        drawPanel.texture = texture;
        texture.Apply();
    }

    void Update()
    {
        
    }
}
