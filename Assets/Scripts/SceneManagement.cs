using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene(1);
    }

    public void GameScene()
    {
        SceneManager.LoadScene(2);
    }
}