using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVent : MonoBehaviour
{

    bool charCanUseVents = false;
    public bool charInVentRange = false;

    GameObject lastVentCollision;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.GetComponentInChildren<CharacterMovement>().characterIndex == gameManager.currentCharacter)
        {
            charCanUseVents = true;
        }
        else
        {
            charCanUseVents = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && charCanUseVents && charInVentRange && (lastVentCollision != null))
        {
            Events.onVentEnter.Invoke(this.gameObject, lastVentCollision.GetComponent<Vent>().exitVentIndex);
        }

    }

    
    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Vent"))
        {
            charInVentRange = false;

            Debug.Log("Just exited " + collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vent"))
        {
            charInVentRange = true;

            lastVentCollision = collision.gameObject;
        }
    }




}
