using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Define state of game 
    public enum GameState
    {
        Ganeplay,
        Pause,
        GameOver
    }
    [Header("UI")]
    public GameObject pauseScene;
    public GameObject gameOverScene;

    //stats
    public Text currentHealthDisplay;
    public Text currentRecoverDisplay;
    public Text currentSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileDisplay;
    public Text currentMagnetDisplay;

    //result DisplayResult
    public Image choosenChraacterImage;
    public Text choosenCharcterName;

    // current state stored
    public GameState currentState;
    public GameState previousState;

    //check if GameOver
    public bool isGameOver = false;

    void Awake()
    {
        //warning check if orther singlton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA" + this);
        }

        DisableScene();
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Ganeplay:
                CheckPauseAndResume();
                break;
            case GameState.Pause:
                CheckPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("GameOver");
                    DisplayResult();
                }
                break;
            default:
                Debug.Log("Invalid State");
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

    }

    public void PauseGame()
    {
        if (currentState != GameState.Pause)
        {
            previousState = currentState;
            ChangeState(GameState.Pause);
            Time.timeScale = 0f; // stop the Time
            pauseScene.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Pause)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScene.SetActive(false);
        }
    }

    void CheckPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState != GameState.Pause)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void DisableScene()
    {
        pauseScene.SetActive(false);
        gameOverScene.SetActive(false);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResult()
    {
        gameOverScene.SetActive(true);
    }

    public void AssignChoosenCharacter (PlayerScriptableObject playerScriptableObject)
    {
        choosenChraacterImage.sprite = playerScriptableObject.Icon;
        choosenCharcterName.text = playerScriptableObject.CharacterName;
    }
}
