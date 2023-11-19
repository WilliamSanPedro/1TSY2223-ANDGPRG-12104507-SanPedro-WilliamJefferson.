using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private bool IsReloading = false;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private GameObject currentTarget;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            enemiesInRange.Add(col.gameObject);
            UpdateTarget();
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            enemiesInRange.Remove(col.gameObject);
            UpdateTarget();
        }
    }
    public void UpdateTarget()
    {
        if (currentTarget != null)
        {
            return;
        }
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy == null) { return; }
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        currentTarget = closestEnemy;
    }
    public void EnemyDestroyed(GameObject enemy)
    {
        if (enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
            UpdateTarget();
        }
    }
    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<GatlingProjectile>().SetDamage(10);
        projectile.GetComponent<Rigidbody>().velocity = firePoint.forward * 12f;
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        IsReloading = true;
        yield return new WaitForSeconds(0.1f);
        IsReloading = false;
    }
}
