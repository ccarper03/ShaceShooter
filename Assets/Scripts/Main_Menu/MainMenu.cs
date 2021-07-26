using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private int _highScore;
    string highScoreKey = "HighScore";
    [SerializeField]
    private TextMeshProUGUI _bestScoreText;
    private void Start()
    {
        _highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        _bestScoreText.text = "Best Score: " + _highScore.ToString();
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit button pressed");
    }
    public void LoadSinglePlayer()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadCoOpMode()
    {
        SceneManager.LoadScene(2);
    }

}
