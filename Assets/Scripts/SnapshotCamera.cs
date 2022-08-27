using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=d-56p770t0U


[RequireComponent(typeof(Camera))]
public class SnapshotCamera : MonoBehaviour
{
    Camera snapshotCamera;
    int resWidth = 1920;
    int resHeight = 1080;

    void Start()
    {
        snapshotCamera = GetComponent<Camera>();
        if (snapshotCamera.targetTexture == null)
        {
            snapshotCamera.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = snapshotCamera.targetTexture.width;
            resHeight = snapshotCamera.targetTexture.height;
        }

        snapshotCamera.gameObject.SetActive(false);

    }



    void LateUpdate()
    {
        if (snapshotCamera.gameObject.activeInHierarchy)
        {
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            snapshotCamera.Render();
            RenderTexture.active = snapshotCamera.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            byte[] bytes = snapshot.EncodeToPNG();
            string snapshotName = SetSnapshotName();
            System.IO.File.WriteAllBytes(snapshotName, bytes);
            Debug.Log("DU HAST EIN FOTO GEMACHT!! SIEHT COOL AUS!");
            snapshotCamera.gameObject.SetActive(false);
        }
    }

    public void CallTakeSnapshot()
    {
        snapshotCamera.gameObject.SetActive(true);
    }

    public string SetSnapshotName()
    {
        string snapshotName;

        snapshotName = string.Format("{0}/Snapshots/Shot-{1}x{2}_{3}.png", Application.dataPath, resWidth, resHeight, System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-s"));

        return snapshotName;
    }
}
