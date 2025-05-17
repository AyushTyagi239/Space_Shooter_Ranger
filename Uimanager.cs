using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Need this for TextMeshProUGUI

public class UIManager : MonoBehaviour // Fixed class name capitalization
{
    [SerializeField] TextMeshProUGUI scoreText; 
    ScoreKeeper scoreKeeper; // Fixed variable name capitalization
    
    void Awake() 
    { 
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // Fixed class name capitalization
    } 
    
    void Start() 
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore(); 
    }
}