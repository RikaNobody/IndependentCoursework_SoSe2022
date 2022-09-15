using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class Gallery : MonoBehaviour
{
    public string[] allSnapshots;
    public GameObject[] img;
    public GameObject imgPrefab;
    public RawImage bigImage;
    string folderPath = "Assets/Resources/Snapshots";
    public Texture2D[] allImages;
    public Texture2D tempTex;
    public GameObject panel;
    public GameObject scrollView;

    public Button backButton;
    void Start()
    {
        AssetDatabase.Refresh();
        backButton.onClick.AddListener(BackToMainMenu);


        allSnapshots = System.IO.Directory.GetFiles(folderPath, "*.png");

        img = new GameObject[allSnapshots.Length];

        for (int i = 0; i < allSnapshots.Length; i++)
        {
            img[i] = new GameObject();
            img[i].transform.parent = scrollView.transform.GetChild(0).transform.GetChild(0);
            img[i].AddComponent<RectTransform>();
            img[i].GetComponent<RectTransform>().sizeDelta = new Vector2(480, 270);
            img[i].AddComponent<Button>();
            img[i].AddComponent<ImageScript>();

            allSnapshots[i] = allSnapshots[i].Substring(17);
            allSnapshots[i] = allSnapshots[i].Substring(0, allSnapshots[i].Length - 4);
        }

        allImages = new Texture2D[allSnapshots.Length];

        for (int i = 0; i < allSnapshots.Length; i++)
        {
            allImages[i] = Resources.Load(allSnapshots[i]) as Texture2D;
            img[i].AddComponent<RawImage>();
            img[i].GetComponent<RawImage>().texture = allImages[i];
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
