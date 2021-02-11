using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private void Start()
    {
        Utilities.CurrentLevel = 1;
        Utilities.Score = 0;
        Utilities.Multiplier = 1;
    }

    public void LoadLevel1()
    {
        Utilities.ScenesChanged++;
        SceneManager.LoadScene("Level1");
    }

    public void LoadHowToPlay()
    {
        Utilities.ScenesChanged++;
        SceneManager.LoadScene("HowToPlay");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
