using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelOverlayScript : MonoBehaviour
{
    public void NextLevel()
    {
        Time.timeScale = 1f;
        print("hello");
        Utilities.CurrentLevel++;
        SceneManager.LoadScene("Level" + Utilities.CurrentLevel.ToString());
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
}
