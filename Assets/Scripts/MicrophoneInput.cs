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
    }

    public void GetSpectrumOfAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman); 
    }
}
