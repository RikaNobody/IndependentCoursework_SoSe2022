using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TMP_InputField degreeInput;
    public GameObject circleScaleInput;
    public GameObject currentHUD;
    public Toggle audioInputToggle, trailRendererToggel, renderDotsToggle;
    public Dropdown chooseColorPalette, chooseTrailColor;
    public GameObject colorChangerInput;
    public Button startButton;
    public Button resetButton;
    public float degree, circleScale;
    public int colorChanger, dropdownValue, dropdownValueTrail;
    string _degreeInput, _circleScaleInput, _colorChangerInput;
    public bool startButtonPressed, useAudioInput, useTrailRenderer, drawDots;


    void Start()
    {
        _degreeInput = degreeInput.gameObject.GetComponent<TMP_InputField>().text;
        _circleScaleInput = circleScaleInput.gameObject.GetComponent<TMP_InputField>().text;
        _colorChangerInput = colorChangerInput.gameObject.GetComponent<InputField>().text;
        resetButton.interactable = false;

    }

    void Update()
    {
        startButton.onClick.AddListener(EndEditInput);
        resetButton.onClick.AddListener(ResetScene);

        /* _degreeInput = degreeInput.gameObject.GetComponent<TMP_InputField>().text;
         degree = float.Parse(_degreeInput); 

         _circleScaleInput = circleScaleInput.gameObject.GetComponent<TMP_InputField>().text;
         circleScale = float.Parse(_circleScaleInput); 
        */

        // Debug.Log("DEGREE INPUT: " +  _degreeInput);
        // Debug.Log("CIRCLE SCALE INPUT: " + _circleScaleInput);

        if (startButtonPressed)
        {
            currentHUD.SetActive(false);
        }
        else
        {
            currentHUD.SetActive(true);
        }


    }

    public void EndEditInput()
    {
        startButtonPressed = true;
        resetButton.interactable = true;


        if (degreeInput.gameObject.GetComponent<TMP_InputField>().text == "")
        {
            degree = 1;
        }

        else
        {
            _degreeInput = degreeInput.gameObject.GetComponent<TMP_InputField>().text;
            degree = float.Parse(_degreeInput);
        }

        if (circleScaleInput.gameObject.GetComponent<TMP_InputField>().text == "")
        {
            circleScale = 1;
        }
        else
        {
            _circleScaleInput = circleScaleInput.gameObject.GetComponent<TMP_InputField>().text;
            circleScale = float.Parse(_circleScaleInput);
        }

        if (colorChangerInput.gameObject.GetComponent<InputField>().text == "")
        {
            colorChanger = 0;
        }
        else
        {
            _colorChangerInput = colorChangerInput.gameObject.GetComponent<InputField>().text;
            colorChanger = int.Parse(_colorChangerInput);
        }



        useAudioInput = audioInputToggle.isOn;
        useTrailRenderer = trailRendererToggel.isOn;
        drawDots = renderDotsToggle.isOn;
        dropdownValue = chooseColorPalette.value;
        dropdownValueTrail = chooseTrailColor.value;

        //Debug.Log("DEGREE INPUT: " + _degreeInput);
        //Debug.Log("CIRCLE SCALE INPUT: " + _circleScaleInput);
    }

    public void ResetScene()
    {
        //SceneManager.LoadScene("SampleScene");
        //Application.LoadLevel(Application.loadedLevel);
    }



}
