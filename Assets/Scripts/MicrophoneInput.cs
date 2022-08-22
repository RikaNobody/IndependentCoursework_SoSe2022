using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    AudioSource audioSource; 
    AudioClip audioClip;
    
    public string selectedDevice;
    public AudioMixerGroup audioMixerGroupMicrophone, audioMixerGroupMaster;

    public static float[] samples = new float[512]; // 512 weil die Spectrum data für die Samples 512 werte gibt
    public static float[] frequencyBand = new float[8]; // die 512 Values in 8 regionen zusammenfassen 
    public static float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];  


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // - - - Microphone Input Stuff - - - 
        if (Microphone.devices.Length > 0)
        {
            selectedDevice = Microphone.devices[0].ToString();
            audioSource.outputAudioMixerGroup = audioMixerGroupMicrophone;
            audioSource.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate ); 
        }
        else
        {
            audioSource.outputAudioMixerGroup = audioMixerGroupMaster; 
        }

        audioSource.Play();
    }

   
    void Update()
    {
        GetSpectrumOfAudioSource();
        CreateFrequencyBand();
        Buffer(); 
    }

    public void GetSpectrumOfAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman); 
    }

    public void CreateFrequencyBand()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2,i)*2;
            if (i==7)
            {
                sampleCount += 2; 
            }
            for (int j = 0; j<sampleCount; j++)
            {
                
                average += samples[count]* (count +1);
                count++; 
            }
            average = average/count;
            frequencyBand[i] = average * 10;
            //Debug.Log("AVERAGE INPUT - - " + average ); 
        }
    }

    public void Buffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (frequencyBand[i] > bandBuffer[i]) {
                bandBuffer[i] = frequencyBand[i];
                bufferDecrease[i] = 0.002f; 
            }
            if (frequencyBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f; 
            }
        }
    }

    public float[] GetFrequencyBand()
    {
        return frequencyBand; 
    }
}
