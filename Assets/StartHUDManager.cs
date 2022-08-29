using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartHUDManager : MonoBehaviour
{
    public GameObject startHUD;
    public Button startButton, quitButton, optionsButton;
    void Start()
    {
        startButton.onClick.AddListener(StartPhyllotaxis);
        quitButton.onClick.AddListener(QuitApp);
        optionsButton.onClick.AddListener(OpenOptions);
    }

    void Update()
    {

    }

    public void StartPhyllotaxis()
    {
        //Debug.Log("S T A R T");
        SceneManager.LoadScene(1);
    }

    public void QuitApp()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OpenOptions()
    {
        // Debug.Log(" - - - ! ! ! OPTIONS ! ! ! - - - ");
    }
}
