using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    public Transform boxHolder;
    public Transform grabDetect;

    public float rayDistance;

    public bool carryingBox;

    
    // Start is called before the first frame update
    void Start()
    {
        carryingBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDistance);
        GameObject pickup;
        if (hit.collider != null && hit.collider.GetComponent<Box>() != null)
        {
            //pickup = box in this case
            pickup = hit.collider.gameObject;
            if (Input.GetKeyDown(KeyCode.E) && !carryingBox && GameManager.Instance.currentCharacter == 2)
            {
                carryingBox = true;
                pickup.transform.parent = boxHolder;
                pickup.transform.position = boxHolder.position;
                pickup.GetComponent<Rigidbody2D>().isKinematic = true;
                //pickup.GetComponent<BoxCollider2D>().enabled = false;
            }

            else if (Input.GetKeyDown(KeyCode.E) && carryingBox && GameManager.Instance.currentCharacter == 2)
            {
                carryingBox = false;
                pickup.transform.parent = null;
                pickup.GetComponent<Rigidbody2D>().isKinematic = false;
                //pickup.GetComponent<BoxCollider2D>().enabled = true;

            }

        }

        
    }
}
