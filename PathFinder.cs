using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig; 
    List<Transform> waypoints;
    int waypointIndex = 0;
    Vector3 targetPosition;
    void  Awake(){
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("Waypoints list is empty or not assigned in WaveConfig!");
            enabled = false;
            return;
        }
        transform.position = waypoints[0].position;
        UpdateTargetPosition();
    }

    void Update()
    {
        FollowPath();
    }

    void UpdateTargetPosition()
    {
        if (waypointIndex < waypoints.Count)
        {
            targetPosition = waypoints[waypointIndex].position;
            // Maintain current z-position to prevent depth issues
            targetPosition.z = transform.position.z;
        }
    }

    void FollowPath()
    {
        if (waypointIndex >= waypoints.Count) return;

        float moveStep = waveConfig.GetMoveSpeed() * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveStep);

        if (Vector3.Distance(transform.position, targetPosition) <= Mathf.Epsilon)
        {
            waypointIndex++;
            if (waypointIndex < waypoints.Count)
            {
                UpdateTargetPosition();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}