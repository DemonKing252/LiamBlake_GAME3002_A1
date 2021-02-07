using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float t = 0f;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t >= 5f)
            Destroy(gameObject);

        transform.position += new Vector3(0f, 1.5f * Time.deltaTime, 0f);
        GetComponent<TextMesh>().color -= new Color(0f, 0f, 0f, 1f * Time.deltaTime);
    }
}
