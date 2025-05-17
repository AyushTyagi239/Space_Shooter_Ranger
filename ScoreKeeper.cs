using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score; 
    static ScoreKeeper instance;

    void Awake() 
    { 
        ManageSingleton(); 
    } 

    void ManageSingleton() 
    { 
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    } 
    
    public int GetScore()
    {
        return score;
    }
    
    public void ModifyScore(int value)
    {
        Debug.Log(score);
        score += value;
        score = Mathf.Clamp(score, 0, int.MaxValue);
    }
    
   
    public void ResetScore()
    {
        score = 0;
    }
}