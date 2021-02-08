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
    private GameObject ParentTransform = null;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            GameObject scoreAdded = Instantiate(ScoreAddedText, new Vector3(-10.76f, 9.57f, 0f), Quaternion.identity, ParentTransform.transform);
            scoreAdded.GetComponent<Text>().text = "+" + (25 * Utilities.Multiplier).ToString();
            scoreAdded.transform.position = new Vector3(95f, 340f, 0f);
            //scoreAdded.GetComponent<Transform>().SetParent(ParentTransform.transform, false);

            Utilities.Score += 25 * Utilities.Multiplier;
            Utilities.Multiplier++;

            CannonRef.ScoreText.text = "Score: " + Utilities.Score.ToString();

            CannonRef.NumCrates--;
            CannonRef.CrateText.text = "x " + CannonRef.NumCrates.ToString();
        }
    }
}
