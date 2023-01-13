using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

// VideoController requires the GameObject to have a VideoPlayer component
[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    [SerializeField] private Renderer mySphereRenderer;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RenderTexture render3840x1920, render4096x2048, render5120x2560, render5760x2880; //render textures from textures folder
    [SerializeField] private  Material material3840x1920, material4096x2048, material5120x2560, material5760x2880; //video material from materials folder

    void Awake()
    {
        mySphereRenderer = GetComponent<Renderer>();
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        ///adding video player events
        videoPlayer.seekCompleted += OnComplete;
        videoPlayer.prepareCompleted += OnComplete;
        videoPlayer.loopPointReached += OnLoop;
        videoPlayer.errorReceived += VideoPlayer_errorReceived;
        //setting video sphere render default material 
        mySphereRenderer.material = material5760x2880;
        mySphereRenderer.GetComponent<GameObject>().SetActive(false);
        videoPlayer.targetTexture = render5760x2880;
    }

    /// Called from CanvasManager and passing video file path on oculus gallery as a parameter
    public void GetVideoProperties(string filePath)
    {
        videoPlayer.url = filePath;
        videoPlayer.Prepare();
    }

    private void OnComplete(VideoPlayer videoPlayer)
    {
        ManageVideoTexture(videoPlayer.texture.width, videoPlayer.texture.height);
        videoPlayer.Play();
    }

    /// ManageVideoTextures according to video file execution chosen
    private void ManageVideoTexture(int x, int y)
    {
        if (x == 3840 && y == 1920)
        {
            mySphereRenderer.material = material3840x1920;
            videoPlayer.targetTexture = render3840x1920;
        }

        else if (x == 4096 && y == 2048)
        {
            mySphereRenderer.material = material4096x2048;
            videoPlayer.targetTexture = render4096x2048;
        }

        else if (x == 5120 && y == 2560)
        {
            mySphereRenderer.material = material5120x2560;
            videoPlayer.targetTexture = render5120x2560;
        }
        else if (x == 5760 && y == 2880)
        {
            mySphereRenderer.material = material5760x2880;
            videoPlayer.targetTexture = render5760x2880;
        }
    }

    public class VideoEvent : UnityEvent<bool> {}

    private void OnLoop(VideoPlayer videoPlayer)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void VideoPlayer_errorReceived(VideoPlayer source, string message)
    {
        //Debug.Log("" + message);
        /// To avoid memory leaks, unsubscribe from the event otherwise it could continuously send this message
        videoPlayer.errorReceived -= VideoPlayer_errorReceived;
    }

    private void OnDestroy()
    {
        videoPlayer.seekCompleted -= OnComplete;
        videoPlayer.prepareCompleted -= OnComplete;
        videoPlayer.loopPointReached -= OnLoop;
    }
}
