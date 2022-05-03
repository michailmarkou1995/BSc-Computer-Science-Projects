using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newScoreText = null;
    public TextMeshProUGUI oldScore;
    private int scoreCount = 0; // newScoreCount > oldScoreCount
    private int oldScoreCount;

    public static Score Instance = null;

    public int ScoreCount { get => scoreCount; set => scoreCount = value; }
    public int OldScoreCount { get => oldScoreCount; set => oldScoreCount = value; }
    public TextMeshProUGUI NewScoreText { get => newScoreText; set => newScoreText = value; }

    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        UpdateScore();
    }
    private void UpdateScore()
    {
            newScoreText.text = "New Score: " + ScoreCount.ToString();
    }
}
