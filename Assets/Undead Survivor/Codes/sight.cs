using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sight : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        Vector3 l_vector = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(l_vector).normalized;

    }
}
