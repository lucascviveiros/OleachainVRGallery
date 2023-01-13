using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Canvas canvasUI;
    [SerializeField] private GameObject invisibleReference;
    [SerializeField] private GameObject videoItemToggle; // ButtomItemToggle from prefabs folder
    [SerializeField] private StreamingFiles streamingFiles;
    [SerializeField] private VideoController videoController;
    private List<string> videoList = new List<string>();
    private int totalVideosFromGallery;
    private Texture2D boxTexture;
    private Sprite boxSprite;
    public GameObject AmbientVisibleAfterVideoExecution;
    public Renderer meshRenderer360;

    void Awake()
    {
        canvasUI = GameObject.Find("Canvas").GetComponent<Canvas>();
        invisibleReference = GameObject.Find("InvisibleReference");
        streamingFiles = FindObjectOfType<StreamingFiles>();
        videoController = FindObjectOfType<VideoController>();
        AmbientVisibleAfterVideoExecution = GameObject.Find("AmbientVisibleAfterVideoExecution");
        meshRenderer360 = GameObject.Find("360SphereVideoPlayer").GetComponent<Renderer>();
    }

    void Start()
    {
        if (streamingFiles.GetVideoListFilled())
            AddVideosToCanvas();

        meshRenderer360.enabled = false;
    }

    private void Update()
    {
        /* if (OVRInput.Get(OVRInput.Button.Back)) //(Input.GetKeyDown(KeyCode.Escape)
        {
            Debug.Log("Back Pressed");
            canvasUI.enabled = false;
        }*/
        /*
        if (OVRInput.GetDown(OVRInput.Button.One))//|| OVRInput.GetDown(OVRInput.Button.Back)) //Back for go
        {
            canvasUI.enabled = !canvasUI.enabled;
        } */
    }

    private void AddVideosToCanvas()
    {
        totalVideosFromGallery = streamingFiles.GetTotalVideosFromGallery();
        videoList = streamingFiles.GetVideoNames();

        for (int i = 0; i < totalVideosFromGallery; i++)
        {
            GameObject videoItem = Instantiate(videoItemToggle);
            videoItem.name = videoList[i];
            videoItem.transform.SetParent(invisibleReference.transform.parent, false);
            videoItem.GetComponent<ToggleList>().SetToggleText(videoList[i], i);

            //Getting button component from videoItem toggle
            Button button = videoItem.GetComponent<ToggleList>().GetButton();
            //Adding onClick event with delegate to each button
            button.onClick.AddListener(delegate () { ButtonAction(button); });
        }
    }

    //Button action event
    private void ButtonAction(Button g)
    {
        int index = g.GetComponent<ToggleList>().GetIndex();
        CheckVideoResolution(videoList[index]);
    }

    private void CheckVideoResolution(string videoPath)
    {
        meshRenderer360.enabled = true;
        AmbientVisibleAfterVideoExecution.SetActive(false);
        canvasUI.enabled = false;
        videoController.GetVideoProperties(videoPath);
    }
}
