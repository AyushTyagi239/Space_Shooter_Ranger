using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; 
    [SerializeField] Transform projectileSpawnPoint; // Dedicated spawn point
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 2f;
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] bool useAI;
    // grab refrence to audioplayer
    AudioPlayer audioPlayer;
     void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    
    public bool isFiring;
    Coroutine firingCoroutine;

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (projectilePrefab == null || projectileSpawnPoint == null)
            {
                Debug.LogError("Projectile prefab or spawn point not assigned!");
                yield break;
            }

            GameObject instance = Instantiate(
                projectilePrefab, 
                projectileSpawnPoint.position, 
                projectileSpawnPoint.rotation // This ensures bullet direction is based on spawn point's orientation
            );

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = projectileSpawnPoint.up * projectileSpeed;
            }
            else
            {
                Debug.LogWarning("Projectile has no Rigidbody2D - won't move");
            }
            audioPlayer.PlayShootingClip();

            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}
