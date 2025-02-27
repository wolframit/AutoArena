﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyBehaviour> SpawnedEnemies;
    public Action AllEnemiesDefeated;

    [Header("DEBUG")]
    public bool DEBUG_SpawnEnemies;
    public int DEBUG_SpawnCount;
    public GameObject DEBUG_SpawnObj_Marine;
    public GameObject DEBUG_SpawnObj_Robot;

    private void Awake()
    {
        SpawnedEnemies = new List<EnemyBehaviour>();
    }

    private void Update()
    {
        if (DEBUG_SpawnEnemies || Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnEnemies(DEBUG_SpawnCount, DEBUG_SpawnObj_Marine);
            DEBUG_SpawnEnemies = false;
        }

        if (DEBUG_SpawnEnemies || Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnEnemies(DEBUG_SpawnCount, DEBUG_SpawnObj_Robot);
            DEBUG_SpawnEnemies = false;
        }
    }

    public void SpawnEnemies(int count, GameObject enemyObj)
    {
        for (int i = 0; i < count; i++)
        {
            EnemyBehaviour tempEnemy = Instantiate(enemyObj).GetComponent<EnemyBehaviour>();

            SpawnedEnemies.Add(tempEnemy);

            Vector3 pos = RandomNavSphere(Vector3.zero, 20);
            //pos.y = 0.6f;
            tempEnemy.transform.position = pos;
        }
        
    }

    public void UnregisterEnemy(EnemyBehaviour enemy)
    {
        SpawnedEnemies.Remove(enemy);

        if(SpawnedEnemies.Count == 0)
        {
            AllEnemiesDefeated?.Invoke();
        }
    }



    public static Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance,-1);

        return navHit.position;
    }


}
