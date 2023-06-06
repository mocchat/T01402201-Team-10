using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;
    

    // Update is called once per frame
    [SerializeField]
    private void Update()
    {

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            currentWaypointIndex++;
            
            if ( currentWaypointIndex >= waypoints.Length)
            {
               
                currentWaypointIndex = 0;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[currentWaypointIndex].transform.position,
            Time.deltaTime * speed);

     }
}
