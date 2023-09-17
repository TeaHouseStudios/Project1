using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI character1DeathsText;
    public TextMeshProUGUI character2DeathsText;
    public TextMeshProUGUI timerText;

    public GameObject characterPortraitImage;
    public Sprite character1Portrait;
    public Sprite character2Portrait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Events.onLevelEnd.AddListener(DisableGameUI);
    }
    private void OnDisable()
    {
        Events.onLevelEnd.RemoveListener(DisableGameUI);
    }

    public void DisableGameUI(float score)
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer(GameManager.Instance.timer);
        character1DeathsText.text = "Character 1 Deaths: " + GameManager.Instance.character1Deaths.ToString();
        character2DeathsText.text = "Character 2 Deaths: " + GameManager.Instance.character2Deaths.ToString();

        
        if (GameManager.Instance.currentCharacter == 1)
        {
            characterPortraitImage.GetComponent<Image>().sprite = character1Portrait;
        }
        else if (GameManager.Instance.currentCharacter == 2)
        {
            characterPortraitImage.GetComponent<Image>().sprite = character2Portrait;
        }
    }

    void UpdateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
