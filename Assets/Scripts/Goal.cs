using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //Goal Atributes
    [HideInInspector] public PaddleController thisPaddle;
    [HideInInspector] public int goalIndex;

    int _score;
    bool _isWall;

    BoxCollider _boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _score = 0;
        _isWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            ScoreManager.Instance.IncreaseScores(goalIndex);
            _score = ScoreManager.Instance.GetPaddleScore(goalIndex);

            if (_score >= ScoreManager.Instance.maxScore && !_isWall)
            {
                ChangeToWall();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            BallManager.Instance.DeactiveBall(other.gameObject);
        }
    }

    public void ChangeToWall()
    {
        _isWall = true;
        _boxCollider.isTrigger = false;
        GameManager.Instance.DeactivePaddle(goalIndex);
        transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
    }
}
