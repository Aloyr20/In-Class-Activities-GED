// Modified Timer.cs
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private GameObject timerDisplay;
    private float currentTime = 300f;
    private bool isLevelCompleted = false;
    private bool isLevelFailed = false;

    private void Start()
    {
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (isLevelCompleted || isLevelFailed)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isLevelFailed = true;
            FindObjectOfType<LevelStatus>().SetLevelFailed(true);
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int displayTime = Mathf.CeilToInt(currentTime);
        timerDisplay.GetComponent<NumberDisplayDefinition>()._numericalValue = displayTime.ToString();
    }

    public void CompleteLevel()
    {
        if (!isLevelCompleted && !isLevelFailed)
        {
            isLevelCompleted = true;
            int scoreToAdd = Mathf.FloorToInt(currentTime) * 1000;

            // Add score first
            FindObjectOfType<ScoreCounter>().AddScore(scoreToAdd);
            
            FindObjectOfType<LevelStatus>().SetLevelComplete(true);
        }
    }
}