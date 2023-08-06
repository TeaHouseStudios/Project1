using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1_Interact : MonoBehaviour
{
    public Transform interactDetect;
    public float rayDistance = 2;

    private void Update()
    {
        if (GameManager.Instance.currentCharacter == 1)
        {
            InteractRaycast();
        }
    }

    public void InteractRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(interactDetect.position, Vector2.right * transform.localScale, rayDistance);
        GameObject pc;
        if (hit.collider != null && hit.collider.GetComponent<PC>() != null)
        {
            pc = hit.collider.gameObject;
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.currentCharacter == 1)
            {
                pc.GetComponent<PC>().Activate();
            }

        }
    }
}
