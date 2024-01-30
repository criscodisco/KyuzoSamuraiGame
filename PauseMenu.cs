using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    #region Variables

    [Header ("Pause Menu UI")]
    [SerializeField] private GameObject mainMenuCanvasGO;
    [SerializeField] private GameObject settingsMenuCanvasGO;

    [Header ("Player Controller")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttackCollision playerAttackCollision;

    [Header ("Pause Menu Order")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;

    [Header ("Pause Menu Status")]
    private bool isPaused;

    #endregion

    #region Start
    private void Start()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsMenuCanvasGO.SetActive(false);
    }

    #endregion

    #region Update
    private void Update()
    {
        if (InputManager.instance.MenuOpenCloseInput)
        {
            if (!isPaused)
            {
                Pause();            
            }
            else
            {
                Unpause();              
            }
        }
    }

    #endregion

    #region Pause Status
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        playerController.enabled = false;
        playerAttackCollision.enabled = false;
        OpenMainMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        playerController.enabled = true;
        playerAttackCollision.enabled = true;
        CloseAllMenus();
    }

    #endregion

    #region Open/Close Menu
    private void OpenMainMenu()
    {
        mainMenuCanvasGO.SetActive(true);
        settingsMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }
    private void OpenSettingsMenuHandle()
    {
        settingsMenuCanvasGO.SetActive(true);
        mainMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void CloseAllMenus()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }

    #endregion

    #region Pause Menu Buttons
    public void PlayGameButtonPressed()
    {
        SceneManager.LoadScene("BattleArenaScene", LoadSceneMode.Single);
    }

    public void SettingsButtonPressed()
    {
        OpenSettingsMenuHandle();
    }

    public void SettingsBackButtonPressed()
    {
        OpenMainMenu();
    }

    public void OnResumePressed()
    {
        Unpause();
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    #endregion
}
