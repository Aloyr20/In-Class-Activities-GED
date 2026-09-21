using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] GameObject livesDisplay;

    int currentLives;

    // Update is called once per frame

    void Update()
    {
        currentLives = PlayerPrefs.GetInt("Lives");

        if (currentLives < 1)
        {
            GetComponent<LevelStatus>().SetGameOver(true);
        }

        livesDisplay.GetComponent<NumberDisplayDefinition>()._numericalValue = currentLives.ToString();
    }

    public void LoseLife()
    {
        currentLives--;
        PlayerPrefs.SetInt("Lives", currentLives);
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }
}