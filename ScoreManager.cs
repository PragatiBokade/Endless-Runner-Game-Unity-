using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance
    public Text scoreTxt;
    private int score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign instance
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate ScoreManager instances
        }
    }

    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreTxt.text = "Points: " + score.ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
}
