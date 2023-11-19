using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnRate = 0.5f;
    public List<Transform> wayPoints;
    public int maxCount = 10;
    private int count = 0;
    void Start()
    {
        InvokeRepeating("Spawn",1,spawnRate);
    }

   void Spawn()
   {
        GameObject enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);

        count++;

        if (count >= maxCount)
        {
            CancelInvoke();
        }
   }
    public void StartNextWave()
    {
        count = 0;
        InvokeRepeating("Spawn", 1, spawnRate);
    }
    public void StopSpawning()
    {
        CancelInvoke();
    }
}
