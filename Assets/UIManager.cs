using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    private GameManager _gameManager; 

    void Start()
    {
        // assign text component to the handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("GameManager is Null.");
        }
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
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
