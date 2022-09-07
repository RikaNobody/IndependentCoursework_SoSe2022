using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class FractalHUDManager : MonoBehaviour
{
    public GameObject fractalSetUpHUD;
    public GameObject fractalLiveHUD;

    public Button backButton;

    // Fractal HUD 
    public Dropdown chooseShapeDropdown;
    public Dropdown chooseColor;
    public InputField shapeSize;
    public Slider multiplierValue;
    public TMP_Text multiplierSliderValue;
    public Button startButton;

    // Live Fractal HUD 
    public Toggle useBezierCurves;
    public Slider bezierCurvesVertexCount;
    public TMP_Text vertexCountSliderValue;
    public Slider lerpAmount;
    public TMP_Text lerpAmountSliderValue;
    public Button restartButton;
    public Button snapButton;

    //
    public bool startButtonPressed;
    public bool useBezier;
    public bool shapeChanged;
    public bool colorIsChanged;
    public int bezierVertexCount;
    public int lerpAmountValue;
    public int _shapeSize;
    public int _multiplierValue;
    public int choosenColor;

    public SnapshotCamera snapshotCamera;

    void Start()
    {
        startButton.onClick.AddListener(EndEditInput);
        restartButton.onClick.AddListener(RestartScene);
        chooseShapeDropdown.onValueChanged.AddListener(delegate { ChangedShapeValue(chooseShapeDropdown.value); });
        chooseColor.onValueChanged.AddListener(delegate { ChangeColor(chooseColor.value); });
        snapButton.onClick.AddListener(CallSnap);
        backButton.onClick.AddListener(LoadMainMenue);
    }

    void ChangedShapeValue(int value)
    {
        shapeChanged = true;
    }

    void ChangeColor(int value)
    {
        colorIsChanged = true;
        choosenColor = value;
    }



    void Update()
    {
        multiplierSliderValue.text = multiplierValue.value.ToString();
        vertexCountSliderValue.text = bezierCurvesVertexCount.value.ToString();
        lerpAmountSliderValue.text = lerpAmount.value.ToString();

        if (startButtonPressed)
        {
            useBezier = useBezierCurves.isOn;
            bezierVertexCount = int.Parse(bezierCurvesVertexCount.value.ToString());
            lerpAmountValue = int.Parse(lerpAmount.value.ToString());
        }
    }

    void EndEditInput()
    {
        startButtonPressed = true;

        if (shapeSize.gameObject.GetComponent<InputField>().text == "")
        {
            _shapeSize = 1;
        }
        else
        {
            string _shapeInput = (shapeSize.gameObject.GetComponent<InputField>().text.ToString());
            _shapeSize = int.Parse(_shapeInput);
        }

        _multiplierValue = int.Parse(multiplierValue.value.ToString());

        fractalSetUpHUD.SetActive(false);
        fractalLiveHUD.SetActive(true);

    }

    void CallSnap()
    {
        snapshotCamera.CallTakeSnapshot();
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadMainMenue()
    {
        SceneManager.LoadScene(0);
    }

}
