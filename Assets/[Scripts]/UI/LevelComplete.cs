using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    public GameObject scoreScreen;
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        Events.onLevelEnd.AddListener(DisplayScore);   
    }
    private void OnDisable()
    {
        Events.onLevelEnd.RemoveListener(DisplayScore);
    }
    public void DisplayScore(float score)
    {
        scoreScreen.gameObject.SetActive(true);
        scoreText.text = score.ToString();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
