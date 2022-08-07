using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParameter : MonoBehaviour
{
    public int band;
    public float startScale, scaleMultipier;
    public bool useBuffer; 
   
    void Start()
    {
        
    }

    void Update()
    {
        if (useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (MicrophoneInput.bandBuffer[band] * scaleMultipier) + startScale, transform.localScale.z);
        }
        if (!useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (MicrophoneInput.frequencyBand[band] * scaleMultipier) + startScale, transform.localScale.z);
        }
    }
}
