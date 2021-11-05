using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int totalScore;
    int bestScore;

    public Text scoreText;
    public Text bestText;

    public GameObject gameOverMenu;

    public AudioListener audioListener;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        bestScore = PlayerPrefs.GetInt("bestScore");
        totalScore = 0;

        bestText.text = "Best = " + bestScore;
        scoreText.text = "Score = " + totalScore;

        if(PlayerPrefs.GetInt("audio") == 0)
        {
            AudioListener.volume = 0f;
        }        
        else
        {
            AudioListener.volume = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            loadScene(0);
        }
        
    }

    public void addScore()
    {
        totalScore = totalScore + 1000;
        scoreText.text = "Score = " + totalScore;
    }

    public void gameOver()
    {
        if(totalScore >= bestScore)
        {
            PlayerPrefs.SetInt("bestScore", totalScore);
            bestText.text = "Best = " + bestScore;
        }

        Time.timeScale = 0;

        gameOverMenu.SetActive(true);
    }

    public void loadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void toggleVolume()
    {
        if(AudioListener.volume == 1f)
        {
            AudioListener.volume = 0f;
            PlayerPrefs.SetInt("audio", 0);
        }
        else if(AudioListener.volume == 0f)
        {
            AudioListener.volume = 1f;
            PlayerPrefs.SetInt("audio", 1);
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
