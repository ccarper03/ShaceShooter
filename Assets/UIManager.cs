using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _bestScoreText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartText;
    [SerializeField]
    private GameObject _pauseMenuPanel;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    private bool _isPaused = false;
    private GameManager _gameManager;
    [SerializeField]
    private Animator _anim;
    private int _highScore;
    string highScoreKey = "HighScore";
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _anim = _pauseMenuPanel.GetComponent<Animator>();
        _pauseMenuPanel.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        
        if (_gameManager == null)
        {
            Debug.LogError("GameManager is Null.");
        }

        UpdateHighscore(_highScore);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        _pauseMenuPanel.gameObject.SetActive(true);// must be active before animating
        _isPaused = true;
        _anim.SetBool("isPaused",true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        _anim.SetBool("isPaused", false);// not giving ths 
        _pauseMenuPanel.gameObject.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);// main menu
    }
    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
        
    }
    public void UpdateHighscore(int highscore)
    {
        _bestScoreText.text = "Best: " + _highScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence(currentLives);
        }
    }

    private void GameOverSequence(int currentLives)
    {
        StartCoroutine(GameOverFlicker(currentLives));
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }

    private IEnumerator GameOverFlicker(int currentLives)
    {
        while (currentLives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.7f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.7f);
        }
    }
}
