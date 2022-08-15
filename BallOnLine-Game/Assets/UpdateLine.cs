using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLine : MonoBehaviour
{

    float halfWidth;
    void Start()
    {
        halfWidth = DrawArea.DrawAreaSize.x * 0.5f;
        Vector3 startPos = new Vector3(-halfWidth, 0f, 0f);
        transform.position = startPos;
        StartCoroutine(SetStartPos(Vector3.zero));
    }

    private void Update()
    {
        transform.Translate(DrawArea.WorldSpeed * Time.deltaTime);
    }

    IEnumerator SetStartPos(Vector3 pos)
    {
        yield return null;
        transform.position = pos;
    }
}