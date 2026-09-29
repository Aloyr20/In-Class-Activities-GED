using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteBox : MonoBehaviour
{
    [SerializeField] List<Sprite> powerupList;

    float changeTimer = 0;
    int previousChoice = 5;
    bool itemCollected;

    void Update()
    {
        if (!itemCollected)
        {
            ChangeImage();
        }
    }

    void ChangeImage()
    {
        if (changeTimer < Time.realtimeSinceStartup)
        {
            int choice = (int)Random.Range(0, powerupList.Count - 1);

            if (choice == previousChoice)
            {
                ChangeImage();
                return;
            }

            GetComponent<SpriteRenderer>().sprite = powerupList[choice];
            changeTimer = Time.realtimeSinceStartup + 0.25f;
            previousChoice = choice;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Get reference to Timer and trigger completion sequence
        GameTimer timer = FindObjectOfType<GameTimer>();
        if (timer != null)
        {
            timer.CompleteLevel();
        }

        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("LevelClear");
        itemCollected = true;
    }
}