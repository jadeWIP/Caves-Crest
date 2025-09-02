using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints; // An array to hold the waypoints that the object will patrol between
    private int currentWaypointIndex = 0; // Index to keep track of the current waypoint in the array

    [SerializeField] private float speed = 2f; // Speed at which the object moves between waypoints

    // Update is called once per frame
    private void Update()
    {
        // Check if the object is close to the current waypoint
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            // Move to the next waypoint and reset to the first one if the last waypoint is reached
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        // Move the object towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}