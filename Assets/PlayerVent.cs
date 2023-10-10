// THIS DOES NOT WORK RIGHT NOW
// TODO: MAKE THIS WORK WITH EVENTS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVent : MonoBehaviour
{

    bool charCanUseVents = false;
    bool charInVentRange = false;

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
            Events.onVentEnter.Invoke(this.gameObject, lastVentCollision.GetComponent<Vent>().ventIndex);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Collision!");


            if (collision.gameObject.CompareTag("Vent"))
            {

                Debug.Log("It's a vent!");

                charInVentRange = true;

                lastVentCollision = collision.gameObject;
    
            }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        charInVentRange = false;
    }



}
