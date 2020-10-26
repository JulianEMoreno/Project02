using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Level03Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] Canvas _pauseMenu;
    [SerializeField] Canvas _deathMenu;
    int _currentScore;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // load new level
            SceneManager.LoadScene("MainMenu");
        }
        if (_currentScore == 35)
        {
            NextLevel();
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitLevel()
    {
        Debug.Log("Exit");

        //compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore)
        {
            //save current score as new high score
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score: " + _currentScore);
        }

        // load new level
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //IncreaseScore score
        _currentScore += scoreIncrease;
        //update score display, so we can see the new score
        _currentScoreTextView.text =
                "Score: " + _currentScore.ToString();
    }
}
