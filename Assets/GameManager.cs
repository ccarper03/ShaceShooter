using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _isCoopMode = false;
    [SerializeField]
    private bool _isGameOver = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);// main menu
        }

        if (_isCoopMode)
        {
            if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
            {
                SceneManager.LoadScene(2);// coop
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
            {
                SceneManager.LoadScene(1);// solo
            }
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
