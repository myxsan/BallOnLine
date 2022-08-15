using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColliderHandler : MonoBehaviour
{
    public float ballSpeed;

    Rigidbody2D ballRigidbody;

    float _ballSpeed;
    Vector3 normal;
    Transform dir;

    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        dir = new GameObject("dirPoint").GetComponent<Transform>();
    }

    private void Start()
    {
        dir.position = Vector3.right;
        _ballSpeed = ballSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 pos = transform.position;
        normal = pos - collision.GetContact(0).normal;
        dir.position = normal;
        dir.RotateAround(transform.position, Vector3.forward, 90f);
        ballRigidbody.velocity = dir.position.normalized * _ballSpeed;
    }

    #region Gizmos
    //Shows the velocity direction
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, dir.position);
        Gizmos.DrawSphere(dir.position, 0.01f);
    }
    #endregion
}
