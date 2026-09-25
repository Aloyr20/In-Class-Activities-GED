using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Chapter.Singleton;
public class LevelStatus : MonoBehaviour
{
    bool levelCompleted = false;
    bool levelFailed = false;
    bool gameOver = false;

    public string _levelFailedScene;
    public string _levelCompleteScene;
    public string _gameOverScene;

    [SerializeField] float completionDelay = 5f;

    // Update is called once per frame

    void Update()
    {
        if (levelCompleted)
        {
            ScoreCounter.Instance.GetScore();
            DelayedLevelComplete();
        }
        if (levelFailed)
        {
            SceneManager.LoadScene(_levelFailedScene);
            ScoreCounter.Instance.ResetScore();
        }
        if (gameOver)
        {
            SceneManager.LoadScene(_gameOverScene);
            ScoreCounter.Instance.ResetScore();
        }
    }

    public void SetLevelComplete(bool complete)
    {
        levelCompleted = complete;
    }

    public void SetLevelFailed(bool failed)
    {
        levelFailed = failed;
    }

    public void SetGameOver(bool isGameOver)
    {
        gameOver = isGameOver;
    }

    public void DelayedLevelComplete()
    {
        StartCoroutine(DelayedTransition());
    }

    private IEnumerator DelayedTransition()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(_levelCompleteScene);
    }
}