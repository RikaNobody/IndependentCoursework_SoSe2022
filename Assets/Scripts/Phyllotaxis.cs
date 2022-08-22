using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Phyllotaxis : MonoBehaviour
{
    public GameObject _circle;
    public HUDManager hud;
   
    private TrailRenderer trailRenderer; 
    public float _degree, _scale, _circleScale; 
    public int numberStart, stepSize, maxIteration;
    private int number;
    
    public bool useLerping;
    public float intervalLerp;
    private bool isLerping;
    private Vector3 startPosition, endPosition;
    private float timeStartLerping;

    private int currentIteration; 

    float hudDegree, hudScale; 

    private Vector3 _phyllotaxisPosition;

    void Awake()
    {   
        hud = gameObject.GetComponent<HUDManager>();
        hudDegree = 0f;
        hudScale = 0f; 
        trailRenderer = GetComponent<TrailRenderer>();
        number = numberStart;
        transform.localPosition = CalculatePhyllotaxis(_degree, _scale, number);

        if (useLerping)
        {
            StartLerping(); 
        }
    }

    void FixedUpdate()
    {
        if (useLerping)
        {
            if (isLerping)
            {
                float timeSinceStart = Time.time - timeStartLerping;
                float percentageComplete = timeSinceStart / intervalLerp;
                transform.localPosition = Vector3.Lerp(startPosition, endPosition, percentageComplete);
               
                if (percentageComplete >= 0.97f) // Das ist shitty 
                {
                    transform.localPosition = endPosition;
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
            _phyllotaxisPosition = CalculatePhyllotaxis(_degree, _scale, number);
            transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
            number += stepSize;
            currentIteration++;
        }
       
    }
    void Update()
    {
       // hudDegree = hud.degree;
       // hudScale = hud.circleScale; 

        if (Input.GetKey(KeyCode.Space)){
          //  hudDegree = hud.degree;
          //  hudScale = hud.circleScale;
          // _phyllotaxisPosition = CalculatePhyllotaxis(hudDegree, hudScale, numberStart);
            _phyllotaxisPosition = CalculatePhyllotaxis(_degree, _scale, numberStart);
            GameObject circleInstance = (GameObject)Instantiate(_circle); 
            circleInstance.transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);   
            circleInstance.transform.localScale = new Vector3(_circleScale, _circleScale, _circleScale); 
            numberStart++; 
        }
      
        if(Input.GetKey(KeyCode.KeypadEnter)) {
             SceneManager.LoadScene("SampleScene");
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
        _phyllotaxisPosition = CalculatePhyllotaxis(_degree, _scale, number);
        startPosition = this.transform.localPosition;
        endPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.x, 0); 
    }
}
