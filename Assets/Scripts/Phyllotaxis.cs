using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Phyllotaxis : MonoBehaviour
{
    public GameObject _circle;
    public HUDManager hud;
    public GameObject audioManager;
    public MicrophoneInput microphoneInput; 
   
    private TrailRenderer trailRenderer;
  
    public float _degree, _scale, _circleScale, audioInputDegree; 
    public int numberStart, stepSize, maxIteration, colorChanger, instanceCounter, dropdownValue;

    public Material[] colorfulMaterials = new Material[4];
    public Material[] redMaterials = new Material[4];
    public Material[] blueMaterials = new Material[4];
    public Material[] greenMaterials = new Material[4];
    public Material[] purpleMaterials = new Material[4];

    private int number, counter;
    
    public static float[] frequencyBand = new float[8];

    public bool useLerping;
    public bool useAudioInput, useTrailRenderer; 
    public float intervalLerp;
    
    private bool isLerping;
    private Vector3 startPosition, endPosition;
    private float timeStartLerping;
    private int currentIteration; 

    float hudDegree, hudScale; 

    private Vector3 _phyllotaxisPosition;

    void Awake()
    {     
        hudDegree = 0f;
        hudScale = 0f;
        counter = 0;
        instanceCounter = 0; 
        trailRenderer = this.GetComponent<TrailRenderer>();
        number = numberStart;
        transform.position = CalculatePhyllotaxis(hudDegree, hudScale, number);

        microphoneInput = audioManager.GetComponent<MicrophoneInput>(); 

        if (useLerping)
        {
            StartLerping(); 
        }
    }

    void FixedUpdate()
    {
        if (hud.startButtonPressed)
        {
            if (useLerping)
            {
                if (isLerping)
                {
                    float timeSinceStart = Time.time - timeStartLerping;
                    float percentageComplete = timeSinceStart / intervalLerp;
                    transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);

                    if (percentageComplete >= 0.97f) // Das ist shitty 
                    {
                        transform.position = endPosition;
                        number += stepSize;
                        currentIteration++;

                        if (currentIteration <= maxIteration)
                        {
                            StartLerping();
                        }

                        else
                        {
                            isLerping = false;
                        }
                    }
                }
            }

            else
            {
                _phyllotaxisPosition = CalculatePhyllotaxis(hudDegree, hudScale, number);
                transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
                number += stepSize;
                currentIteration++;
            }
        }
       
    }
    void Update()
    {
        hudDegree = hud.degree;
        hudScale = hud.circleScale;
        colorChanger = hud.colorChanger;
        useAudioInput = hud.useAudioInput;
        useTrailRenderer = hud.useTrailRenderer;
        dropdownValue = hud.dropdownValue; 

        if (!useAudioInput)
        {
            if (hud.startButtonPressed && hud.drawDots) { 
                _phyllotaxisPosition = CalculatePhyllotaxis(hudDegree, hudScale, numberStart);
                GameObject circleInstance = (GameObject)Instantiate(_circle); 
                circleInstance.transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);   
                circleInstance.transform.localScale = new Vector3(_circleScale, _circleScale, _circleScale); 
                numberStart++;
                instanceCounter++;
            }
        }
     
        

        if (useAudioInput)
        {    
            float average = 0;
            
            for (int i = 0; i<8; i++)
            {
                frequencyBand[i] = microphoneInput.GetFrequencyBand()[i];
                average = average + frequencyBand[i];
            }

            average = average / 8;
           
            if (hud.startButtonPressed && hud.drawDots)
            {
                if (counter == 0)
                {
                    audioInputDegree = hudDegree;
                }
                Debug.Log(" - DEGREE - " + audioInputDegree);
                _phyllotaxisPosition = CalculatePhyllotaxis(audioInputDegree, hudScale, numberStart);
                GameObject circleInstance = (GameObject)Instantiate(_circle);
                circleInstance.transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
                circleInstance.transform.localScale = new Vector3(_circleScale, _circleScale, _circleScale);
                numberStart++;
                counter++;
                instanceCounter++;
                if (average > 0.02)
                {
                    Debug.Log("- - - WERTE ÄNDERN - - -");
                    audioInputDegree++;

                }
            }
         
        }

        if (useTrailRenderer)
        {
            trailRenderer.enabled = true;  
        }

        if (instanceCounter == colorChanger)
        {
            //audioInputDegree = 0;
            instanceCounter = 0;
            _circle.GetComponent<MeshRenderer>().material = GetRadomMaterial(GetColorPalette(dropdownValue));
        }
    }  
    private Vector2 CalculatePhyllotaxis(float degree, float scale, int number){
        double angle = number * (degree* Mathf.Deg2Rad); 
        float r = scale * Mathf.Sqrt(number); 
        float x = r*(float)System.Math.Cos(angle);
        float y = r*(float)System.Math.Sin(angle); 

        Vector2 vector2 = new Vector2(x,y); 
        return vector2; 
    }
    void StartLerping()
    {
        isLerping = true; 
        timeStartLerping = Time.time;
        _phyllotaxisPosition = CalculatePhyllotaxis(hudDegree, hudScale, number);
        startPosition = this.transform.position;
        endPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.x, 0); 
    }

    public Material GetRadomMaterial(Material [] selectedColors)
    {
        Material selectedColor;

        int randomInt = Random.Range(0,4);
        selectedColor = selectedColors[randomInt];

        return selectedColor; 
    }

    public Material[] GetColorPalette(int dropdownValue)
    {
        Material[] selectedColorPalette = new Material[4];

        switch (dropdownValue)
        {
            case 0:
                selectedColorPalette = colorfulMaterials;
                Debug.Log("Case 1"); 
                break;
            case 1:
                selectedColorPalette = redMaterials;
                Debug.Log("Case 2");
                break;
            case 2:
                selectedColorPalette = blueMaterials;
                Debug.Log("Case 3");
                break;
            case 3:
                selectedColorPalette = greenMaterials;
                Debug.Log("Case 4");
                break;
            case 4:
                selectedColorPalette = purpleMaterials;
                Debug.Log("Case 5");
                break;
            default:
                selectedColorPalette = colorfulMaterials;
                Debug.Log("Case default");
                break; 
        }

        return selectedColorPalette; 

    }
}
