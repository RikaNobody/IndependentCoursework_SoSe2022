using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class HUDManager : MonoBehaviour
{
    public GameObject degreeInput;
    public GameObject circleScaleInput;
    public GameObject currentHUD; 
    public float degree, circleScale; 
    string _degreeInput, _circleScaleInput;

    
    void Start()
    {
       _degreeInput = degreeInput.gameObject.GetComponent<TMP_InputField>().text;
       _circleScaleInput = circleScaleInput.gameObject.GetComponent<TMP_InputField>().text;

    }

    void Update()
    {
        _degreeInput = degreeInput.gameObject.GetComponent<TMP_InputField>().text;
        degree = float.Parse(_degreeInput); 
        
        _circleScaleInput = circleScaleInput.gameObject.GetComponent<TMP_InputField>().text;
        circleScale = float.Parse(_circleScaleInput); 
       
        Debug.Log("DEGREE INPUT: " +  _degreeInput);
        Debug.Log("CIRCLE SCALE INPUT: " + _circleScaleInput);

        if (Input.GetKey(KeyCode.Space))
        {
            currentHUD.SetActive(false); 
        }
        else
        {
            currentHUD.SetActive(true);
        }
    }
}
