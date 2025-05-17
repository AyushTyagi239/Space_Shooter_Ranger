using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // List of wave configurations (each defines enemy types, count, spawn timing, etc.)
    [SerializeField] List<WaveConfigSO> WaveConfigs;
    [SerializeField] Transform firingPoint;

    // Delay between completing one wave and starting the next
    [SerializeField] float TimebetweenWaves = 1f;
    
    // Should the waves loop infinitely? (Toggle in Inspector)
    [SerializeField] bool islooping;
    
    // Reference to the currently active wave configuration
    WaveConfigSO currentwave;

    // Public method to let other scripts access the current wave config
    public WaveConfigSO GetCurrentWave()
    {
        return currentwave;
    }

    void Start()
    {
        // Start the enemy spawning coroutine when the game begins
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine that handles the wave spawning logic
    IEnumerator SpawnEnemies()
    {
        do // Keep spawning waves as long as islooping is true
        {
            // Process each wave configuration in order
            foreach (WaveConfigSO wave in WaveConfigs)
            {
                currentwave = wave; // Set the current wave
                
                // Spawn all enemies for this wave
                for (int i = 0; i < currentwave.EnemyCount(); i++)
                {
                    // Create enemy instance:
                    // 1. Use the first enemy prefab from the wave (index 0)
                    // 2. Position it at the wave's starting waypoint
                    // 3. No rotation (Quaternion.identity)
                    // 4. Make this spawner the parent in the hierarchy
                    Instantiate(
                        currentwave.GetEnemyPrefabs(i),
                        currentwave.GetStartingWaypoint().position,
                        Quaternion.Euler(0,0,180),
                        transform
                    );
                    
                    // Wait for random time before spawning next enemy
                    yield return new WaitForSeconds(currentwave.GetRandomSpawnTime());
                }
                
                // Wait between waves (after completing all enemies in current wave)
                yield return new WaitForSeconds(TimebetweenWaves);
            }
        } while (islooping); // Loop forever if islooping is true
    }
}