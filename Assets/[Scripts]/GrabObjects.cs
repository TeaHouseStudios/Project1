using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    public Transform boxHolder;
    public Transform grabDetect;

    public float rayDistance;

    public bool carryingBox;
    GameObject pickup = null; // Initialize to null


    // Start is called before the first frame update
    void Start()
    {
        carryingBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDistance);
        

        if (!carryingBox)
        {
            if (hit.collider != null && hit.collider.GetComponent<Box>() != null)
            {
                pickup = hit.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.E) && !carryingBox && GameManager.Instance.currentCharacter == 2)
                {
                    carryingBox = true;
                    pickup.transform.parent = boxHolder;
                    pickup.transform.position = boxHolder.position;
                    //pickup.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    pickup.GetComponent<Rigidbody2D>().isKinematic = true;
                    pickup.GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
        }
        else if (carryingBox)
        {
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.currentCharacter == 2)
            {
                Debug.Log(pickup);
                
                if (pickup != null && pickup.GetComponent<Box>().canBeDropped) // Check for null before accessing pickup
                {
                    carryingBox = false;
                    Debug.Log("DROP");
                    pickup.transform.parent = null;
                    pickup.GetComponent<Rigidbody2D>().isKinematic = false;
                    pickup.GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }
        }

    }
}
