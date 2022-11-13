using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    [Header("List Spawn Locations")]
    [SerializeField] List<Transform> spawnLocation = new List<Transform>();
    [Header("Spawn Time")]
    [SerializeField] float spawnTime = 2f;
    [SerializeField] Image spawnBar;
    [Header("Count Enemies on Field")]
    [SerializeField] Image countEnemyBar;
    [Header("Game Over")]
    [SerializeField] GameObject gameOverCanvas;

    float timer = 0f;
    int countEnemy;

    GameProcess gameProcess;
    //return functions
    public Transform ReturnSpawnPoint(int index) {return spawnLocation[index];}
    public int ReturnCountPoints() {return spawnLocation.Count;}
    
    void Start()
    {
        gameProcess = GetComponent<GameProcess>();
        timer = 0f;
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while(true)
        {
            foreach(Transform enemy in transform)
            {
                if(enemy.gameObject.activeSelf == false)
                {
                    yield return new WaitForSeconds(timer);
                    countEnemy++;
                    if(countEnemy == transform.childCount)
                    {
                        ResetSpawn();
                        PlayerPrefs.SetInt("Current Score", gameProcess.ReturnScore());
                        if(PlayerPrefs.GetInt("Hight Score") < PlayerPrefs.GetInt("Current Score"))
                            PlayerPrefs.SetInt("Hight Score", gameProcess.ReturnScore());
                    }
                    enemy.gameObject.SetActive(true);  
                    timer = spawnTime;
                }
            }
        }
    }

    void Update()
    {
        SpawnTimer();
        countBarUpdate();
    }

    public void DecreaseCountEnemy()
    {
        countEnemy--;
    }

    void countBarUpdate()
    {
        countEnemyBar.fillAmount = (float)countEnemy/transform.childCount;
        if(countEnemyBar.fillAmount < 0.5f)
        {
            countEnemyBar.color = Color.green;
        }
        if(countEnemyBar.fillAmount >= 0.5f && countEnemyBar.fillAmount < 0.8f)
        {
            countEnemyBar.color = Color.yellow;
        }
        if(countEnemyBar.fillAmount >= 0.8f)
        {
            countEnemyBar.color = Color.red;
        }
    }

    void SpawnTimer()
    {
        if(GetComponent<Boosters>().IsStop() == false)
        {
            timer -= Time.deltaTime;
            spawnBar.fillAmount = timer/spawnTime;
        }
    }
    //Next difficult mode
    public void DecreaseSpawnTime(float minusTime)
    {
        spawnTime -= minusTime;
    }
    private void ResetSpawn()
    {
        Time.timeScale = 0;
        gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}
