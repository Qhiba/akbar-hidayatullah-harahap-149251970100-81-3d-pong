using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] int _scoreValue = 1;
    public int maxScore = 15;

    public List<Text> scoreTexts;

    [HideInInspector] public List<int> paddleScoreList;

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
        paddleScoreList = new List<int>();

        foreach (var text in scoreTexts)
        {
            if (text.IsActive())
            {
                text.text = ($"Paddle {scoreTexts.IndexOf(text) + 1}\n 0");
                paddleScoreList.Add(0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScores(int index)
    {
        paddleScoreList[index] += _scoreValue;
        scoreTexts[index].text = string.Format("Paddle {0}\n {1}", (index + 1).ToString(), paddleScoreList[index].ToString()); 
    }

    public int GetPaddleScore(int index)
    {
        return paddleScoreList[index];
    }
}
