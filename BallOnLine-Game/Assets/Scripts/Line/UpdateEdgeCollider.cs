using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEdgeCollider : MonoBehaviour
{
    TrailRenderer trailRenderer;
    [HideInInspector] public EdgeCollider2D edgeCollider;
    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();

        GameObject colliderGameObject = new GameObject("trailCollider", typeof(EdgeCollider2D));
        edgeCollider = colliderGameObject.GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        SetColliderPoints(trailRenderer, edgeCollider);
    }

    private void SetColliderPoints(TrailRenderer trail, EdgeCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();
        for(int position = 0; position < trail.positionCount; position++)
        {
            points.Add(trail.GetPosition(position));
        }
        collider.SetPoints(points);
    }
}
