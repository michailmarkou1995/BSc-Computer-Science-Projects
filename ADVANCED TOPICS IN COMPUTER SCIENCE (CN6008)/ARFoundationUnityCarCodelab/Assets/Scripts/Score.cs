using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText = null;
    public TextMeshProUGUI oldScore;
    public static int scoreCount = -1; // newScoreCount > oldScoreCount
    public static int oldScoreCount;

    private void Update()
    {
        UpdateScore();
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            Score.scoreCount++;
        }
#endif
    }
    private void UpdateScore()
    {
        if (scoreCount != -1)
            ScoreText.text = "New Score: " + scoreCount.ToString();
        else
            ScoreText.text = "New Score: 0";
    }
}
