using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void OnPlayButton() {
        SceneManager.LoadScene("MainMap");
    }
    public void OnBackButton() {
        SceneManager.LoadScene("Menu");
    }
    public void OnQuitButton() {
        Application.Quit();
    }
    public void OnHelpButton() {
        SceneManager.LoadScene("Tutorial");
    }
}
