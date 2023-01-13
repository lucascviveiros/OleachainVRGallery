using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;

public class VideoManager : MonoBehaviour
{
    #region publicVariables
    //public GameObject myCubeDirection, myMenuHotspot, myExitApp, scifiManager;
    //public Canvas whiteCanvas;
    public VideoEvent onPause = new VideoEvent();
    public VideoEvent onLoad = new VideoEvent();
    public RenderTexture mRenderTexture5K, mImageFromVideo;// mRenderTexture8K, mImageFromVideo;
    public Material mVideoMaterial5K;//, mVideoMaterial8K; //5K:5760x2880 - 8K:7200x3600
    public Renderer mySphereRenderer;

    Texture2D videoFrameTexture;
    RenderTexture renderTexture;
    Texture videoTexture;
    #endregion

    #region privateVariables
    //private string[] videoList = { "boreal_texto_fixed_NOAUDIO.mp4", "Douro360_h265_8K_OculusQuest2_v3.mp4" };
    private List<string> videoList = new List<string>();

    //private bool isPaused = false;
    private string rootPath; //root path video folder in oculus gallery
    private string path; //actual path with root path and video's name
    private float timer = 0.0f;
    private float videoWidth;
    private float videoHeigh;
    #endregion

    /*
    public bool IsPaused
    {
        get
        {
            return isPaused;
        }

        private set
        {
            isPaused = value;
            //Every time that is set the pause variable is gonna invoke this event
            //If we pause the video with the spacebar is going to cause
            //the pause toogle function which is going to switch that variable 
            //im summary, this event is used for changing quickly between icons, that is, precisly
            onPause.Invoke(isPaused);
        }
    }*/

    private bool isVideoReady = false;
    public bool IsVideoReady
    {
        get
        {
            return isVideoReady;
        }
        private set
        {
            isVideoReady = value;
            onLoad.Invoke(isVideoReady);
        }
    }

    private int index = 0;
    private VideoPlayer videoPlayer = null;
    private VideoPlayer vp;

    public AudioSource audioSource = null;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        //audioSource = GetComponent<AudioSource>();
        //videoPlayer.targetTexture = mRenderTexture5K;
        //mySphereRenderer.material = mVideoMaterial5K;

        //Whenever we complete seeking player we gonna call the complete function 
        //seekComplete is a event
        videoPlayer.seekCompleted += OnComplete;
        videoPlayer.prepareCompleted += OnComplete;
        videoPlayer.loopPointReached += OnLoop;

        //Looping for the menu video
        //videoPlayer.isLooping = true;
        //audioSource.Play();
     
    }

    public void Start()
    {
        videoPlayer.errorReceived += VideoPlayer_errorReceived;

        //audioSource = GetComponent<AudioSource>();
        //PlayVideoMenu();

        //videoFrameTexture = new Texture2D(2, 2);
        //vp = GetComponent<VideoPlayer>();
        //vp.playOnAwake = false;
        //vp.waitForFirstFrame = true;
        //vp.sendFrameReadyEvents = true;
        //vp.frameReady += OnNewFrame;
        //vp.Play();
    }


    public int GetIndex()
    {
        return index;
    }


    public void PlayVideoMenu()
    {
        index = 0;
        StartPrepare(0);
        StopAllCoroutines();

        videoPlayer.targetTexture = mRenderTexture5K;
        mySphereRenderer.material = mVideoMaterial5K;

    }


    public void PlayVideoPortuguese()
    {
        //index = 1;
        StartPrepare(1);
        StopAllCoroutines();
        //videoPlayer.targetTexture = mRenderTexture8K;
        //mySphereRenderer.material = mVideoMaterial8K;
      
    }

    public int GetCurrentVideo()
    {
        return index;
    }

    public void SearchVideoList(int videoPosition)  
    {
        StartPrepare(videoPosition);
    }

    private void StartPrepare(int clipIndex)
    {
        //IsVideoReady = false;
        //videoPlayer.clip = videos[clipIndex];
        //videoPlayer.Prepare();

        isVideoReady = false;
        // videoPlayer.url = videoList[clipIndex];

        videoPlayer.url = StreamingVideo(clipIndex);

        videoPlayer.Prepare();
    }
    
    private void StartPrepareByPath(string clipPath)
    {
        //IsVideoReady = false;
        //videoPlayer.clip = videos[clipIndex];
        //videoPlayer.Prepare();

        isVideoReady = false;
        // videoPlayer.url = videoList[clipIndex];

        videoPlayer.url = clipPath;

        videoPlayer.Prepare();
    }

    private void OnComplete(VideoPlayer videoPlayer)
    {
        videoPlayer.renderMode = VideoRenderMode.APIOnly;

        //IsVideoReady = true;
        videoTexture = videoPlayer.texture;
        videoWidth = videoTexture.width;
        videoHeigh = videoTexture.height;
        Debug.Log("Width: " + videoPlayer.texture.width + "Height: " + videoPlayer.texture.height);

        //videoPlayer.Play();
        /*

        if (index == 0)
        {
            audioSource.clip = videoAudios[0]; //menu sound
            audioSource.Play();
        }
        */
    }

    private void OnLoop(VideoPlayer videoPlayer)  //* Looping only for the Menu video
    {/*
        if (index != 0) 
        {
            //antes era loop somente pro menu agora chamar a cena novamente
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (index == 0)
        {
            //StartCoroutine(ReturnDayLight());
            audioSource.clip = videoAudios[0];
            audioSource.Play();
        }*/

      //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    //VideoEvent is a new class that inherits from the UnityEvent. So we can giver our own custom event
    public class VideoEvent : UnityEvent<bool>
    {

    }

    ///Streaming video from oculus root folder
    public string StreamingVideo(int clipIndex)
    {/*
        string fileName = videoList[clipIndex];
        rootPath = Application.persistentDataPath;
        path = Path.Combine(rootPath, fileName);
        return path;
     */

        //videoPlayer.url = path;
        //videoPlayer.Play();

        return "";
    }

    private void VideoPlayer_errorReceived(VideoPlayer source, string message)
    {
        /// So that I can see the debug message in the headset in my case
        //_debugText.text += message;
        Debug.Log("" + message);
        /// To avoid memory leaks, unsubscribe from the event
        /// otherwise it could continuously send this message
        videoPlayer.errorReceived -= VideoPlayer_errorReceived;
    }

    /*
    public RenderTexture GetVideoImage(string path)
    {
        //videoPlayer.renderMode = VideoRenderMode.APIOnly;
        videoPlayer.url = path;
        videoPlayer.targetTexture = mImageFromVideo;
        videoPlayer.time = 0;
        //videoPlayer.Play();
        //videoPlayer.Stop();
        //videoPlayer.sendFrameReadyEvents = true;
        //videoPlayer.frameReady += FrameReady;

        return mImageFromVideo;
    }*/

    public void GetVideoProperties(string filePath)
    {
        //VideoPlayer myVideoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = filePath;
        //myVideoPlayer.renderMode = VideoRenderMode.APIOnly;
        //videoPlayer.targetTexture = new RenderTexture(1,1,0);
        //StartPrepareByPath(filePath);
        //RenderTexture vidTex = GetComponent<RenderTexture>();

        videoPlayer.Prepare();
        // vidTex = myVideoPlayer.texture;
        //videoTexture = myVideoPlayer.texture;
        //float videoWidth = vidTex.width;
        //float videoHeight = vidTex.height;

    }

    /*
    void FrameReady(VideoPlayer vp, long frameIndex)
    {
        Debug.Log("FrameReady " + frameIndex);
        videoPlayer.Pause();

        //rend.material.SetTexture("Default-Diffuse", Get2DTexture(vp));

        vp = null;

        //thumbnailOk = true;
    }

    void OnNewFrame(VideoPlayer source, long frameIdx)
    {
        int framesValue = 0;//Get video What frame of picture
        framesValue++;
        if (framesValue == 20)
        {
            renderTexture = source.texture as RenderTexture;
            //if (videoFrameTexture.width != renderTexture.width || videoFrameTexture.height != renderTexture.height)
            //{
              //  videoFrameTexture.Resize(renderTexture.width, renderTexture.height);
            //}
            RenderTexture.active = renderTexture;
            videoFrameTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            videoFrameTexture.Apply();
            RenderTexture.active = null;
            vp.sendFrameReadyEvents = false;
        }
    }*/

    /* void OnDisable()
     {
         vp.frameReady -= OnNewFrame;

         if (!File.Exists(Application.persistentDataPath + "/temp.jpg"))
         {
             ScaleTexture(videoFrameTexture, 800, 400, (Application.persistentDataPath + "/temp.jpg"));
         }
     }
     //Generate thumbnail
     void ScaleTexture(Texture2D source, int targetWidth, int targetHeight, string savePath)
     {

         Texture2D result = new Texture2D(targetWidth, targetHeight, TextureFormat.ARGB32, false);
         for (int i = 0; i < result.height; ++i)
         {
             for (int j = 0; j < result.width; ++j)
             {
                 Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                 result.SetPixel(j, i, newColor);
             }
         }
         result.Apply();
         File.WriteAllBytes(savePath, result.EncodeToJPG());
     }*/

    /*
     * public void SeekForward()
        {
            StartSeek(10.0f);
        }

        public void SeekBack()
        {
            StartSeek(-10.0f);
        }

        private void StartSeek(float seekAmount)
        {
            isVideoReady = false;
            videoPlayer.time += seekAmount;
        }
        public void PreviousVideo()
        {
            index--;
            //if (index == -1)
                //index = videos.Count - 1;
            StartPrepare(index);
        }
    */

    /*
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }*/

    /*
    public void PauseToggle()
    {
        IsPaused = !videoPlayer.isPaused;

        if (isPaused)
            videoPlayer.Pause();
        else
            videoPlayer.Play();
    }*/

    private void OnDestroy()
    {
        videoPlayer.seekCompleted -= OnComplete;
        videoPlayer.prepareCompleted -= OnComplete;
        videoPlayer.loopPointReached -= OnLoop;
    }

}