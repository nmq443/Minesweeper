using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject difficultyPanel;
    [SerializeField] GameObject creditPanel;

    private void Start()
    {
        if (difficultyPanel != null)
            difficultyPanel.SetActive(false);

        if (creditPanel != null) 
            creditPanel.SetActive(false);
    }

    public void PlayGame()
    {
        if (difficultyPanel != null)
            difficultyPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Credit()
    {
        if (creditPanel != null) 
            creditPanel.SetActive(true);
    }

    public void EasyMode()
    {
        SceneManager.LoadScene("EasyModeScene");
    }

    public void MediumMode()
    {
        SceneManager.LoadScene("MediumModeScene");
    }

    public void HardMode()
    {
        SceneManager.LoadScene("HardModeScene");
    }
}

