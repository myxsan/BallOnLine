using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulateDraw : MonoBehaviour
{
    [SerializeField] GameObject drawPlane;
    Vector2Int lastPoint = Vector2Int.zero;
    float rayDistance;

    Texture2D texture;
    void Start()
    {
        texture = Instantiate(drawPlane.GetComponent<Renderer>().material.mainTexture) as Texture2D;
        for (int y = 0; y < texture.height; y++) // makes texture black
        {
            for (int x = 0; x < texture.width; x++)
            {
                    texture.SetPixel(x, y, Color.black);
            }
        }
        drawPlane.GetComponent<Renderer>().material.mainTexture = texture;
        texture.Apply();

        rayDistance = drawPlane.transform.position.z - Camera.main.transform.position.z + 1;
        //rayDistance = drawPanel.transform.parent.GetComponentInParent<Canvas>().planeDistance + 1;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit _ray;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _ray))
            {
                lastPoint = new Vector2Int((int)(_ray.textureCoord.x * texture.width),
                                        (int)(_ray.textureCoord.y * texture.height));
            }

            if(_ray.collider)
            {
                Debug.Log("hey " + _ray.collider.name + " " + lastPoint);
            }

            texture.SetPixel(lastPoint.x, lastPoint.y, Color.white);
            texture.Apply();
        }
    }
}