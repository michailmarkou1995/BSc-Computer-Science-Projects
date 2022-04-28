using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText = null;
    public static int scoreCount = -1;

    private void Update()
    {
        UpdateScore();
    }
    private void UpdateScore()
    {
        if (scoreCount != -1)
            ScoreText.text = "New Score: " + scoreCount.ToString();
        else
            ScoreText.text = "New Score: 0";

    }
}
