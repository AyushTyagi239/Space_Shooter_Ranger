using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "WaveConfig", fileName = "New WaveConfig")]
public class WaveConfigSO : ScriptableObject 
{
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] float TimeBetweenEnemySpawns = 1f;
    [SerializeField] float SpawnTimeVarience = 0.5f;
    [SerializeField] float MinimumSpawnTime = 0.2f;


    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public int  EnemyCount()
    {
        return  enemyPrefabs.Count;
    }
    public GameObject GetEnemyPrefabs(int index){
        return enemyPrefabs[index];
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }
    // now we will make a getter to return SpawnTime
    public float GetRandomSpawnTime(){
        float SpawnTime = Random.Range(TimeBetweenEnemySpawns-SpawnTimeVarience , TimeBetweenEnemySpawns + SpawnTimeVarience);
        return Mathf.Clamp( SpawnTime, MinimumSpawnTime, float.MaxValue);
    }
}
