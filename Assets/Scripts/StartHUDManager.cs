using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartHUDManager : MonoBehaviour
{
    public GameObject startHUD, optionsHUD, creditsHUD;
    public Button phyllotaxisButton, fractalButton, quitButton, optionsButton, applyButton, exhibitionButton, backButton, creditsButton, creditsBackButton;
    public Dropdown resolutionDropdown;
    int dropDownValue;
    void Start()
    {
        phyllotaxisButton.onClick.AddListener(StartPhyllotaxis);
        quitButton.onClick.AddListener(QuitApp);
        optionsButton.onClick.AddListener(OpenOptions);
        applyButton.onClick.AddListener(ApplyResolution);
        fractalButton.onClick.AddListener(StartFractal);
        exhibitionButton.onClick.AddListener(OpenExhibition);
        backButton.onClick.AddListener(BackToMenu);
        creditsButton.onClick.AddListener(ShowCredits);
        creditsBackButton.onClick.AddListener(CloseCredits);
    }

    void Update()
    {

    }

    public void StartPhyllotaxis()
    {
        //Debug.Log("S T A R T");
        SceneManager.LoadScene(1);
    }

    public void StartFractal()
    {
        SceneManager.LoadScene(2);
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

    public void OpenExhibition()
    {
        SceneManager.LoadScene(3);
    }

    public void BackToMenu()
    {
        optionsHUD.SetActive(false);
        startHUD.SetActive(true);

    }
    public void ShowCredits()
    {
        startHUD.SetActive(false);
        creditsHUD.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsHUD.SetActive(false);
        startHUD.SetActive(true);
    }

    public void ApplyResolution()
    {
        dropDownValue = resolutionDropdown.value;
        PlayerPrefs.SetInt("resolution", dropDownValue);
        optionsHUD.SetActive(false);
        startHUD.SetActive(true);


    }
}
