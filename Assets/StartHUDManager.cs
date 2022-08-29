using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartHUDManager : MonoBehaviour
{
    public GameObject startHUD, optionsHUD;
    public Button startButton, quitButton, optionsButton, applyButton;
    public Dropdown resolutionDropdown;
    int dropDownValue;
    void Start()
    {
        startButton.onClick.AddListener(StartPhyllotaxis);
        quitButton.onClick.AddListener(QuitApp);
        optionsButton.onClick.AddListener(OpenOptions);
        applyButton.onClick.AddListener(ApplyResolution);
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
        startHUD.SetActive(false);
        optionsHUD.SetActive(true);
    }

    public void ApplyResolution()
    {
        dropDownValue = resolutionDropdown.value;
        PlayerPrefs.SetInt("resolution", dropDownValue);
        optionsHUD.SetActive(false);
        startHUD.SetActive(true);


    }
}
