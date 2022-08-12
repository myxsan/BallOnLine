using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawController : MonoBehaviour
{
    [SerializeField] GameObject drawPlane;
    [SerializeField] Color paintColor = Color.white;
    [SerializeField] Color baseColor = Color.black;
    public GameObject obj;


    Vector2 lastPoint = Vector2.zero;
    private static List<Vector2> drawnPoints = new List<Vector2>();
    
    Texture2D texture;
    DrawToPos drawToPos;

    public static List<Vector2> DrawnPoints { 
        get 
        { return drawnPoints; }
        set
        {
            if (drawnPoints[0] == null)
                drawnPoints = new List<Vector2>();
        }
    }
    
    void Start()
    {
        drawToPos = GetComponent<DrawToPos>();
        drawToPos.tempStartPoint = obj.transform.position;

        texture = Instantiate(drawPlane.GetComponent<Renderer>().material.mainTexture) as Texture2D;
        for (int y = 0; y < texture.height; y++) // paint all pixels black
        {
            for (int x = 0; x < texture.width; x++)
            {
                    texture.SetPixel(x, y, Color.black);
            }
        }
        drawPlane.GetComponent<Renderer>().material.mainTexture = texture;
        texture.Apply();
    }
    void Update()
    {
        //record start of mouse drawing to get the first position the mouse touches down
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit ray;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
            {
                lastPoint = new Vector2((int)(ray.textureCoord.x * texture.width),
                                        (int)(ray.textureCoord.y * texture.height));

                drawToPos.tempStartPoint = lastPoint;
            }
        }

        //draw a line between the last known location of the mouse and the current location
        if (Input.GetMouseButton(0))
        {
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
            {
                DrawPixelLine((int)(ray.textureCoord.x * texture.width),
                                   (int)(ray.textureCoord.y * texture.height),
                                   (int)lastPoint.x, (int)lastPoint.y, paintColor, texture);

                lastPoint = new Vector2((int)(ray.textureCoord.x * texture.width),
                                        (int)(ray.textureCoord.y * texture.height));
            }

            texture.Apply();
        }
    }

    //Makes the particular pixel turn back to base color after few seconds drawing it
    IEnumerator EreasePoint(int x, int y, float ereaseTime)
    {
        yield return new WaitForSeconds(0.18f); //Time to wait before starting ereasing
        while(texture.GetPixel(x, y) != baseColor)
        {
            texture.SetPixel(x, y, Color.Lerp(paintColor, baseColor, ereaseTime));

            texture.Apply();
            yield return new WaitForSeconds(1f);
        }
    }

    //For more information on the algorithm see: https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
    void DrawPixelLine(int x, int y, int x2, int y2, Color color, Texture2D texture)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);
        if (!(longest > shortest))
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            texture.SetPixel(x, y, color);
            
            obj.transform.position = DrawToPos.OnDrawing(obj.transform, new Vector2(x, y), drawToPos.tempStartPoint, lastPoint, texture);
            StartCoroutine(EreasePoint(x, y, 1f)); //starts the ereasing sequence

            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }
        texture.Apply();
    }
}