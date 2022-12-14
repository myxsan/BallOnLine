using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawToPos : MonoBehaviour
{
    public delegate Vector3 DrawAction(Transform pos, Vector3 pixelPos, ref Vector3 startPoint, Vector3 lastPoint, Texture2D texture);
    public static DrawAction OnDrawing;


    private void OnEnable()
    {
        OnDrawing += DrawnPointToPosition;
    }

    private void OnDisable()
    {
        OnDrawing -= DrawnPointToPosition;
    }

    // takes the difference between mouse positions and move the object position accordingly;
    private Vector3 DrawnPointToPosition(Transform pos, Vector3 pixelPos, ref Vector3 startPoint, Vector3 lastPoint, Texture2D texture)
    {
        Vector3 pixDif = startPoint - pixelPos;
        Vector3 dif = pixDif / (Vector2.one * texture.width); // width and height are same, if it is not change it.

        if(dif.x < 0)
        {
            dif.x -= DrawArea.WorldSpeed.x * Time.deltaTime;
        }

        Vector3 tempPos = pos.position + dif;

        startPoint = lastPoint;
        Vector2 halfSize = DrawArea.DrawAreaSize * 0.5f;

        tempPos.x = Mathf.Clamp(tempPos.x, DrawArea.DrawAreaCenter.x - halfSize.x,
                                           DrawArea.DrawAreaCenter.x + halfSize.x);
        tempPos.y = Mathf.Clamp(tempPos.y, DrawArea.DrawAreaCenter.y - halfSize.y,
                                           DrawArea.DrawAreaCenter.y + halfSize.y);
        return tempPos;
    }
}
