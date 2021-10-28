using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Text healtText, scoreText, timeText, finalScore;
    [SerializeField] GameObject gameOverScreen;

    //Todos los managr deben ser singleton.
    private void Awake() {
        if (Instance== null) {
            Instance = this;
        }
    }


    public void UpdateUIScore(int newScore) {
        scoreText.text = newScore.ToString();
    }
    public void UpdateUIHealth(int newHealt) {
        healtText.text = newHealt.ToString();
    }
    public void UpdateUITime(int newTime) {
        timeText.text = newTime.ToString();
    }

    public void ShowGameOverScreen() {
        gameOverScreen.SetActive(true);
        finalScore.text = "SCORE: " + GameManager.Instance.Score;
    }
}
