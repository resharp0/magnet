using QFramework;
using UnityEngine;

public class DonotDestoryController : MonoBehaviour, IController
{
    public IArchitecture GetArchitecture()
    {
        return AudioAr.Interface;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}