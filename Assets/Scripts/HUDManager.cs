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
    public Button startButton;
    public Button resetButton; 
    public float degree, circleScale; 
    string _degreeInput, _circleScaleInput;
    public bool startButtonPressed; 

    
    void Start()
    {
       _degreeInput = degreeInput.gameObject.GetComponent<TMP_InputField>().text;
       _circleScaleInput = circleScaleInput.gameObject.GetComponent<TMP_InputField>().text;
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
        _degreeInput = degreeInput.gameObject.GetComponent<TMP_InputField>().text;
        degree = float.Parse(_degreeInput);

        _circleScaleInput = circleScaleInput.gameObject.GetComponent<TMP_InputField>().text;
        circleScale = float.Parse(_circleScaleInput);

        Debug.Log("DEGREE INPUT: " + _degreeInput);
        Debug.Log("CIRCLE SCALE INPUT: " + _circleScaleInput);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
