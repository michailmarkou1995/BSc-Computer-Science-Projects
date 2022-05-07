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

    //private static Score instance = null; // no need when auto property

    public int ScoreCount { get => scoreCount; set => scoreCount = value; }
    public int OldScoreCount { get => oldScoreCount; set => oldScoreCount = value; }
    public TextMeshProUGUI NewScoreText { get => newScoreText; set => newScoreText = value; }

    // can be read by other scripts, but it can only be set from within its own class.
    public static Score Instance { get; private set; } = null; // auto property

    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else
            Instance = this;

        //Set Score to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
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
