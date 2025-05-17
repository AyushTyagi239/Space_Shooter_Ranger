using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;  // Fixed capitalization for consistency
    ScoreKeeper scorekeeper;
    void Awake()
    {
        scorekeeper = FindObjectOfType<ScoreKeeper>();
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadMainMenu()
    {
        scorekeeper.ResetScore();
        SceneManager.LoadScene(0);
    }
    
    public void LoadGameover()
    {
        Debug.Log("LoadGameover() in LevelManager called!");
        StartCoroutine(WaitAndLoad(2, sceneLoadDelay));  // Using corrected variable name
    }

    public void QuitGame()  // Fixed typo in method name (was QuitGAme)
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    
    IEnumerator WaitAndLoad(int sceneIndex, float delay)  // Better parameter name
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}