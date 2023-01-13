using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using B83.Image.BMP;

public class StreamingFiles : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuSphere;
    private int totalVideosFromGallery;
    private string rootPath;
    private string moviesPath;
    private string MoviesFolderName = "Movies";
    private string Audio2 = "myWav.wav";
    private string[] filesList;
    private List<string> videosList = new List<string>();
    private bool videoListFilled = false;

    void Awake() => GetVideosFromOculusGallery();

    public byte[] GetAudioMenu() 
    {
        byte[] fileData;
        fileData = File.ReadAllBytes(GetPath(Audio2));

        return fileData;
    }
 
    public WWW GetAudioFromFile()
    {
        string audioToLoad = GetPath(Audio2);
        Debug.Log("audioToLoad: " + audioToLoad);
        WWW request = new WWW(audioToLoad);
        return request;
    } 

    private string GetPath(string pathSearch)
    {
        string filePath = Application.persistentDataPath; //for computer folders
        filePath = Path.Combine(filePath, pathSearch);
        //Debug.Log("file: " + filePath);
        return filePath;
    }

    private void GetVideosFromOculusGallery()
    {
#if UNITY_EDITOR
        rootPath = Application.persistentDataPath; //for computer folders
#else
        rootPath = Application.persistentDataPath.Substring(0, Application.persistentDataPath.IndexOf("Android", StringComparison.Ordinal));
#endif
        moviesPath = Path.Combine(rootPath, MoviesFolderName);

        //Debug.Log("moviesPath: " + moviesPath);
        //Get All Files
        filesList = Directory.GetFiles(moviesPath);
        int countFiles = filesList.Length;
        //Debug.Log("countFile: " + countFiles);

        int i = 0;
        foreach (string videos in filesList)
        {
            string result = videos.Substring(videos.LastIndexOf('.') + 1);

            if (result == "mp4" || result == "avi" || result == "mov" || result == "mpg" || result == "mpeg")
            {
                //Get only videos
                videosList.Add(videos);
                //Debug.Log("video List add");
            }
            
            else if (result == ".jpeg" || result == ".png" || result == "jpg" || result == ".tga")
            {
                //Debug.Log("Found Images");
            }

            else
            {
                //Debug.Log("Found a files that is not a video or a image");
            }
            
            i++;

            videoListFilled = true;
            totalVideosFromGallery = videosList.Count;
        }
    }

    public bool GetVideoListFilled()
    {
        return videoListFilled;
    }

    public List<string> GetVideoNames()
    {
        return videosList;
    }

    public int GetTotalVideosFromGallery()
    {
        return totalVideosFromGallery;
    }
}
