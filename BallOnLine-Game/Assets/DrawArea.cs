using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArea : MonoBehaviour
{
    public static Vector3 DrawAreaCenter;
    public static Vector3 DrawAreaSize;
    public static Vector3 WorldSpeed;

    [Header("DrawArea Values")]
    [SerializeField] Vector3 drawAreaCenter = Vector3.zero;
    [SerializeField] Vector3 drawAreaSize = Vector3.zero;

    [Tooltip("Switch the world speed value for camera and the pen game object")]
    [SerializeField] Vector3 worldSpeed = Vector3.zero;

    private void Start()
    {
        DrawAreaSize = drawAreaSize;
        WorldSpeed = worldSpeed;
    }

    private void Update()
    {
        drawAreaCenter.x = transform.position.x;
        DrawAreaCenter = drawAreaCenter;

    }

    private void FixedUpdate()
    {
        transform.Translate(WorldSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(drawAreaCenter, drawAreaSize);
    }
}
