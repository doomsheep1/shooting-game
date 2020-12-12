using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemy spawner code
public class enemySpawner : MonoBehaviour
{
    EnemyPooler enemyPooler;
    private LevelUI tracker;

    void Start()
    {
        enemyPooler = EnemyPooler.Instance;
        tracker = GameObject.FindGameObjectWithTag("tracking").GetComponent<LevelUI>();
    }

    public void EnemySpawn(int stageNo)
    {
        for (int enemyCount = 1; enemyCount <= stageNo*10; enemyCount++)
        {
            Vector3 position = new Vector3(-10.5f, -5f, 5.5f);
            enemyPooler.spawnFromPool("Cube", position, Quaternion.identity);
            tracker.EnemyUpdate(enemyCount);
        }
    }

}
