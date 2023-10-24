using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject);

        if (collision.gameObject.CompareTag("Character1") || collision.gameObject.CompareTag("Character2"))
        {
            // kill character
            collision.gameObject.GetComponent<CharacterMovement>().KillCharacter();

            Debug.Log(collision.gameObject);
        }

        Destroy(gameObject);
    }
}
