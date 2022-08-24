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
    public int numberStart, stepSize, maxIteration, colorChanger;

    public Material[] changeMaterials = new Material[4];  
    private int number, counter;
    
    public static float[] frequencyBand = new float[8];

    public bool useLerping;
    public bool useAudioInput; 
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
        colorChanger = 0; 
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

        if (!useAudioInput)
        {
            if (hud.startButtonPressed) { 
                _phyllotaxisPosition = CalculatePhyllotaxis(hudDegree, hudScale, numberStart);
                GameObject circleInstance = (GameObject)Instantiate(_circle); 
                circleInstance.transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);   
                circleInstance.transform.localScale = new Vector3(_circleScale, _circleScale, _circleScale); 
                numberStart++; 
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
           
            if (hud.startButtonPressed)
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
                colorChanger++;
                if (average > 0.02)
                {
                    Debug.Log("- - - WERTE ÄNDERN - - -");
                    audioInputDegree++;

                }
            }
            if (colorChanger == 50 || colorChanger == 100 || colorChanger == 150)
            {
                //audioInputDegree = 0;
                _circle.GetComponent<MeshRenderer>().material = GetRadomMaterial(changeMaterials); 
            }
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
}
