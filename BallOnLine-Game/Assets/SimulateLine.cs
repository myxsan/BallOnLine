using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateLine : MonoBehaviour
{
    [SerializeField] Renderer drawPlane;

    public Vector3 drawAreaCenter = Vector3.zero;
    public Vector2 drawAreaSize = Vector3.zero;

    public delegate Vector3 DrawAction(Transform pos, Vector3 pixelPos, Vector3 startPoint, Vector3 lastPoint, Texture2D texture);
    public static DrawAction OnDrawing;

    public Vector3 tempStartPoint { get; set; }

    private void OnEnable()
    {
        OnDrawing += DrawnPointToPosition;
    }

    private void OnDisable()
    {
        OnDrawing -= DrawnPointToPosition;
    }

    // takes the difference between mouse positions and move the transform accordingly;
    private Vector3 DrawnPointToPosition(Transform pos, Vector3 pixelPos, Vector3 startPoint, Vector3 lastPoint, Texture2D texture)
    {
        Vector3 pixDif = startPoint - pixelPos;
        Vector3 dif = pixDif / (Vector2.one * texture.width);

        Vector3 tempPos = pos.position + dif;

        tempStartPoint = lastPoint;
        Vector2 halfSize = drawAreaSize * 0.5f;

        tempPos.x = Mathf.Clamp(tempPos.x, drawAreaCenter.x - halfSize.x,
                                           drawAreaCenter.x + halfSize.x);
        tempPos.y = Mathf.Clamp(tempPos.y, drawAreaCenter.y - halfSize.y,
                                           drawAreaCenter.y + halfSize.y);
        return tempPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(drawAreaCenter, drawAreaSize);
    }
}
