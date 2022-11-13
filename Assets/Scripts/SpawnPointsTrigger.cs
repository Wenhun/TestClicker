using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPointsTrigger : MonoBehaviour
{
    [SerializeField] int index = 0;

    void OnTriggerEnter(Collider other)
    {
        if(index == other.GetComponent<EnemyMover>().CurrentPositionIndex())
        {
            other.GetComponent<EnemyMover>().MoveToRandomTarget();
        }
    }
}
