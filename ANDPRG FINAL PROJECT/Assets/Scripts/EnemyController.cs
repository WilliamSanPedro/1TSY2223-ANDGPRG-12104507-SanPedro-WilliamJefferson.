using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    private List<Transform> wayPoints;
    private int currentWayPointIndex = 0;
    private float agentStoppingDistance = 1f;
    private bool wayPointsSet = false;
    public LevelManager levelManager; 
    NavMeshAgent agent;
    public Slider healthBarPrefab;
    Slider healthBar;
    public int maxHealth = 100;
    public LayerMask towerLayer;
    void Start()
    {     
        agent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>();

        healthBar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar)
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 0.8f);
        }
        if(!wayPointsSet)
        {
            return;
        }
        if(!agent.pathPending && agent.remainingDistance <= agentStoppingDistance )
        {
            if (currentWayPointIndex == wayPoints.Count) 
            {
                levelManager.EnemyDestroyed();
                Destroy( this.gameObject,0.1f);
            }
            else
            {
                currentWayPointIndex++;
                agent.SetDestination(wayPoints[currentWayPointIndex].position);
            }
          
        }
       
    }
    public void SetDestination(List<Transform> wayPoints)
    {
        this.wayPoints = wayPoints;
        wayPointsSet = true;
    }
    public void Hit(int damage)
    {
        if(healthBar)
        {
            healthBar.value -= damage;
            if(healthBar.value <=0)
            {
                float range = 15f;

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, towerLayer);
                foreach (var hitCollider in hitColliders)
                {
                    Tower tower = hitCollider.GetComponent<Tower>();
                    if (tower !=null)
                    {
                        tower.EnemyDestroyed(gameObject);
                    }
                }

                Destroy(healthBar.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
