using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;
    [SerializeField] float deathDelay = 0.1f;

    CameraShake cameraShake;
    LevelManager levelManager;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int GetHealth() => health;

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(
                hitEffect, 
                transform.position, 
                Quaternion.identity
            );
            
            float totalDuration = instance.main.duration + instance.main.startLifetime.constantMax;
            Destroy(instance.gameObject, totalDuration);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper?.ModifyScore(score);
            Destroy(gameObject, deathDelay);
        }
        else
        {
            levelManager?.LoadGameover();
            Destroy(gameObject, deathDelay);
        }
    }
}