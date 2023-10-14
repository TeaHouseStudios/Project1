using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewDistance;
    public float fovAngle;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        // Find player
        GameObject character1 = GameObject.FindGameObjectWithTag("Character1");

        // Get vector to player
        Vector2 toCharacter1 = character1.transform.position - transform.position;

        // Check if player is within FOV angle
        if (Vector2.Angle(transform.up, toCharacter1) < fovAngle * 0.5f)
        {
            // Check if player is within view distance
            if (toCharacter1.magnitude < viewDistance)
            {
                // Check for line of sight using raycasting
                RaycastHit2D hit = Physics2D.Raycast(transform.position, toCharacter1.normalized, viewDistance, playerLayer | obstacleLayer);

                // Check if player is the first thing hit
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Character1Body"))
                {
                    // The player is seen
                    Debug.Log("Character 1 Detected!");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the FOV as a debug gizmo
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, fovAngle * 0.5f) * transform.up * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -fovAngle * 0.5f) * transform.up * viewDistance);
    }


}
