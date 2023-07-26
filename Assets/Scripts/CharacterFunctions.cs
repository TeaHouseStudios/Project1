using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFunctions : MonoBehaviour
{
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        character = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Hazard")
        {
           
            KillCharacter();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {

            KillCharacter();
        }
    }

    public void KillCharacter()
    {
        if (character.gameObject.tag == "Character1")
        {
            GameManager.Instance.character1Deaths++;
        }
        else
        {
            GameManager.Instance.character2Deaths++;
        }
        GameManager.Instance.RespawnCharacter(character);
    }
}
