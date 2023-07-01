using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject character1;
    private GameObject character2;

    private Character1 character1MoveScript;
    private Character2 character2MoveScript;

    private Transform c1RespawnPoint;
    private Transform c2RespawnPoint;

    public int currentCharacter;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        character1 = GameObject.FindGameObjectWithTag("Character1");
        character2 = GameObject.FindGameObjectWithTag("Character2");

        character1MoveScript = character1.GetComponent<Character1>();
        character2MoveScript = character2.GetComponent<Character2>();

        c1RespawnPoint = character1.transform;
        c2RespawnPoint = character2.transform;

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

    public void RespawnCharacter(GameObject character)
    {
        if (character.CompareTag("Character1"))
        {
            character1.transform.position = c1RespawnPoint.transform.position;
        }
        if (character.CompareTag("Character2"))
        {
            character2.transform.position = c2RespawnPoint.transform.position;
        }
    }
}
