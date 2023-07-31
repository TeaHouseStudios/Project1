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

    public Vector3 c1RespawnPoint;
    public Vector3 c2RespawnPoint;

    public int currentCharacter;

    public int character1Deaths;
    public int character2Deaths;

    public TextMeshProUGUI character1DeathsText;
    public TextMeshProUGUI character2DeathsText;
    public TextMeshProUGUI timerText;
    public float timer;

    public int numCharactersBeatenLevel = 0;
    private bool hasRun = false;

    public GameObject characterPortraitImage;
    public Sprite character1Portrait;
    public Sprite character2Portrait;

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

        character1Deaths = 0;
        character2Deaths = 0;

        

        c1RespawnPoint = new Vector3(character1.transform.position.x, character1.transform.position.y, character1.transform.position.z);
        c2RespawnPoint = new Vector3(character2.transform.position.x, character2.transform.position.y, character2.transform.position.z);

        
        currentCharacter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SwitchCharacter();

        }

        timer = timer += Time.deltaTime;
        UpdateTimer(timer);

        character1DeathsText.text = "Character 1 Deaths: " + character1Deaths.ToString();
        character2DeathsText.text = "Character 2 Deaths: " + character2Deaths.ToString();

        
        if (numCharactersBeatenLevel >= 2 && hasRun == false)
        {
            EndLevel();
            //ONLY RUN ONCE
            hasRun = true; 
        }
    }

    void UpdateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60); 
        float seconds = Mathf.FloorToInt(currentTime % 60);

        
    }

    public void SwitchCharacter()
    {
        
        Debug.Log("Switch");
        if (currentCharacter == 1)
        {
            characterPortraitImage.GetComponent<Image>().sprite = character2Portrait;
            currentCharacter = 2;
            Events.onSwitch.Invoke(currentCharacter);
        }
        else if (currentCharacter == 2)
        {
            characterPortraitImage.GetComponent<Image>().sprite = character1Portrait;
            currentCharacter = 1;
            Events.onSwitch.Invoke(currentCharacter);
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
