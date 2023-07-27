using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject character1;
    private GameObject character2;

    private Character1 character1MoveScript;
    private Character2 character2MoveScript;

    public Vector3 c1RespawnPoint;
    public Vector3 c2RespawnPoint;

    public int currentCharacter;

    public int character1Deaths;
    public int character2Deaths;

    public TextMeshProUGUI character1DeathsText;
    public TextMeshProUGUI character2DeathsText;

    public int numCharactersBeatenLevel = 0;
    private bool hasRun = false;

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

        character1Deaths = 0;
        character2Deaths = 0;

        character1MoveScript = character1.GetComponent<Character1>();
        character2MoveScript = character2.GetComponent<Character2>();

        c1RespawnPoint = new Vector3(character1.transform.position.x, character1.transform.position.y, character1.transform.position.z);
        c2RespawnPoint = new Vector3(character2.transform.position.x, character2.transform.position.y, character2.transform.position.z);

        character2MoveScript.enabled = false;
        currentCharacter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SwitchCharacter();

        }

        character1DeathsText.text = "Character 1 Deaths: " + character1Deaths.ToString();
        character2DeathsText.text = "Character 2 Deaths: " + character2Deaths.ToString();

        
        if (numCharactersBeatenLevel >= 2 && hasRun == false)
        {
            EndLevel();
            //ONLY RUN ONCE
            hasRun = true; 
        }
    }

    public void SwitchCharacter()
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

    public void RespawnCharacter(GameObject character)
    {
        
        if (character.CompareTag("Character1"))
        {
            
            character.transform.position = c1RespawnPoint;
        }
        if (character.CompareTag("Character2"))
        {
            character.transform.position = c2RespawnPoint;
        }
    }

    public void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Play Fade out animation!!!
        //GO TO NEXT LEVEL
    }
}
