using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Ball Attributes")]
    [SerializeField] float _speed;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>(); // I don't know why this line doesn't get called on start when calling LaunchBall function on BallManager. 
    }

    //Launch ball at random direction when it spawned by Spawner
    public void LaunchBall(Vector3 direction)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
        _rb.AddForce(direction.normalized * _speed);
    }

    #region Ball Hit a Paddle
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Paddle")
        {
            PaddleController paddleController = collision.transform.GetComponent<PaddleController>();
            Vector3 paddlePos = collision.transform.position;
            float horizontalOffset = this.transform.position.x - paddlePos.x;
            float verticalOffset = this.transform.position.z - paddlePos.z;

            if (paddleController.isHorizontal())
            {
                PushBallByHorizontalPaddle(horizontalOffset, paddlePos.z);
            }
            else
            {
                PushBallByVerticalPaddle(verticalOffset, paddlePos.x);
            }
        }
    }

    void ChangeDirection(float horizontal, float vertical)
    {
        Vector3 newVelocity = new Vector3(horizontal, 0.0f, vertical).normalized * _rb.velocity.magnitude;
        _rb.velocity = newVelocity;
    }

    //Push ball when it hit an area of the Paddle
    void PushBallByHorizontalPaddle(float offset, float paddlePos)
    {
        float horizontalDirection = 1.0f; //Default Number goind right
        float randomVerticalDirection = 1.0f; //Default number going upward

        if (offset < 0.0f) //Ball Going to Left Direction
        {
            horizontalDirection = -horizontalDirection;
        }

        if (paddlePos > 0) //Top Paddle Hit
        {
            randomVerticalDirection = Random.Range(-0.7f, -0.2f);
        }
        else //Bottom Paddle Hit
        {
            randomVerticalDirection = Random.Range(0.3f, 0.8f);
        }

        ChangeDirection(horizontalDirection, randomVerticalDirection);
    }

    void PushBallByVerticalPaddle(float offset, float paddlePos)
    {
        float horizontalDirection = 1.0f; //Default Number goind right
        float randomVerticalDirection = 1.0f; //Default number going upward

        if (paddlePos > 0.0f) //Right Paddle Hit
        {
            horizontalDirection = -horizontalDirection;
        }

        if (offset > 0) //Ball Going Upward
        {
            randomVerticalDirection = Random.Range(0.3f, 0.8f);
        }
        else //Ball Going Down
        {
            randomVerticalDirection = Random.Range(-0.7f, -0.2f);
        }

        ChangeDirection(horizontalDirection, randomVerticalDirection);
    }
    #endregion
}
