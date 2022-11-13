using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMover : MonoBehaviour
{
    [Header("Rotate Speed")]
    [SerializeField] float turnSpeed = 2f;
    [Header("Freeze Booster")]
    [SerializeField] Material enemyNormalMaterial;
    [SerializeField] Material enemyFreezeMaterial;
    
    Transform target;
    SpawnEnemy spawnEnemy;
    NavMeshAgent navMeshAgent;

    int currentPositionIndex, spawnPointIndex;
    public int CurrentPositionIndex() {return currentPositionIndex;}
    bool rotationOn = false, onEnable = false; //debug variables
    bool isFreeze = false;
    public bool IsFreeze() {return isFreeze;}
    void Start()
    {
        spawnEnemy = GetComponentInParent<SpawnEnemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        spawnPointIndex = Random.Range(0, spawnEnemy.ReturnCountPoints());
        currentPositionIndex = spawnPointIndex;
        gameObject.transform.position = spawnEnemy.ReturnSpawnPoint(spawnPointIndex).position;
        onEnable = true;
    }
    
    void OnEnable()
    {
        if(onEnable && gameObject.activeSelf == true) MoveToRandomTarget();
    }

    public void MoveToRandomTarget()
    {
        rotationOn = false;
        while(spawnPointIndex == currentPositionIndex)
        {
            spawnPointIndex = Random.Range(0, spawnEnemy.ReturnCountPoints());
        }
        currentPositionIndex = spawnPointIndex;
        target = spawnEnemy.ReturnSpawnPoint(spawnPointIndex);
        navMeshAgent.SetDestination(target.position);
        rotationOn = true;
    }

    void LateUpdate()
    {
        if(rotationOn) FaceToTarget();
    }
    private void FaceToTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public IEnumerator FreezeMove(float freezingTime)
    {
        isFreeze = true;
        GetComponent<NavMeshAgent>().stoppingDistance = 100f;
        GetComponentInChildren<SkinnedMeshRenderer>().material = enemyFreezeMaterial;
        yield return new WaitForSeconds(freezingTime);
        isFreeze = false;
        GetComponent<NavMeshAgent>().stoppingDistance = 0;
        GetComponentInChildren<SkinnedMeshRenderer>().material = enemyNormalMaterial;
    }
}
