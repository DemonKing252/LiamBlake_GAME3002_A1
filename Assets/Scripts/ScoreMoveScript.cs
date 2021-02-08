using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f, 60f * Time.deltaTime, 0f);
        GetComponent<Text>().color -= new Color(0f, 0f, 0f, 1f * Time.deltaTime);
    }
}
