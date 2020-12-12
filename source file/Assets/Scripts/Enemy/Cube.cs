using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Enemy behavior code
public class Cube : MonoBehaviour //IPooledEnemy
{
    public NavMeshAgent agent;
    EnemyPooler enemyPooler;

    //Patroling
    private Vector3 patrolPoint;
    public float patrolPointRange;
    bool patrolPointSet;
    private float enemySpeed = 3f;

    //Access LevelUI
    private LevelUI tracker;

    void Start()
    {
        enemyPooler = EnemyPooler.Instance;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemySpeed;
        tracker = GameObject.FindGameObjectWithTag("tracking").GetComponent<LevelUI>();
    }

    void Update()
    {
        patrol();
    }

    private void patrol()
    {
        if (patrolPointSet)
            agent.SetDestination(patrolPoint);
        else
            searchPatrolPoint();
        Vector3 distance = transform.position - patrolPoint;
        if (distance.magnitude < 1f) 
            patrolPointSet = false;
    }

    private void searchPatrolPoint()
    {
        float randomX = Random.Range(-patrolPointRange, patrolPointRange);
        float randomZ = Random.Range(-patrolPointRange, patrolPointRange);
        patrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        UnityEngine.Debug.DrawRay(patrolPoint, -transform.up * 10f, Color.green);
        if (Physics.Raycast(patrolPoint, -transform.up, 1f))
            patrolPointSet = true;
    }

    private void OnCollisionEnter (Collision coll)
    {
        if (coll.collider.tag == "enemyCube" || coll.collider.tag != "Floor")
        {
            patrolPointSet = false;
        }
    }

    public void Destroy(string tag)
    {
        gameObject.SetActive(false);
        enemyPooler.enemyDictionary[tag].Enqueue(gameObject);
        tracker.ScoreUpdate(1);
        tracker.EnemyUpdate(-1);
    }

    public override string ToString ()
    {
        return "Cube";
    }
}
