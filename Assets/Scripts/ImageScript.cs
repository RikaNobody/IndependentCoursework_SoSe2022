using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{

    Button imgBtn;
    public GameObject bigImage;

    void Start()
    {
        imgBtn = GetComponent<Button>();
        imgBtn.onClick.AddListener(ImageClicked);
        bigImage = GameObject.FindWithTag("BigImage");
        bigImage.GetComponent<RawImage>().enabled = false;
    }

    void ImageClicked()
    {
        Debug.Log("bild geklickt: " + gameObject.GetComponent<RawImage>().texture.ToString());


        bigImage.GetComponent<RawImage>().enabled = true;
        bigImage.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 580);
        bigImage.GetComponent<RawImage>().texture = this.gameObject.GetComponent<RawImage>().texture;

        bigImage.AddComponent<Button>();
        bigImage.GetComponent<Button>().onClick.AddListener(CloseImage);
    }

    void CloseImage()
    {
        Debug.Log("Close Image");
        bigImage.GetComponent<RawImage>().enabled = false;
    }
}
