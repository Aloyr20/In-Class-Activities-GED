using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Chapter.Singleton
{
    public class ScoreCounter : Singleton<ScoreCounter>
    {
        private Text scoreDisplay;

        private int score = 0;

         public override void Awake()
 {
            base.Awake(); 

            GameObject scoreObj = GameObject.FindWithTag("ScoreDisplay");

            if (scoreObj != null)
            {
                scoreDisplay = scoreObj.GetComponent<Text>();
            }
            
            UpdateScoreUI();
}

        public int GetScore()
        {
            return score;
        }

        public void AddScore(int amount)
        {
            score += amount;
            UpdateScoreUI();

        }

        public void RemoveScore(int amount)
        {
            score -= amount;
            UpdateScoreUI();
        }

        public void ResetScore()
        {
            score = 0;
            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            if(scoreDisplay == null)
            {
                GameObject scoreObj = GameObject.FindWithTag("ScoreDisplay");
                if (scoreObj != null)
                {
                    scoreDisplay = scoreObj.GetComponent<Text>();
                }
            }
            if (scoreDisplay != null)
            {
                scoreDisplay.text = score.ToString();
            }
        }
    }
}
