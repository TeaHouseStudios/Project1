using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject character1;
    private GameObject character2;
    public Rigidbody2D character1Rigidbody;
    public Rigidbody2D character2Rigidbody;

    public Vector3 c1RespawnPoint;
    public Vector3 c2RespawnPoint;

    public int currentCharacter;

    public int character1Deaths;
    public int character2Deaths;

    public float timer;

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
        timer = 0.0f;
        character1 = GameObject.FindGameObjectWithTag("Character1");
        character2 = GameObject.FindGameObjectWithTag("Character2");
        character1Rigidbody = character1.GetComponent<Rigidbody2D>();
        if (character1Rigidbody == null)
        {
            Debug.LogError("character1 does not have a Rigidbody2D component attached.");
            return;
        }
        character2Rigidbody = character2.GetComponent<Rigidbody2D>();

        character1Deaths = 0;
        character2Deaths = 0;

        

        c1RespawnPoint = new Vector3(character1.transform.position.x, character1.transform.position.y, character1.transform.position.z);
        c2RespawnPoint = new Vector3(character2.transform.position.x, character2.transform.position.y, character2.transform.position.z);

        
        currentCharacter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && numCharactersBeatenLevel == 0)
        {
            SwitchCharacter();

        }

        timer = timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && numCharactersBeatenLevel != 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }


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
            
            currentCharacter = 2;
            Events.onSwitch.Invoke(currentCharacter);
        }
        else if (currentCharacter == 2)
        {
            
            currentCharacter = 1;
            Events.onSwitch.Invoke(currentCharacter);
        }
    }

    public void RespawnCharacter(int characterIndex)
    {
        

        if (characterIndex == 1)
        {
            if (character1Rigidbody == null)
            {
                Debug.LogError("character1 Rigidbody2D is null");
                return;
            }

            //Debug.Log("Before setting isKinematic to true");
            character1Rigidbody.isKinematic = true;

            //Debug.Log("Before setting position");
            character1.transform.position = c1RespawnPoint;

            //Debug.Log("Before setting isKinematic to false");
            character1Rigidbody.isKinematic = false;

            //Debug.Log("Before setting isDead to false");
            character1.GetComponent<CharacterMovement>().isDead = false;

        }
        else
        {
            if (character2Rigidbody == null)
            {
                Debug.LogError("character1 Rigidbody2D is null");
                return;
            }

            //Debug.Log("Before setting isKinematic to true");
            character2Rigidbody.isKinematic = true;

            //Debug.Log("Before setting position");
            character2.transform.position = c2RespawnPoint;

            //Debug.Log("Before setting isKinematic to false");
            character2Rigidbody.isKinematic = false;

            //Debug.Log("Before setting isDead to false");
            character2.GetComponent<CharacterMovement>().isDead = false;

        }
    }

    public void EndLevel()
    {

        Events.onLevelEnd.Invoke(CalculateScore());
        //Play Fade out animation!!!
        //GO TO NEXT LEVEL
    }
    public float CalculateScore()
    {
        float score = timer + 5 * (character1Deaths + character2Deaths);
        score = Mathf.Floor(score);

        return score;
    }
}
