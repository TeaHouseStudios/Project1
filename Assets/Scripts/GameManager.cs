using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject character1;
    private GameObject character2;

    private Character1 character1MoveScript;
    private Character2 character2MoveScript;

    public int currentCharacter;

    // Start is called before the first frame update
    void Start()
    {
        character1 = GameObject.FindGameObjectWithTag("Character1");
        character2 = GameObject.FindGameObjectWithTag("Character2");

        character1MoveScript = character1.GetComponent<Character1>();
        character2MoveScript = character2.GetComponent<Character2>();

        character2MoveScript.enabled = false;
        currentCharacter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Switch");
            if (currentCharacter == 1)
            {
                character1MoveScript.enabled = false;
                character2MoveScript.enabled = true;
                currentCharacter = 2;
            }
            else if (currentCharacter == 2)
            {
                character2MoveScript.enabled = false;
                character1MoveScript.enabled = true;
                currentCharacter = 1;
            }

        }
    }
}
