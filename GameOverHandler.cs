using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverHandler : MonoBehaviour
{
    #region Variables
    [Header ("Game Over GUI")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;
    [SerializeField] private GameObject returnButtonGO;
    [SerializeField] private GameObject restartGameButtonGO;
    [SerializeField] private GameObject textMeshProUGUI;
    [SerializeField] private GameObject gameOverMainMenuCanvasGO;
    [SerializeField] private GameObject gameoverSettingsMenuCanvasGO;

    [Header ("Player")]
    [SerializeField] private PlayerController playerController;

    [Header ("Player Attack Collision")]
    [SerializeField] private PlayerAttackCollision playerAttackCollision;

    [Header ("Player Health")]
    private PlayerHealth playerHealth;

    [Header ("Enemy Spawn")]
    private SpawnEnemy spawnEnemy;

    #endregion

    #region Start
    private void Start()
    {
        playerHealth = playerController.gameObject.GetComponent<PlayerHealth>();
        gameOverMainMenuCanvasGO.SetActive(false);
        gameoverSettingsMenuCanvasGO.SetActive(false);
        spawnEnemy = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<SpawnEnemy>();
    }

    #endregion

    #region Update
    private void Update()
    {
        if (InputManager.instance.GameOverMenuOpenCloseInput)
        {
            if (playerHealth.isPlayerDead == true)
            {
                StartCoroutine(SetPlayerControlsDisabledCoroutine());
            }

            if (spawnEnemy.isSpawningDone && spawnEnemy.youWinTheGame)
            {
                playerController.enabled = false;
                playerAttackCollision.enabled = false;
            }
        }
    }

    #endregion

    #region Disable Player Control Coroutine
    private IEnumerator SetPlayerControlsDisabledCoroutine()
    {
        yield return new WaitForSeconds(4f);
        playerController.enabled = false;
        playerAttackCollision.enabled = false;
        DisplayGameOverText();
    }

    #endregion

    #region Show Game Over Text
    public void DisplayGameOverText()
    {
        textMeshProUGUI.gameObject.SetActive(true);
        StartCoroutine(DelayTimeFreezing());     
    }

    #endregion

    #region Stop Time Before Opening Menu
    private IEnumerator DelayTimeFreezing()
    {
        yield return new WaitForSeconds(5.3f);
        Time.timeScale = 0f;
        OpenMainMenu();
    }

    #endregion

    #region Restart Level
    public void RestartLevel()
    {
        playerController.enabled = true;
        playerAttackCollision.enabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("BattleArenaScene", LoadSceneMode.Single);
    }

    #endregion

    #region Open/Close Menus
    public void OpenMainMenu()
    {
        gameOverMainMenuCanvasGO.SetActive(true);
        gameoverSettingsMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
      
        if (gameoverSettingsMenuCanvasGO.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(returnButtonGO);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(restartGameButtonGO);
        } 
    }

    private void CloseAllMenus()
    {
        gameOverMainMenuCanvasGO.SetActive(false);
        gameoverSettingsMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }

    private void OpenSettingsMenuHandle()
    {
        gameOverMainMenuCanvasGO.SetActive(false);
        gameoverSettingsMenuCanvasGO.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    #endregion

    #region Settings Buttons
    public void SettingsButtonPressed()
    {
        OpenSettingsMenuHandle();
    }

    public void SettingsBackButtonPressed()
    {
        OpenMainMenu();
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    #endregion
}
