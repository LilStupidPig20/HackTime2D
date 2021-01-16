using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject finishMenuUI;
    public GameObject coinUI;
    public int Snake = 0;
    public static int Lose = 0;

    void Update()
    {
        InputButton();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Obstacle")
        {
            Pause();
        }
        if (other.name == "Finish")
        {
            Finish();
        }
        if(other.name == "Coin" && Lose != 4)
        {
            coinUI.SetActive(true);
            Snake = 1;
        }

        if (other.name == "UnCoin")
        {
            coinUI.SetActive(false);
            Snake = 0;
        }

        if ((other.name == "Coin" || other.name == "UnCoin") && Lose == 3)
        {
            Destroy(other.gameObject);
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Lose = 1;
    }

    void Finish()
    {
        finishMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Lose = 2;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }

    public void LoadSnake()
    {
        SceneManager.LoadScene("SnakeMiniGame");
        Time.timeScale = 1f;
    }

    public void InputButton()
    {
        if (Input.GetKeyDown(KeyCode.M) && Snake == 1 && Lose!= 4)
        {
            coinUI.SetActive(false);
            LoadSnake();
        }
        if (Input.anyKey && Lose == 1)
        {
            Lose = 0;
            LoadSnake();
        }
        if (Input.anyKey && Lose == 2)
        {
            Lose = 3;
            LoadMenu();
        }
    }
}
