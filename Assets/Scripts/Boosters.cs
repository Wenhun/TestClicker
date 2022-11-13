using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boosters : MonoBehaviour
{
    [Header("Stopping Spawn Booster")]
    [SerializeField] float stopSpawnTime = 3f;
    [SerializeField] Button stopSpawnButton;
    [SerializeField] GameObject stopSpawnText;
    [SerializeField] Image spawnBar;
    [Header("Kill Enemies Booster")]
    [SerializeField] Button killEnemiesButton;
    [Header("Freeze Enemies Booster")]
    [SerializeField] Button freezeEnemiesButton;
    [SerializeField] Image freezeBar;
    [SerializeField] GameObject freezeCanvas;
    [SerializeField] float freezeTime = 3f;
    [Header("God Mode Booster")]
    [SerializeField] Button godModeButton;
    [SerializeField] Image godBar;
    [SerializeField] GameObject godCanvas;
    [SerializeField] float godModeTime = 2f;
    bool stopSpawn = false; public bool IsStop() {return stopSpawn;}
    bool isFreeze = false, isGod = false;
    float timerFreeze = 0f, timerGod = 0f, timerStopSpawn;

    SpawnEnemy spawnEnemy;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy = GetComponent<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        FreezeBarChange();
        GodModeBarChange();
        if(stopSpawn)
        {
            timerStopSpawn -= Time.deltaTime;
            stopSpawnText.GetComponent<TMP_Text>().text = (Mathf.Round(timerStopSpawn * 10) / 10).ToString();
        }
    }

    private void FreezeBarChange()
    {
        if (isFreeze)
        {
            timerFreeze -= Time.deltaTime;
            freezeBar.fillAmount = timerFreeze / freezeTime;
            if (timerFreeze <= 0f)
            {
                freezeCanvas.SetActive(false);
                isFreeze = false;
            }
        }
    }

    private void GodModeBarChange()
    {
        if (isGod)
        {
            timerGod -= Time.deltaTime;
            godBar.fillAmount = timerGod / godModeTime;
            if (timerGod <= 0f)
            {
                godCanvas.SetActive(false);
                isGod = false;
            }
        }
    }

    IEnumerator StopSpawn()
    {
        stopSpawn = true;
        spawnBar.color = Color.red;
        stopSpawnText.SetActive(true);
        timerStopSpawn = stopSpawnTime;
        yield return new WaitForSeconds(stopSpawnTime);
        spawnEnemy.StartCoroutine(spawnEnemy.Spawn());
        stopSpawnText.SetActive(false);
        stopSpawn = false;
        spawnBar.color = Color.green;
    }    

    public void StopSpawn_Button()
    {
        spawnEnemy.StopAllCoroutines();
        StartCoroutine(StopSpawn());
        StartCoroutine(stopSpawnButton.GetComponent<ResetButton>().Reset());
    }

    public void KillEnemies()
    {
        StartCoroutine(killEnemiesButton.GetComponent<ResetButton>().Reset());
        foreach (Transform enemy in transform)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public void FreezeEnemies()
    {
        isFreeze = true;
        timerFreeze = freezeTime;
        freezeCanvas.SetActive(true);
        StartCoroutine(freezeEnemiesButton.GetComponent<ResetButton>().Reset());
        foreach(Transform enemy in transform)
        {
            StartCoroutine(enemy.gameObject.GetComponent<EnemyMover>().FreezeMove(freezeTime));
        }
    }

    public void GodMode()
    {
        isGod = true;
        timerGod = godModeTime;
        godCanvas.SetActive(true);
        StartCoroutine(godModeButton.GetComponent<ResetButton>().Reset());
        foreach(Transform enemy in transform)
        {
            StartCoroutine(enemy.gameObject.GetComponent<DamageEnemy>().GodMode(godModeTime));
        }
    }


}
