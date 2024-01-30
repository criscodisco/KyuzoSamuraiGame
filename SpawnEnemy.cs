using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    #region Variables

    [Header ("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header ("Enemy Animation")]
    public Animator enemyAnimator;

    [Header ("Enemy Spawning")]
    [SerializeField] private Transform[] spawnPoints;
    public int numberOfEnemiesSpawned;

    [Header ("Enemies Done Spawning Status")]
    public bool isSpawningDone = false;

    [Header ("You Win/You Lose UI")]
    [SerializeField] private GameObject youWinCanvasGO;
    [SerializeField] private GameObject gameOverHandlerGO;

    [Header ("Game Over Handler")]
    private GameOverHandler gameOverHandler;

    [Header ("Player")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttackCollision playerAttackCollision;
    public bool youWinTheGame = false;

    #endregion

    #region Start
    private void Start()
    {
        gameOverHandler = gameOverHandlerGO.GetComponent<GameOverHandler>();
        numberOfEnemiesSpawned = 0;   
        InvokeRepeating("Spawn", 5, 5);
    }

    #endregion

    #region Update
    private void Update()
    {
        if (isSpawningDone && youWinTheGame)
        {
                StartCoroutine(DisplayYouWinCanvasCoroutine());
        }
    }

    #endregion

    #region Spawn Enemies
    private void Spawn()
    {
        int numberOfCurrentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (numberOfCurrentEnemies < 10)
        {
            int randomPrefabIndex = Random.Range(0, 7);
            int randomSpawnPointIndex = Random.Range(0, 2);
            GameObject spawnedEnemy = Instantiate(enemyPrefabs[randomPrefabIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            enemyAnimator = spawnedEnemy.GetComponent<Animator>();
            numberOfEnemiesSpawned++;

            if (numberOfEnemiesSpawned == 100)
            {
                CancelInvoke();
                isSpawningDone = true;
            }
        }
    }

    #endregion

    #region Player Wins UI Coroutine
    private IEnumerator DisplayYouWinCanvasCoroutine()
    {
        youWinCanvasGO.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);       
        gameOverHandler.OpenMainMenu();
        playerController.enabled = false;
        playerAttackCollision.enabled = false;
        youWinCanvasGO.SetActive(false);
        Time.timeScale = 0f;
        youWinTheGame = false;
    }

    #endregion
}
