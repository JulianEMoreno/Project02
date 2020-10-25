using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip _startingSong;
    [SerializeField] Text _highScoreTextView;
    // Start is called before the first frame update
    void Start()
    {
        //load high score display
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();

        if(_startingSong != null)
        {
            AudioManager.Instance.PlaySong(_startingSong);
        }
    }
    public void EraseData()
    {
        Debug.Log("Erase Highscore");
         PlayerPrefs.SetInt("HighScore", 0);
        //update score display, so we can see the new score
        _highScoreTextView.text = "0 ";
    }

}
