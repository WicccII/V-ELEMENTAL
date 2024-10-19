using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    AudioManager audioManager;
    // Define state of game 
    public enum GameState
    {
        Ganeplay,
        Pause,
        GameOver,
        LevelUp
    }
    [Header("UI")]
    public GameObject pauseScene;
    public GameObject gameOverScene;
    public GameObject levelUpScene;

    [Header("Stats")]
    public Text currentHealthDisplay;
    public Text currentRecoverDisplay;
    public Text currentSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileDisplay;
    public Text currentMagnetDisplay;

    [Header("Result scene display")]
    public Image choosenChraacterImage;
    public Text choosenCharcterName;
    public Text timeSurvivedDisplay;
    public List<Image> chooseSkill = new List<Image>(6);
    public List<Image> chooseItem = new List<Image>(6);

    [Header("State")]
    public GameState currentState;
    public GameState previousState;

    //check if GameOver
    [HideInInspector]
    public bool isGameOver = false;
    public bool chooseUpgrade = false;

    [Header("StopWatch")]
    public float timeLimit;
    float StopWatchTime;
    public Text timeDisplay;

    public GameObject playerObject;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
                UpdateStopWatch();
                break;
            case GameState.Pause:
                CheckPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    DisplayResult();
                }
                break;
            case GameState.LevelUp:
                if (!chooseUpgrade)
                {
                    chooseUpgrade = true;
                    Time.timeScale = 0f;
                    levelUpScene.SetActive(true);
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
        audioManager.PauseBackgroundMusic();
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
        audioManager.PlayBackgroundMusic();
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
        levelUpScene.SetActive(false);
    }

    public void GameOver()
    {
        audioManager.PlaySFX(audioManager.death);
        audioManager.PauseBackgroundMusic();
        timeSurvivedDisplay.text = timeDisplay.text;
        ChangeState(GameState.GameOver);
    }

    void DisplayResult()
    {
        gameOverScene.SetActive(true);
    }

    public void AssignChoosenCharacter(PlayerScriptableObject playerScriptableObject)
    {
        choosenChraacterImage.sprite = playerScriptableObject.Icon;
        choosenCharcterName.text = playerScriptableObject.CharacterName;
    }

    public void ChooseSkillAssign(List<Sprite> choseSkillData)
    {
        if (choseSkillData.Count > chooseSkill.Count)
        {
            return;
        }
        for (int i = 0; i < chooseSkill.Count; i++)
        {
            if (choseSkillData[i])
            {
                chooseSkill[i].sprite = choseSkillData[i];
                chooseSkill[i].enabled = true;
            }
            else
            {
                chooseSkill[i].enabled = false;
            }
        }
    }

    public void ChooseItemAssign(List<Sprite> choseItemData)
    {
        if (choseItemData.Count > chooseItem.Count)
        {
            return;
        }
        for (int i = 0; i < chooseItem.Count; i++)
        {
            if (choseItemData[i])
            {
                chooseItem[i].sprite = choseItemData[i];
                chooseItem[i].enabled = true;
            }
            else
            {
                chooseItem[i].enabled = false;
            }
        }
    }

    void UpdateStopWatch()
    {
        StopWatchTime += Time.deltaTime; // update the time

        UpdateTimeDisplay();

        if (StopWatchTime >= timeLimit)
        {
            GameOver();
        }
    }

    void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(StopWatchTime / 60f);
        int seconds = Mathf.FloorToInt(StopWatchTime % 60f);
        timeDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        audioManager.PauseBackgroundMusic();
        audioManager.PlaySFX(audioManager.levelUp);
        ChangeState(GameState.LevelUp);
        // Find the UIManager in the scene
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            // Call the method directly
            uiManager.RemoveAndApplyUpgradeOption();
        }
        else
        {
            Debug.LogError("UIManager not found in the scene.");
        }
    }

    public void EndLevelUp()
    {
        chooseUpgrade = false;
        Time.timeScale = 1f;
        levelUpScene.SetActive(false);
        ChangeState(GameState.Ganeplay);
        audioManager.PlayBackgroundMusic();
    }
}
