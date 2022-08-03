using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateVisuals : MonoBehaviour
{
    public float maxScale; 
    public GameObject cubePrefab;
    GameObject[] sampleCubes = new GameObject[512]; 

  
    void Start()
    {
        for (int i = 0; i< 512; i++)
        {
            GameObject instancePrefabCube = (GameObject)Instantiate(cubePrefab);
            instancePrefabCube.transform.position = this.transform.position;
            instancePrefabCube.transform.parent = this.transform;
            instancePrefabCube.name = "VisualCube" + i;
            this.transform.eulerAngles = new Vector3 (0, -0.703125f * i, 0);
            instancePrefabCube.transform.position = Vector3.forward * 100; 
            sampleCubes[i] = instancePrefabCube;
        }
    }

    void Update()
    {
        for (int i = 0; i<512; i++)
        {
            if (sampleCubes!= null)
            {
                sampleCubes[i].transform.localScale = new Vector3(10, (MicrophoneInput.samples[i]*maxScale)+2,10); 
            }
        }
    }
}
