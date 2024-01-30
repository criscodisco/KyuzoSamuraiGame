using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Variables

    [Header ("Menu UI")]
    [SerializeField] private GameObject mainMenuCanvasGO;
    [SerializeField] private GameObject settingsMenuCanvasGO;
    [SerializeField] private Button returnButton;

    [Header ("Menu Display Order")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;

    #endregion

    #region Start
    private void Start()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsMenuCanvasGO.SetActive(false);
    }

    #endregion

    #region Opening Menu
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

    #endregion

    #region Menu Buttons
    public void PlayGameButtonPressed()
    {
        SceneManager.LoadScene("BattleArenaScene", LoadSceneMode.Single);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
    public void SettingsButtonPressed()
    {
        OpenSettingsMenuHandle();
    }

    public void SettingsBackButtonPressed()
    {
        OpenMainMenu();
    }

    #endregion
}
