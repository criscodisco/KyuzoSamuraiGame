using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMapScreen : MonoBehaviour
{
    #region Start
    private void Start()
    {
        StartCoroutine(DelayLoadBattleLevelCoroutine());
    }

    #endregion

    #region LoadLevelCoroutine
    private IEnumerator DelayLoadBattleLevelCoroutine()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("BattleArenaScene", LoadSceneMode.Single);
    }

    #endregion
}
