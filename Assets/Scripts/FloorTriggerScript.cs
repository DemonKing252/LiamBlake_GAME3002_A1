using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTriggerScript : MonoBehaviour
{
    [SerializeField]
    private CannonScript CannonRef;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crate")
        {
            Utilities.Score += 25 * Utilities.Multiplier;
            Utilities.Multiplier++;

            CannonRef.ScoreText.text = "Score: " + Utilities.Score.ToString();

            CannonRef.NumCrates--;
            CannonRef.CrateText.text = "x " + CannonRef.NumCrates.ToString();
        }
    }
}
