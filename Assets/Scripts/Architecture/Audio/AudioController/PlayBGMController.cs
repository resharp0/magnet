using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;



public class PlayBGMController : MonoBehaviour, IController
{
    public AudioSource BGM;

    public IArchitecture GetArchitecture()
    {
        return AudioAr.Interface;
    }

    void Start()
    {
        if (!GameObject.FindObjectOfType<AudioSource>())
        {
            GameObject.DontDestroyOnLoad(Instantiate(BGM));
            //Screen.SetResolution(1920, 1080, false);
            Application.targetFrameRate = 60;
        }
    }
}
