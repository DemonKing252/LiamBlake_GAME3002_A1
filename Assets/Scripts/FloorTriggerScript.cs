using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FloorTriggerScript : MonoBehaviour
{
    [SerializeField]
    private Canvas overlay;

    [SerializeField]
    private CannonScript CannonRef;

    [SerializeField]
    private GameObject ScoreAddedText = null;

    [SerializeField]
    private GameObject ParentTransform = null;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            // Spawn a text as a child of the canvas (we don't want this to render in world space):
            GameObject scoreAdded = Instantiate(ScoreAddedText, new Vector3(-10.76f, 9.57f, 0f), Quaternion.identity, ParentTransform.transform);
            scoreAdded.GetComponent<Text>().text = "+" + (25 * Utilities.Multiplier).ToString();
            scoreAdded.transform.position = new Vector3(95f, 340f, 0f);

            // add score
            Utilities.Score += 25 * Utilities.Multiplier;

            // if the player doesn't fire, then add the multiplier by 1, if they do fire this will be set back to 0.
            // ie: better accuracy = more points!
            Utilities.Multiplier++;

            CannonRef.ScoreText.text = "Score: " + Utilities.Score.ToString();

            CannonRef.NumCrates--;
            if (CannonRef.NumCrates <= 0)
            {
                if (Utilities.CurrentLevel == 3)
                {
                    SceneManager.LoadScene("WinScene");
                }
                else
                {
                    // pause
                    Time.timeScale = 0f;
                    overlay.gameObject.SetActive(true);

                }
            }
            
            CannonRef.CrateText.text = "x " + CannonRef.NumCrates.ToString();
        }
    }
}
