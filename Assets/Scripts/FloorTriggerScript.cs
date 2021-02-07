using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorTriggerScript : MonoBehaviour
{
    [SerializeField]
    private CannonScript CannonRef;

    [SerializeField]
    private GameObject ScoreAddedText = null;

    [SerializeField]
    private Canvas ParentTransform = null;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            GameObject scoreAdded = Instantiate(ScoreAddedText, new Vector3(-10.76f, 9.57f, 0f), Quaternion.identity);
            scoreAdded.GetComponent<TextMesh>().text = "+" + (25 * Utilities.Multiplier).ToString();

            Utilities.Score += 25 * Utilities.Multiplier;
            Utilities.Multiplier++;

            CannonRef.ScoreText.text = "Score: " + Utilities.Score.ToString();

            CannonRef.NumCrates--;
            CannonRef.CrateText.text = "x " + CannonRef.NumCrates.ToString();
        }
    }
}
