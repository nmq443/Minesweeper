using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject choosingModePict;
    public void Restart() {
        if (SceneManager.GetActiveScene().name == "9x9Mode")
            SceneManager.LoadScene("9x9Mode");
        else if (SceneManager.GetActiveScene().name == "16x16Mode")
            SceneManager.LoadScene("16x16Mode");
        else if (SceneManager.GetActiveScene().name == "16x30Mode")
            SceneManager.LoadScene("16x30Mode");
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }

    public void Mode9x9() {
        SceneManager.LoadScene("9x9Mode");
    }

    public void Mode16x16() {
        SceneManager.LoadScene("16x16Mode");
    }

    public void Mode16x30() {
        SceneManager.LoadScene("16x30Mode");
    }

    public void Return() {
        choosingModePict.SetActive(false);
    }

    public void Play() {
        choosingModePict.SetActive(true);
    }
    
    public void Quit() {
        Application.Quit();
    }
}
