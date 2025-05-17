using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float ShakeDuration = 1f;
    [SerializeField] float ShakeMagnitude = 0.25f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; // Lowercase 'transform'
    }

    public void Play()
    {
        StartCoroutine(Shake()); 
    }

    IEnumerator Shake() 
    {
        float elapsedTime = 0;
        while (elapsedTime < ShakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * ShakeMagnitude; 
            elapsedTime += Time.deltaTime; 
            yield return new WaitForEndOfFrame(); 
        }
        transform.position = initialPosition;
    }
}