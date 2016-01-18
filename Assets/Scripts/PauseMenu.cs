using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenuPanel;
    bool isPaused = false;
    LvlMngrController levelManager;
    SceneFadeScript sceneFade;
    PlayerController player;


    void Start()
    {
        pauseMenuPanel = GameObject.Find("PauseMenu");
        sceneFade = GameObject.Find("LevelManager").GetComponent<SceneFadeScript>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LvlMngrController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !sceneFade.sceneStart)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UnpauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;        
        pauseMenuPanel.SetActive(true);
        player.enabled = false;
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;        
        pauseMenuPanel.SetActive(false);
        player.enabled = true;
    }

    public void GoToMainMenu()
    {
        Application.LoadLevel(0);
    }

    public void RestartFromPause()
    {
        UnpauseGame();
        levelManager.restartLevel();
    }
}