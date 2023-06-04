using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBlackScreen : MonoBehaviour
{
    public int isActive;
    public int delay = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= 1;
        } else
        {
            isActive = Random.Range(1, 101); // 1 ~ 100
            if (isActive <= 35) // 1 ~ 35
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else // 36 ~ 100
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            delay = 1800;
        }
    }
}
