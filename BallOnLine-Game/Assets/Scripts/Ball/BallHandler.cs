using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    public float ballSpeed;
    [Tooltip("When the ball is out of x bounds or the direction is higher then ball this value fixes the velocity")]
    [Range(0f, 1f)] public float speedGap = 0.5f;
    [Tooltip("When the direction is higher then ball this value fixes the gravity scale")]
    [Range(0f, 1f)] public float gravityScaleGap = 0.5f;

    Rigidbody2D ballRigidbody;
    Transform mainCamPos;

    float _ballSpeed;
    float baseGravityScale;

    float xBoundsMagnitude = 1f;

    Vector3 normal;
    Transform direction;

    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        mainCamPos = Camera.main.GetComponent<Transform>();

        direction = new GameObject("dirPoint").GetComponent<Transform>();
    }

    private void Start()
    {
        direction.position = Vector3.right;
        baseGravityScale = ballRigidbody.gravityScale;
        _ballSpeed = ballSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 pos = transform.position;

        normal = pos - collision.GetContact(0).normal;

        //Gets the normal and rotates it 90 degrees around the object, ta daaa the direction :)
        direction.position = normal;
        direction.RotateAround(transform.position, Vector3.forward, 90f);

        ballRigidbody.velocity = CheckDirOnY(direction.position, CheckXBounds(_ballSpeed, speedGap), speedGap, gravityScaleGap);
    }

    //Adds or subtracts speed gap due to direction on y axis
    Vector3 CheckDirOnY(Vector3 _direction, float speed, float speedGap, float gravityGap)
    {
        if(transform.position.y - _direction.y < 0)
        {
            ballRigidbody.gravityScale = baseGravityScale - gravityGap;
            return _direction.normalized * (speed + speedGap);
        }
        else
        {
            ballRigidbody.gravityScale = baseGravityScale;
            return _direction.normalized * speed;
        }
    }


    //Adds or subtracts speed gap due to the ball's position on x axis
    float CheckXBounds(float speed, float gap)
    {
        if(transform.position.x - (mainCamPos.position.x - xBoundsMagnitude) <= 0)
        {
            return speed + gap;
        }
        else if(transform.position.x - (mainCamPos.position.x + xBoundsMagnitude) >= 0)
        {
            return speed - gap;
        }
        else
        {
            return speed;
        }
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        //Shows the velocity direction
        if (!Application.isPlaying) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, direction.position);
        Gizmos.DrawSphere(direction.position, 0.01f);

        //Shows x bounds for adding or subtracting the speed gap value
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector2(Camera.main.transform.position.x - xBoundsMagnitude,
                        Camera.main.transform.position.y + 1),0.05f);
        Gizmos.DrawSphere(new Vector2(mainCamPos.position.x + xBoundsMagnitude,
                mainCamPos.position.y + 1), 0.05f);
    }
    #endregion
}
