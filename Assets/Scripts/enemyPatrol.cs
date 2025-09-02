using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    // Public variables for defining patrol points and movement speed
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        // Get Rigidbody2D and Animator components and set initial values
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform; // Start at pointB
        anim.SetBool("isRunning", true); // Set the "isRunning" parameter in the animator to true
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position; // Calculate the vector from the enemy to the current patrol point

        // Move the enemy towards the current point based on the assigned speed
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0); // Enemy is going right.
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0); // Enemey is going left.
        }

        // Check if the enemy has reached the current point and update accordingly
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip(); // Flip the enemy's direction
            currentPoint = pointA.transform; // Switch to pointA
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip(); // Flip the enemy's direction
            currentPoint = pointB.transform; // Switch to pointB
        }
    }

    // Flip the enemy's direction
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Draw Gizmos for visualizing patrol points and path in Unity
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f); // Draw a wire sphere at pointA
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f); // Draw a wire sphere at pointB
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position); // Draw a line between pointA and pointB
    }
}
