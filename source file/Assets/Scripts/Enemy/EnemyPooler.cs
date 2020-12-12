using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enemy pool manager
public class EnemyPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static EnemyPooler Instance;

    private void Awake ()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> enemyDictionary;

    // Start is called before the first frame update
    void Start()
    {
        enemyDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> enemyPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject enemy = Instantiate(pool.prefab);
                enemy.SetActive(false);
                enemyPool.Enqueue(enemy);
            }
            enemyDictionary.Add(pool.tag, enemyPool);
        }
    }

    public GameObject spawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!enemyDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " doesn't exist");
            return null;
        }
        GameObject enemyToSpawn = enemyDictionary[tag].Dequeue();
        enemyToSpawn.SetActive(true);
        enemyToSpawn.transform.position = position;
        enemyToSpawn.transform.rotation = rotation;
        /*IPooledEnemy pooledEnemy = enemyToSpawn.GetComponent<IPooledEnemy>();
        if (pooledEnemy != null)
        {
            pooledEnemy.onEnemySpawn();
        }*/
        return enemyToSpawn;
    }
}
