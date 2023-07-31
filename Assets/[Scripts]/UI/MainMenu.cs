/* 27 July 2023
 * I just realized that this is the first script that I personally authored
 * throughout this entire project. And we are a month into production.
 * Thanks Grant for knowing what you're doing.
 * -P
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("GAME QUIT"); // ensure quit actually happens
        Application.Quit();
    }

}
