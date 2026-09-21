using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            FindObjectOfType<ScoreCounter>().GetScore();
            DelayedLevelComplete();
        }
        if (levelFailed)
        {
            SceneManager.LoadScene(_levelFailedScene);
        }
        if (gameOver)
        {
            SceneManager.LoadScene(_gameOverScene);
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