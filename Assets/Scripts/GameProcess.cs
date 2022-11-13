using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class GameProcess : MonoBehaviour
{
    [Header("ScoreBoard")]
    [SerializeField] TMP_Text scoreBoard;
    [SerializeField] TMP_Text difficultMode;
    [Header("Boosters")]
    [SerializeField] int healthEnemyUp = 2;
    [SerializeField] float speedEnemyUp = 1f;
    [SerializeField] float spawnTimeDown = 1f;

    int currentDifficult;
    int score = 0;

    //return functions
    public int ReturnScore() {return score;}
    
    void Start()
    {
        scoreBoard.text = score.ToString();
        difficultMode.text = "Zero";
        difficultMode.color = Color.white;
        currentDifficult = 0;
    }

    void Update()
    {
        //easy mode
        if(score >= 5 && currentDifficult == 0)
        {
            foreach(Transform enemy in transform)
            {
                enemy.gameObject.GetComponent<NavMeshAgent>().speed += speedEnemyUp;
            }
            difficultMode.text = "Easy";
            difficultMode.color = Color.green;
            currentDifficult++;
        }
        //normal mode
        if(score >= 10 && currentDifficult == 1)
        {
            foreach(Transform enemy in transform)
            {
                enemy.gameObject.GetComponent<DamageEnemy>().IncreaseHealth(healthEnemyUp);
            }
            difficultMode.text = "Normal";
            difficultMode.color = Color.yellow;
            currentDifficult++;
        }
        //hard mode
        if(score >= 15 && currentDifficult == 2)
        {
            GetComponent<SpawnEnemy>().DecreaseSpawnTime(spawnTimeDown);
            difficultMode.text = "Hard";
            difficultMode.color = Color.red;
            currentDifficult++;
        }
        //extreme mode
        if(score >= 20 && currentDifficult == 3)
        {
            foreach(Transform enemy in transform)
            {
                enemy.gameObject.GetComponent<NavMeshAgent>().speed += speedEnemyUp;
                enemy.gameObject.GetComponent<DamageEnemy>().IncreaseHealth(healthEnemyUp);
            }
            GetComponent<SpawnEnemy>().DecreaseSpawnTime(spawnTimeDown);
            difficultMode.text = "EXTREME";
            difficultMode.color = Color.black;
            currentDifficult++;
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreBoard.text = score.ToString();
    }
}
