using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseSceneScript : MonoBehaviour
{
    [SerializeField]
    Text score;

    void Start()
    {
        if (score != null)
        {
            score.text = Utilities.Score.ToString();
        }

        // Mainly just for the Scene Editor while playing, so the player starts out with the main menu, not this one:
        // After packaging the project it goes by the order in build settings by default to know which scene to load
        if (Utilities.ScenesChanged == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void LoadMenu()
    {

        Utilities.ScenesChanged++;
        SceneManager.LoadScene("MainMenu");
    }
}
