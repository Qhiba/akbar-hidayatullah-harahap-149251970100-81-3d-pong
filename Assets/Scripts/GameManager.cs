using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<PaddleController> paddles;
    public List<Goal> goals;

    [SerializeField] GameObject _scoreCanvas;
    [SerializeField] GameObject _gameOverCanvas;
    [SerializeField] Text _winnerText;

    [HideInInspector] public int activePaddles;

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
        Time.timeScale = 1;
        SetGoalAttribute();
        activePaddles = paddles.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameOverCanvas.GetComponent<SceneController>().BackToMainMenu();
        }

        if (activePaddles == 1)
        {
            GameOver();
        }
    }

    public void SetGoalAttribute()
    {
        int i = 0;
        foreach (Goal goal in goals)
        {
            goal.thisPaddle = paddles[i];
            goal.goalIndex = i;
            i++;
        }
    }

    public void DeactivePaddle(int index)
    {
        activePaddles--;
        paddles[index].gameObject.SetActive(false);
    }

    void GameOver()
    {
        Time.timeScale = 0;
        _scoreCanvas.SetActive(false);
        _gameOverCanvas.SetActive(true);
        _winnerText.text = GetWinner() + " Win!";
    }

    string GetWinner()
    {
        int winnerIndex = 0;
        foreach (int score in ScoreManager.Instance.paddleScoreList)
        {
            if (score < ScoreManager.Instance.maxScore)
            {
                winnerIndex = ScoreManager.Instance.paddleScoreList.IndexOf(score);
            }
        }

        return paddles[winnerIndex].gameObject.name;
    }
}
