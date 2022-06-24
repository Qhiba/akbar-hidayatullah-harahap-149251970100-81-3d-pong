using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PaddleController : MonoBehaviour
{
    [Header("Move Direction")]
    [SerializeField] bool _horizontalDirection;
    [SerializeField] bool _verticalDirection;

    [Header("Paddle Boundries")]
    [SerializeField] float _horizontalBoundryMax;
    [SerializeField] float _horizontalBoundryMin;
    [SerializeField] float _verticalBoundryMax;
    [SerializeField] float _verticalBoundryMin;

    [Header("Paddle Atributes")]
    [SerializeField] float _speed;    

    [Header("Movement Input")]
    public KeyCode positiveDirection;
    public KeyCode negativeDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
    }

    void MovePaddle(Vector3 direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime);
        Vector3 currentPos = transform.position;

        if (_horizontalDirection)
        {
            float xPos = Mathf.Clamp(currentPos.x, _horizontalBoundryMin, _horizontalBoundryMax);
            transform.position = new Vector3(xPos, currentPos.y, currentPos.z);
        }
        else if (_verticalDirection)
        {
            float zPos = Mathf.Clamp(currentPos.z, _verticalBoundryMin, _verticalBoundryMax);
            transform.position = new Vector3(currentPos.x, currentPos.y, zPos);
        }
    }

    void MovementInput()
    {
        Vector3 direction = Vector3.zero;

        if (_horizontalDirection)
        {
            if (Input.GetKey(positiveDirection))
            {
                direction = Vector3.right;
            }
            else if (Input.GetKey(negativeDirection))
            {
                direction = Vector3.left;
            }
        }
        else if (_verticalDirection)
        {
            if (Input.GetKey(positiveDirection))
            {
                direction = Vector3.forward;
            }
            else if (Input.GetKey(negativeDirection))
            {
                direction = Vector3.back;
            }
        }

        MovePaddle(direction);
    }

    #region Get Paddle Movement
    //To give a value on BallController
    public bool isHorizontal()
    {
        return _horizontalDirection;
    }

    public bool isVertical()
    {
        return _verticalDirection;
    }
    #endregion
}