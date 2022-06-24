using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }

    [SerializeField] int _ballSpawnDelay; //In Second
    [SerializeField] int _maxBallOnBoard;

    [SerializeField] GameObject _ballPrefab;
    [SerializeField] Spawner _spawner;

    bool _isSpawning;
    int _activeBall;

    List<BallController> balls;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        balls = new List<BallController>();
        _activeBall= 0;
        _isSpawning = false;
        Invoke("SpawnBall", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isSpawning)
        {
            CheckSpawningCondition();            
        }
    }

    public void SpawnBall()
    {
        BallController ball = GetOrCreateBall();
        if (ball == null)
        {
            return;
        }

        ball.transform.SetParent(_spawner.SetRandomSpawner());
        Transform ballParent = ball.transform.parent;
        ball.transform.position = ballParent.position;

        ball.gameObject.SetActive(true);
        ball.LaunchBall(_spawner.SetRandomDirection(ballParent.name));
        _activeBall++;

        CheckSpawningCondition();
    }

    BallController GetOrCreateBall()
    {
        BallController ball = balls.Find(b => !b.gameObject.activeSelf);        
        if (ball == null)
        {
            ball = Instantiate(_ballPrefab).GetComponent<BallController>();
            balls.Add(ball);
        }
        return ball;
    }

    void CheckSpawningCondition()
    {
        if (_activeBall >= _maxBallOnBoard)
        {
            Debug.Log("Spawn Stop");
            _isSpawning = false;
            return;
        }
        else
        {
            Debug.Log("Spawning");
            _isSpawning = true;
            StartCoroutine(SpawningBall());
        }
    }

    public void DeactiveBall(GameObject ball)
    {
        ball.SetActive(false);
        _activeBall--;
    }

    IEnumerator SpawningBall()
    {
        yield return new WaitForSeconds(_ballSpawnDelay);
        SpawnBall();
    }
}
