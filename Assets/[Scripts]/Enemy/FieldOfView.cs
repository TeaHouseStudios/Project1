using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewDistance;
    public float fovAngle;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    GameObject character1;
    GameObject character2;

    private void Start()
    {
        character1 = GameObject.FindGameObjectWithTag("Character1");
        character2 = GameObject.FindGameObjectWithTag("Character2");
    }
    private void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        Vector2 toCharacter1 = character1.transform.position - transform.position;
        Vector2 toCharacter2 = character2.transform.position - transform.position;

        // Check if player is within FOV angle
        if (Vector2.Angle(transform.up, toCharacter1) < fovAngle * 0.5f)
        {
            if (toCharacter1.magnitude < viewDistance)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, toCharacter1.normalized, viewDistance, playerLayer | obstacleLayer);
                // Check if player is the first thing hit
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Character1Body"))
                {
                    Debug.Log("Character 1 Detected!");
                }
            }
        }
        if (Vector2.Angle(transform.up, toCharacter2) < fovAngle * 0.5f)
        {
            if (toCharacter2.magnitude < viewDistance)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, toCharacter2.normalized, viewDistance, playerLayer | obstacleLayer);
                // Check if player is the first thing hit
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Character2Body"))
                {
                    Debug.Log("Character 2 Detected!");
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
